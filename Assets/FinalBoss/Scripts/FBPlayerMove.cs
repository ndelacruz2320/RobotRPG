using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBPlayerMove : MonoBehaviour {
    public float speed;             //will change to private variables when finished
    public GameObject Follower;         //Using 3d import without declaring skeleton may do later
    public GameObject Camera;

    private float sprintSpeed;
    private float OSpeed;

    private Variables Var = Variables.getVariable();

    private float angle = 60;           //changing angle for camera movement
    private float angleV = 60;
    private float Achange = .01f;
    private float offset;                   //player and camera offset
    private float yoffset;
    private Vector3 jump = new Vector3(0.0f, 1000000.0f, 0.0f);    //jump force
    private Vector3 zero = new Vector3(0.0f, -50f, 0.0f);      //full stop sets velocity
    private Vector3 noFloat = new Vector3(0.0f, -100000.0f, 0.0f);  //in the air gravity isn't particular harsh with small mass but dealing with larger force is more complex

    private Rigidbody rb, rb2;
    private Vector3 realZero = new Vector3(0f, 0f, 0f);
    private int count;
    private int o;


    void Start() //gets sphere rigid and camera distance
    {
        sprintSpeed = speed * 1.4f;
        OSpeed = speed;

        rb = GetComponent<Rigidbody>();
        Var.VarArray[0, 0] = 2;


        for (int i = 0; i < 10; i++)//Resets every
        {
            Var.VarArray[1, i] = 0;
        }

    }

    void Update() //all movement
    {
        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = OSpeed;
        }
        //End of Sprint


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
                zero = new Vector3(0f, (100f - count * 2f) * transform.localScale.x / 7f, 0f);
            }
        }

        //Ensures that when camera rotates moveVertical and moveHorizontal are relative to player rotation
        Vector3 fromCameraToMe = transform.position - Camera.transform.position;
        fromCameraToMe.y = 0;

        fromCameraToMe.Normalize();

        rb.velocity = zero;
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");


        Vector3 movement = (fromCameraToMe * moveVertical + Camera.transform.right * moveHorizontal) * speed;
                                                                                                                  // Vector3 movement2 = new Vector3(1500 * moveHorizontal * Mathf.Cos(angle + 184.95f * turnspeed), 0.0f, 1500 * moveHorizontal * Mathf.Sin(angle + 184.95f * turnspeed));
        rb.AddForce(movement * speed);
       // rb.AddForce(movement2 * speed);



        //Objects from the game
        Follower.transform.position = new Vector3((transform.position.x), transform.position.y, (transform.position.z));


    }

}

