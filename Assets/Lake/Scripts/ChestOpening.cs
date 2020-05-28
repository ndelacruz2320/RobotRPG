using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpening : MonoBehaviour
{

    private bool canOpen=false;
    private bool hasRun=false;
    private Quaternion rotator;
    private int count=0;

    private Variables Var = Variables.getVariable();

    public int ChestNum=-1;
    public int ChestNum2 = -1;
    public int Village=0;

    public GameObject treasure;
    public GameObject fullChest;


    void Start()
    {
        if (Var.VarArray[0, ChestNum] != 0)
            Destroy(fullChest);
    }

	void Update ()
    {
        if (canOpen && Input.GetKeyDown(KeyCode.O))
        {
            if(!hasRun && Var.VarArray[5,Village] ==1)
                StartCoroutine(Open());
        }
    }

    private IEnumerator Open()
    {

        while (count < 70)
        {
            count++;
            transform.Rotate(0, 0, -2, 0);
            yield return new WaitForSeconds(.01f);
        }
        while(count>0)
        {
            count--;
            treasure.transform.position = new Vector3(treasure.transform.position.x, treasure.transform.position.y + .4f*transform.localScale.y, treasure.transform.position.z);
            yield return new WaitForSeconds(.01f);
        }
        while(count<10)
        {
            count++;
            yield return new WaitForSeconds(.5f);
        }
        Destroy(treasure.gameObject);
        Variables updater = Variables.getVariable();
        updater.VarArray[0, ChestNum] = 1;
        updater.VarArray[0, ChestNum2] = 1;
        Destroy(fullChest.gameObject);
        hasRun = true;

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            canOpen = true;

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            canOpen = false;

        
                
    }
}
