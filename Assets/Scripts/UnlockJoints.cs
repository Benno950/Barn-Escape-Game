using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A simple script that will unlock the goat's constraints resulting in funnier crashes
public class UnlockJoints : MonoBehaviour
{
    
    public Rigidbody rb; //Cache rigidbody
    void Start()
    {
       // rb = GetComponent<Rigidbody>(); // Store the rigid body to be called later
    }

    void OnCollisionEnter(Collision other) 
    {

        switch (other.gameObject.tag) // A system that detects objects based on tags.
        {
            case "Safezone":
                break; 

            case "Finish":
                ClearConstraints();
                break; 

            default:  // Any other object
                ClearConstraints();
                break;
        }       
    }

    public void ClearConstraints()
    {
        Debug.Log("Unlocking Joints");
        //rb.constraints = RigidbodyConstraints.None;
        rb.isKinematic = false;
    }
}
