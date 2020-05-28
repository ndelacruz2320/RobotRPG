using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRay : MonoBehaviour
{

    private Rigidbody rb;
    private float speed = -100.0f;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0f, speed, 0f);
        }

}

