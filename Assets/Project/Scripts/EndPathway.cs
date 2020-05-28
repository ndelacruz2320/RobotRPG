using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPathway : MonoBehaviour
{

    public GameObject RobotParent;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Robot")
            Destroy(RobotParent);
    }

}
