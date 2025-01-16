using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloppyBone : MonoBehaviour
{
    public float boneMass = 0.1f; // Adjust this value to control the bone's "floppiness"

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>(); // Get the Rigidbody component

        if (rb == null)

        {

            rb = gameObject.AddComponent<Rigidbody>(); // Add a Rigidbody if it doesn't exist

        }

        rb.mass = boneMass; // Set the bone's mass

        rb.drag = 0.5f; // Add drag to slow down bone movement

        rb.angularDrag = 0.5f; // Add angular drag to control rotation

        rb.useGravity = false;
    
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
