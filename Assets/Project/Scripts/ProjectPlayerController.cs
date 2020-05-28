//Contains movement for the player in the overworld based upon 
//moving with a sphere. Moves the camera as well in a circle around the player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectPlayerController : MonoBehaviour
{

    public float speed;             //will change to private variables when finished
    public float turnspeed;
    public GameObject MainCamera;      //Camera only follows player and is controlled here
    public GameObject Follower;         //Using 3d import without declaring skeleton may do later
    public GameObject Menu;
    public GameObject Control;

    private float sprintSpeed;
    private float OSpeed;

    private Variables Var = Variables.getVariable();
    private bool MenuOn=false;
    private bool ControlOn = false;

    private float angle = 60;           //changing angle for camera movement
    private float Achange = .01f;
    private float offset;                   //player and camera offset
    private float yoffset;
    private Vector3 jump = new Vector3(0.0f, 1000000.0f, 0.0f);    //jump force
    private Vector3 zero = new Vector3(0.0f, -50f, 0.0f);      //full stop sets velocity
    private Vector3 noFloat = new Vector3(0.0f, -100000.0f, 0.0f);  //in the air gravity isn't particular harsh with small mass but dealing with larger force is more complex

    private Rigidbody rb;
    private int count;
    

    void Start() //gets sphere rigid and camera distance
    {
        sprintSpeed = speed * 2f;
        OSpeed = speed;

        rb = GetComponent<Rigidbody>();
        offset = Mathf.Sqrt((MainCamera.transform.position.x - transform.position.x) * (MainCamera.transform.position.x - transform.position.x) + (MainCamera.transform.position.y - transform.position.y) * (MainCamera.transform.position.y - transform.position.y) + (MainCamera.transform.position.z - transform.position.z) * (MainCamera.transform.position.z - transform.position.z));
        yoffset = MainCamera.transform.position.y - transform.position.y;
        Var.VarArray[0, 0] = 2;

        Menu.SetActive(false);
        Control.SetActive(false);

        for(int i = 0; i <10; i++)//Resets every
        {
            Var.VarArray[1, i] = 0;
        }

        transform.position = Var.OVP;

    }

    void Update() //all movement
    {
        //Sprint
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = OSpeed;
        }
        //End of Sprint

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!MenuOn)
            {
                Menu.SetActive(true);
                MenuOn = true;
                Var.OVV = rb.velocity;
                rb.velocity = new Vector3(0f, 0f, 0f);
            }
            else
            {
                Menu.SetActive(false);
                MenuOn = false;
                rb.velocity = Var.OVV;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!ControlOn)
            {
                Control.SetActive(true);
                ControlOn = true;
                Var.OVV = rb.velocity;
                rb.velocity = new Vector3(0f, 0f, 0f);
            }
            else
            {
                Control.SetActive(false);
                ControlOn = false;
                rb.velocity = Var.OVV;
            }
        }

        if (!MenuOn && !ControlOn)
        {
            if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y > (-10f))    //jump function
            {
                count++;
            }

            if (count > 0)
            {

                if (count > 100)
                {
                    zero = new Vector3(0f, -50f, 0f);
                    count = 0;
                }
                else
                {
                    count++;
                    zero = new Vector3(0f, (100f - count * 2f)*transform.localScale.x/7f, 0f);
                }
            }

            rb.velocity = zero;

            //float turnHorizontal = Input.GetMouseButton("Mouse X");
            float turnHorizontal = -Input.GetAxis("Horizontal"); //Switched to turn camera
            float moveVertical = -Input.GetAxis("Vertical");


            angle = angle + turnHorizontal * Achange;
            Vector3 movement = new Vector3(1500 * moveVertical * Mathf.Cos(angle * turnspeed), 0.0f, 1500 * moveVertical * Mathf.Sin(angle * turnspeed)); //changed from moveH to 0


            rb.AddForce(movement * speed);



            //Objects from the game
            MainCamera.transform.position = new Vector3(transform.position.x + offset * Mathf.Cos(angle * turnspeed), transform.position.y + yoffset, transform.position.z + offset * Mathf.Sin(angle * turnspeed));//offset*Mathf.Cos(angle),0.0f,offset*Mathf.Sin(angle));
            Follower.transform.position = new Vector3((transform.position.x), transform.position.y, (transform.position.z));
            //Turning
            Follower.transform.rotation = MainCamera.transform.rotation;



            MainCamera.transform.LookAt(transform);                 //stays with player
            transform.eulerAngles = (MainCamera.transform.eulerAngles);
        }
    }

}
