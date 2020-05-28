using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Variables
{
    public static Variables getVariable()
    {
        if (Var == null)
        {
            Var = new Variables();
        }
        return Var;
    }

    public static Variables remake()
    {
        Var = new Variables();
        return Var;
    }

    private static Variables Var;


    public int[,] VarArray = new int[10, 10];
    public Vector3[] RobotPositions = new Vector3[5];
    public int[] RobotNodes = new int[5];

    public Vector3 OVP = new Vector3(4000f, 150f, 3815f);
    public Vector3 OVV = new Vector3(0f, 0f, 0f);





 //   public GameObject MakeVariables()
//    {         

        
//   }  

 }
      

    

