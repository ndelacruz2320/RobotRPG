using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBall : MonoBehaviour
{

    private Rigidbody rb;
    private int count=0;
    private float speed = 75f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(PBall());
    }

    private IEnumerator PBall()
    {
        while (count < 4)
        {
            if (count == 0)
                rb.velocity = transform.forward * speed;
            if (count == 1)
                rb.velocity = transform.forward * -1f *speed;
            if (count == 2)
                Destroy(gameObject);
            count++;
            print(count);
            yield return new WaitForSeconds(.5f);
        }
    }

}

