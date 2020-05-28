using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotOverworld : MonoBehaviour
{

    public int robot;
    public int numRobot;
    private Variables Var = Variables.getVariable();

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Var.VarArray[9, robot] = numRobot;
            Var.VarArray[7, 0] = robot;
            //print(Var.VarArray[9, robot]);
        }
    }

 /*   void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Var.VarArray[9, robot] = 0;
        }
    }
    */
}
