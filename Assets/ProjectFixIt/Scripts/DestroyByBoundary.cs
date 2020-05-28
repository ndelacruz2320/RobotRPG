using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    public GameObject Explosion;

    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
        //Instantiate(Explosion, other.transform.position, other.transform.rotation);
    }

}
