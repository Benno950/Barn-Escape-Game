using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float mainThrustSpeed = 750f;
    [SerializeField] float mainRotationSpeed = 100f;
    [SerializeField] ParticleSystem mainThrusterParticles;


    Rigidbody rb; //Cache rigidbody
    AudioSource audioSource; // Cache flight sound effect

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Store the rigid body to be called later
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        ProcessThrust();
        ProcessRotation();

    }

    // Update is called once per frame
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.W)) 
        {
            rb.AddRelativeForce(Vector3.up * mainThrustSpeed * Time.deltaTime);
            if(!audioSource.isPlaying) 
            {
                audioSource.Play();
            }
            if (!mainThrusterParticles.isPlaying) {
                mainThrusterParticles.Play();
            }

        } 
        else
        {
            audioSource.Pause();
            mainThrusterParticles.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A)) 
        {
            ApplyRotation(mainRotationSpeed);
        }

        else if (Input.GetKey(KeyCode.D)) 
        {
            ApplyRotation(-mainRotationSpeed);
        }     
    }

    void ApplyRotation(float rotationDirection)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationDirection * Time.deltaTime);
        rb.freezeRotation = false;
    }
}


