using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFire : MonoBehaviour
{
    public GameObject spawnPoint;
    private bool test = false;
    private int count = 0;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(extend(1f));
    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
                spawnPoint.transform.eulerAngles = new Vector3(spawnPoint.transform.eulerAngles.x, spawnPoint.transform.eulerAngles.y + 1f, spawnPoint.transform.eulerAngles.z); //roates laser
            
            //count = 0;
        }
    }
    private IEnumerator extend(float num)
    {
        while (count < 45)
        {
            count++;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + num, transform.localScale.z);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + num);
            yield return new WaitForSeconds(.01f);
        }
        test = true;
    }

}