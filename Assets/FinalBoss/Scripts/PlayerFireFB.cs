using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireFB : MonoBehaviour
{
    public GameObject RepairPart;
    public Transform PartLauncher;

    private float nextFire;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireBasicShot();
        }

    }
    private void FireBasicShot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + .25f;
            Instantiate(RepairPart, PartLauncher.position, PartLauncher.rotation);
        }
    }
}
