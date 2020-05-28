using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossHealth : MonoBehaviour
{
    public int Health = 10; 
    public GameObject Explosion;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Health == 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Part")
        {
            Health--;
            if (Health > 0)
                Instantiate(Explosion, other.transform.position, other.transform.rotation);

            Destroy(other.gameObject);
        }
    }
}
