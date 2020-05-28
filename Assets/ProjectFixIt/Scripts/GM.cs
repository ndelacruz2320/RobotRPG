using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    private Variables Var = Variables.getVariable();
    public GameObject B, G, R, W, E, Player;

    public GameObject RobotSpawnPoint;


    private Vector3 playerSpawn = new Vector3(-6.24f, 7.6f, -40.41f);
    private int count = 0;
    // Use this for initialization
    void Start()
    {
        //Var.VarArray[9, 1] = 2; FOR TESTING@@@
        for (int i = 0; i < 10; i++)
        {
            if (Var.VarArray[9, i] != 0)
            {
                StartCoroutine (spawn(i));
            }
            
            

        }
  //testing      Instantiate(Player, playerSpawn, transform.rotation);
    }

    private IEnumerator spawn(int i)
    {
        while (Var.VarArray[9, i] != 0)
        {

            switch (i)
            {
                case 0:
                    Instantiate(W, RobotSpawnPoint.transform.position, transform.rotation);
                    break;
                case 1:
                    Instantiate(B, RobotSpawnPoint.transform.position, transform.rotation);
                    break;
                case 2:
                    Instantiate(G, RobotSpawnPoint.transform.position, transform.rotation);
                    break;
                case 3:
                    Instantiate(E, RobotSpawnPoint.transform.position, transform.rotation);
                    break;
                case 4:
                    Instantiate(R, RobotSpawnPoint.transform.position, transform.rotation);
                    break;
            }
            count++;
            Var.VarArray[9, i] = Var.VarArray[9, i] - 1;
            yield return new WaitForSeconds(3f);
        }
    }
}
