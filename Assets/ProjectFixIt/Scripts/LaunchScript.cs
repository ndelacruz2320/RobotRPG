using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchScript : MonoBehaviour
{

    private Rigidbody rb;
    public float speed=75.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
            rb.velocity = transform.forward * speed;
    }

}

