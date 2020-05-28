using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OWRobotManager : MonoBehaviour
{
    private Variables Var = Variables.getVariable();

    private int robots = 1;
    private int counter=0;
    private int RobotFound = 0;
    private bool isRunning = false;


    public GameObject[] Robots = new GameObject[5];
    public GameObject[] SpawnPoints = new GameObject[5];

    void Start()
    {

        for(int i=0; i<5; i++)
        {
            Var.RobotPositions[i] = SpawnPoints[i].transform.position;
        }

    }

    void Update()
    {
        for(int i=0; i<5; i++)
        {
            if(Var.VarArray[8,i] ==1)
            {
                RobotFound = 1;
            }
        }
        if (!isRunning && RobotFound == 1)
        {
            StartCoroutine(SpawnBots());
        }
    }

    private IEnumerator SpawnBots()
    {
        isRunning = true;
        while(counter==0)
        { 
            for (int i = 0; i < 5; i++)
            {
                if (Var.VarArray[8, i] == 1)
                {

                    Instantiate(Robots[i], SpawnPoints[i].transform.position, transform.rotation);
                }
            }
            
            yield return new WaitForSeconds(40f);
        }
    }

}
