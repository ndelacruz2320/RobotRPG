using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int Health = 10;
    public GameObject Explosion;
    public GameObject ParentObject;
    private int time = 0;

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Destroy(ParentObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Laser")
        {
            if (time + 2 < Time.time) //Ensures that player doesnt die immidiatly with many collision detections
            {
                time = time + 2;
                Health = Health - 2;
                print(Health);
            }
        }
        else if (other.tag == "RAttack")
        {
            Health = Health -1;
            print(Health);
            Destroy(other.gameObject);
        }

    }

}

