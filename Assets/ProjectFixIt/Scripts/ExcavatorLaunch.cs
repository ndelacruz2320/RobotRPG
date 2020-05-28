using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcavatorLaunch : MonoBehaviour
{

    private Rigidbody rb;
    public float speed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        speed += 5f;
        rb.velocity = transform.forward * speed;
    }

}

