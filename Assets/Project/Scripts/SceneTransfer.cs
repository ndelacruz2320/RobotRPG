using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    private bool Ready=false;
    private bool EndReady = false;
    private int Scene;
    private Variables Var = Variables.getVariable();

    public GameObject[] RobotPosition = new GameObject[5];
    public int[] RobotNodes = new int[5]; 

    //   private Vector3 position = new Vector3(0f, 0f, 0f);

    //  public static GameObject Player;


    void Update()
    {
        if(Ready)
        {

            //           Var.OVP = transform.position;
            if (SceneManager.GetActiveScene().name == "Project")
            {
                Var.OVP = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }

                if (Scene == 1)
                    SceneManager.LoadScene("ProjectFixIt");


            if (Input.GetKeyDown(KeyCode.K))
            {


                //                if (SceneManager.GetActiveScene().name == "Project")
                //                    position = transform.position;

                switch (Scene)
                {

                    case 2:
                        if (Var.VarArray[3, 3] == 0)
                        {
                            Var.VarArray[3, 3] = 1; //Happens once
                            Var.VarArray[8, 3] = 1;
                        }
                        SceneManager.LoadScene("Project_Village_Castle");
                        break;
                    case 3:
                        if (Var.VarArray[3, 4] == 0)
                        {
                            Var.VarArray[3, 4] = 1; //Happens once
                            Var.VarArray[8, 4] = 1;
                        }
                        SceneManager.LoadScene("Project_Village_Forrest");
                        break;
                    case 4:
                        if (Var.VarArray[3, 1] == 0)
                        {
                            Var.VarArray[3, 1] = 1; //Happens once
                            Var.VarArray[8, 1] = 1;
                        }
                        SceneManager.LoadScene("Project_Village_Desert");
                        break;
                    case 5:
                        if (Var.VarArray[3, 2] == 0)
                        {
                            Var.VarArray[3, 2] = 1; //Happens once
                            Var.VarArray[8, 2] = 1;
                        }
                        SceneManager.LoadScene("Project_Village_Burnt");
                        break;
                    case 6:
                        if (Var.VarArray[3, 0] == 0)
                        {
                            Var.VarArray[3, 0] = 1; //Happens once
                            Var.VarArray[8, 0] = 1;
                        }
                        SceneManager.LoadScene("Project_Village_Lake");
                        break;
                    case 7:

                        EndReady = true;
                        for(int i=0; i<5; i++)
                        {
                            if(Var.VarArray[3,i]==0 || Var.VarArray[8,i]!=0)
                            {
                                EndReady = false;
                            }
                        }

                        if (EndReady)
                            SceneManager.LoadScene("PortalStage");
                        else
                            SceneManager.LoadScene("Project");
 //                       Player.transform.position = position;
                        break;
                    case 8:
                        SceneManager.LoadScene("FinalStage");
                        break;
                }
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {


        if (other.tag == "Robot")//change later for multiple robot cases
        {
            Scene = 1;
            Ready = true;
        }
        if (other.tag == "VCastle")
        {
            Scene = 2;
            Ready = true;
        }
        if (other.tag == "VForrest")
        {
            Scene = 3;
            Ready = true;
        }
        if (other.tag == "VDesert")
        {
            Scene = 4;
            Ready = true;
        }
        if (other.tag == "VBurnt")
        {
            Scene = 5;
            Ready = true;
        }
        if (other.tag == "VLake")
        {
            Scene = 6;
            Ready = true;
        }
        if (other.tag == "Overworld")
        {
            Scene = 7;
            Ready = true;
        }
        if (other.tag == "Final")
        {
            Scene = 8;
            Ready = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        Ready = false;
    }
}
