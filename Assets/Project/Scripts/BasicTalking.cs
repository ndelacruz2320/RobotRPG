using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicTalking : MonoBehaviour
{

    private bool CanTalk=false;
    private bool InConvo;
    private bool SecondLine = false;
    private int Index=0;
    private float location;
    private Quaternion angle;
    private Variables Var = Variables.getVariable();
    
    public Text Dialog;
    public GameObject Player;
    public string[] Lines = new string[6];
    public string[] Lines1 = new string[6];
    public int whichLine = 0;
    
	void Start()
    {
        
        CanTalk = false;
        Index = -1;
        Dialog.text = "";
        if (Var.VarArray[2, whichLine] == 1)
        {
            Lines = Lines1;
            SecondLine = true;
        }
    }

	void Update ()
    {


		if(Input.GetKeyDown(KeyCode.J) && CanTalk==true)
        {

            if (Index < Lines.Length-1)
            {
                Index++;
                Dialog.text = Lines[Index];
                if(SecondLine && Index>1)
                {
                    Var.VarArray[5, whichLine] = 1;
                }
             
            }
        }
	}

    void FixedUpdate()
    {
        location = -Mathf.Atan((Player.transform.position.z - Dialog.GetComponent<RectTransform>().position.z) /(Player.transform.position.x- Dialog.GetComponent<RectTransform>().position.x))*180.0f/3.14f-90.0f;

        if ((Player.transform.position.x - Dialog.GetComponent<RectTransform>().position.x) < 0)
            location = location + 180;

        angle.eulerAngles = new Vector3(0.0f, location, 0.0f);
        Dialog.GetComponent<RectTransform>().rotation = angle;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CanTalk = true;
            Dialog.text = "(Press J to talk)";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CanTalk = false;
            Dialog.text = "";
            Index = -1;
        }
    }
}
