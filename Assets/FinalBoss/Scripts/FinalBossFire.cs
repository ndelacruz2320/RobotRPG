using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossFire : MonoBehaviour {
    public GameObject attackSpawn;
    public GameObject Attack;
    private Quaternion fireRotation;

    void Start()
    {
        StartCoroutine(BalisticBurst());
	}

    private IEnumerator BalisticBurst()
    {
        while (true)
        { 
            for (int i = 0; i < 60; i++)
            {
                GameObject allBalls = Instantiate(Attack, attackSpawn.transform.position, fireRotation);
                fireRotation.eulerAngles = new Vector3(0f, fireRotation.eulerAngles.y + 6f, 0f);
                Destroy(allBalls, 4); //deletes wave after 4 seconds
            }
        yield return new WaitForSeconds(5f);
        }
    }
}
