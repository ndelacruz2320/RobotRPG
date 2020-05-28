using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GhostFixIt : MonoBehaviour
{
    private WaitForSeconds wait = new WaitForSeconds(.75f);

    private int num;
    private int lastMove;
    private int Health = 11;//Test
    private int count = 0;

    private float distance;
    private float lowerX, lowerZ, maxD;
    private string Line = "7hAnk Y0u 4 F1xIn6 M3";
    private bool hasThanked = false;
    private Quaternion fireRotation;
    private Quaternion Face;


    private Variables Var = Variables.getVariable();
    private int ArrayNum = -1;

    public Text Dialog;

    public System.Random randNum = new System.Random();
    public GameObject Attack;
    public GameObject attackSpawn;
    public GameObject Explosion;

    void Start()
    {
        lowerX = -42.5f;
        lowerZ = -42.5f;
        maxD = 80f;   //square 

        distance = 10f;

        Dialog.text = "";

        StartCoroutine(Move());
        StartCoroutine(RockThrow());

        for (int i = 0; i < 10; i++)
        {
            if (Var.VarArray[1, i] == 0 && ArrayNum == -1)
            {
                ArrayNum = i;
            }
        }


    }

    void FixedUpdate()
    {
        Var.VarArray[1, ArrayNum] = Health;
        if (Health <= 0 && !hasThanked)
        {
            Dialog.text = Line;

            if (count < 100)
                count++;

            if (count == 100)
            {
                Dialog.text = "";
                hasThanked = true;
            }
        }
    }

    private IEnumerator RockThrow()
    {
        while (Health > 0)
        {
            
            Instantiate(Attack, attackSpawn.transform.position, Face);
            Face.eulerAngles = new Vector3(Face.eulerAngles.x, Face.eulerAngles.y + 90f, Face.eulerAngles.z);
            Instantiate(Attack, attackSpawn.transform.position, Face);
            Face.eulerAngles = new Vector3(Face.eulerAngles.x, Face.eulerAngles.y + 90f, Face.eulerAngles.z);
            Instantiate(Attack, attackSpawn.transform.position, Face);
            Face.eulerAngles = new Vector3(Face.eulerAngles.x, Face.eulerAngles.y + 90f, Face.eulerAngles.z);
            Instantiate(Attack, attackSpawn.transform.position, Face);

            yield return new WaitForSeconds(.75f);
        }
    }

    private IEnumerator Move()
    {
        while (Health > 0)
        {

            num = randNum.Next(4);

            if ((num == 0) && ((transform.position.x + distance) < (lowerX + maxD)) && ((transform.position.z+ distance) < (lowerZ +maxD)))//upper right
            {
                transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z+distance);
                lastMove = 1;
                Face.eulerAngles = new Vector3(0f, 45f, 0f);
                transform.rotation = Face;
            }


            if ((num == 1) && ((transform.position.x + distance) < (lowerX+maxD)) && ((transform.position.z - distance) > lowerZ))//down right
            {
                transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z-distance);
                lastMove = 2;
                Face.eulerAngles = new Vector3(0f, 135f, 0f);
                transform.rotation = Face;
            }


            if ((num == 2) && ((transform.position.x - distance) > (lowerX)) && ((transform.position.z - distance) > lowerZ))//down left
            {
                transform.position = new Vector3(transform.position.x-distance, transform.position.y, transform.position.z - distance);
                lastMove = 3;
                Face.eulerAngles = new Vector3(0f, 225f, 0f);
                transform.rotation = Face;
            }

            if ((num == 3) && ((transform.position.x - distance) > lowerX) && ((transform.position.z + distance) < (lowerZ + maxD)))//upper left
            {
                transform.position = new Vector3(transform.position.x-distance, transform.position.y, transform.position.z + distance);
                lastMove = 4;
                Face.eulerAngles = new Vector3(0f, 315f, 0f);
                transform.rotation = Face;
            }

            //Instantiate(RoundAttack, transform.position, transform.rotation);

            yield return wait;

        }
    }


    void OnTriggerEnter(Collider other) //Handles player collisions
    {
        if (other.tag == "DeathRay")
        {
            Health = 0;
            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(other.gameObject);
        }
        if (other.tag == "Part" || other.tag == "RAttack" || other.tag == "LargePart")
        {
            if (other.tag == "Part")
                Health--;

            if (other.tag == "LargePart")
                Health -= 2;

            if (Health > 0)
                Instantiate(Explosion, other.transform.position, other.transform.rotation);

            Destroy(other.gameObject);
        }
        else if (other.tag == "WAttack")
        { }
        else if (Health > 0)
        {
            switch (lastMove)
            {
                case 1:
                    if (transform.position.x - distance > lowerX)
                        transform.position = new Vector3(transform.position.x - distance, transform.position.y, transform.position.z);
                    break;
                case 2:
                    if (transform.position.x + distance < lowerX + maxD)
                        transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
                    break;
                case 3:
                    if (transform.position.z - distance > lowerZ)
                        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - distance);
                    break;
                case 4:
                    if (transform.position.z + distance < lowerZ + maxD)
                        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + distance);
                    break;
            }
        }

    }
}