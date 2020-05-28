using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldRobotMovement : MonoBehaviour
{

    private Variables Var = Variables.getVariable();

    private int index = 0;
    public int directions;

    //public int size;
    public GameObject[] Directions = new GameObject[10];
    public GameObject Parent;


    public int RobotNumber;
    public GameObject StartSpawn;
    private Vector3 MySpawn;

    void Start()
    {
        /*
        //Directions = new GameObject[size];
        index = Var.RobotNodes[RobotNumber];
        transform.position = Var.RobotPositions[RobotNumber];
        if(index==0)
        {
            MySpawn = transform.position;
        }
        */


        MySpawn = StartSpawn.transform.position;
        if(Var.RobotNodes[RobotNumber]<9)
        {
            transform.position = Var.RobotPositions[RobotNumber];
            index = Var.RobotNodes[RobotNumber];
        }
        StartCoroutine(OWMovement());
    }

    private IEnumerator OWMovement()
    {

        while (index < 10)
        {

            if (transform.position.x > Directions[index].transform.position.x + 1 || transform.position.x < Directions[index].transform.position.x - 1)
            {
                if (transform.position.x < Directions[index].transform.position.x)
                {
                    directions = 1;
                }
                else
                {
                    directions = -1;
                }

                transform.position = new Vector3(transform.position.x + (1f * directions), transform.position.y, transform.position.z);
            }
            else if (transform.position.z > Directions[index].transform.position.z + 1 || transform.position.z < Directions[index].transform.position.z - 1)
            {
                if (transform.position.z < Directions[index].transform.position.z)
                {
                    directions = 1;
                }
                else
                {
                    directions = -1;
                }

                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (1f * directions));
            }
            else if (index < 9)
            {
                index++;
            }

            yield return new WaitForSeconds(.01f);
        }
        index = 0;

    }

    void FixedUpdate()
    {
        if((transform.position.x - Directions[9].transform.position.x < 5f || transform.position.x - Directions[9].transform.position.x > -5f) && (transform.position.z - Directions[9].transform.position.z < 5f || transform.position.z - Directions[9].transform.position.z > -5f))
        {
            Var.RobotPositions[RobotNumber] = transform.position;
            Var.RobotNodes[RobotNumber] = index;

        }
        else
        {
            Var.RobotPositions[RobotNumber] = MySpawn;
        }

//        if (Mathf.Abs(transform.position.x - Directions[0].transform.position.x) > 1000 || Mathf.Abs(transform.position.y - Directions[0].transform.position.y) > 1000)
 //           Destroy(Parent);
    }
}
