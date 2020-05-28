﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RussianFixIt : MonoBehaviour
{
    private WaitForSeconds wait = new WaitForSeconds(1.5f);

    private int num;
    private int lastMove;
    private int Health = 11;//Test
    private int count = 0;
    private int counter = 0;

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
    public GameObject Axe;

    void Start()
    {
        lowerX = -42.5f;
        lowerZ = -42.5f;
        maxD = 80f;   //square 

        distance = 30f;

        Dialog.text = "";

        StartCoroutine(Move());
        StartCoroutine(TrunkSmash());

        for (int i = 0; i < 10; i++)
        {
            if (Var.VarArray[1, i] == 0 && ArrayNum == -1)
            {
                ArrayNum = i;
            }
        }
        Axe.SetActive(false);
        print("Excavator arraynum" + ArrayNum);


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

    private IEnumerator TrunkSmash()
    {
        while (Health > 0)
        {
            Axe.SetActive(true);
            counter = 0;
            StartCoroutine(Spin());
            yield return new WaitForSeconds(1.5f);
        }
    }

    private IEnumerator Spin()
    {
        while(counter<16)
        {
            counter++;
            Face.eulerAngles = new Vector3(0f, Face.eulerAngles.y + 24f, 0f);
            transform.rotation = Face;
            if (counter == 15)
                Axe.SetActive(false);

            yield return new WaitForSeconds(.1f);
        }
    }

    private IEnumerator Move()
    {
        while (Health > 0)
        {

            num = randNum.Next(4);

            if ((num == 0) && (transform.position.x + distance) < (lowerX + maxD))
            {
                transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
                lastMove = 1;
                fireRotation.eulerAngles = new Vector3(0f, 90f, 0f);
                Face.eulerAngles = new Vector3(0f, 90f, 0f);
                transform.rotation = Face;
            }


            if ((num == 1) && (transform.position.x - distance) > (lowerX))
            {
                transform.position = new Vector3(transform.position.x - distance, transform.position.y, transform.position.z);
                lastMove = 2;
                fireRotation.eulerAngles = new Vector3(0f, -90f, 0f);
                Face.eulerAngles = new Vector3(0f, -90f, 0f);
                transform.rotation = Face;
            }


            if ((num == 2) && (transform.position.z + distance) < (lowerZ + maxD))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + distance);
                lastMove = 3;
                fireRotation.eulerAngles = new Vector3(0f, 0f, 0f);
                Face.eulerAngles = new Vector3(0f, 0f, 0f);
                transform.rotation = Face;
            }

            if ((num == 3) && (transform.position.z - distance) > (lowerZ))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - distance);
                lastMove = 4;
                fireRotation.eulerAngles = new Vector3(0f, 180f, 0f);
                Face.eulerAngles = new Vector3(0f, 180f, 0f);
                transform.rotation = Face;
            }

            //Instantiate(RoundAttack, transform.position, transform.rotation);

            yield return wait;

        }
    }


    void OnTriggerEnter(Collider other) //Handles player collisions
    {
        if(other.tag == "DeathRay")
        {
            Health = 0;
            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(other.gameObject);
        }
        if (other.tag == "Part" || other.tag == "WAttack" || other.tag == "LargePart")
        {
            if (other.tag == "Part")
                Health--;

            if (other.tag == "LargePart")
                Health -= 2;

            if (Health > 0)
                Instantiate(Explosion, other.transform.position, other.transform.rotation);

            Destroy(other.gameObject);
        }
        else if (other.tag == "RAttack")
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