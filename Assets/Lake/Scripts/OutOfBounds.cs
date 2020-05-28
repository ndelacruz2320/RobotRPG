using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{

    public GameObject spawnpoint;
	
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "OoB")
            transform.position = spawnpoint.transform.position;
    }
}
