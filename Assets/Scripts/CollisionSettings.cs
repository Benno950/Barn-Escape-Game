using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionSettings : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2.5f;

    [SerializeField] AudioClip victorySound;
    [SerializeField] AudioClip crashSound;

    [SerializeField] ParticleSystem winParticle;
    [SerializeField] ParticleSystem crashParticle;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        
    }

    void Update() 
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys() 
    {
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            LoadNextLevel();
        }
    }

    // Start is called before the first frame update
    void OnCollisionEnter(Collision other) 
    {

        if (isTransitioning) {return;} // Stop the gameloop

        switch (other.gameObject.tag) // A system that detects objects based on tags.
        {
            case "Safezone":
                Debug.Log("You have Spawned");
                break; 

            case "Finish":
                VictorySequence();
                break; 

            default:  // Any other object
                PlayerCrashSequence();
                break;
        }       
    }

    void PlayerCrashSequence() // Sound effects, VFX and delays on Lose
    {
        isTransitioning = true;
        crashParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel" , levelLoadDelay );
    }

    void VictorySequence() //Sound effects, VFX and delays on Win
    {
        isTransitioning = true;
        winParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(victorySound);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel" , levelLoadDelay );
    }


    void ReloadLevel() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) 
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}

