using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerFixItMove : MonoBehaviour
{

    private bool moveForward, moveLeft, moveRight, moveBack;
    private int lastMove;
    private int Health = 11;//Test
    private int RobotNumber = 0;

    private float distance;
    private float lowerX, lowerZ, maxD;
    private bool hasBroke = false;

    private float nextFire;
    //public float fireRate;

    private Quaternion LaunchRotation;
    private Quaternion ExperimentalLaunchRotation;
    private Quaternion BrokenAngle;
    private Quaternion Face;

    private int FireMode = -1;
    private Variables Var;
    private int MessageCounter = 0;

    private int[] ChosenFM = new int[4];
    private int ChosenCount = 0;

    private bool Victory = false;
    private bool hasEnded = false;
    private string VMessage = "You Win";
    private string LMessage = "You Lose";
    private string TempString = "";

    public GameObject RepairPart;
    public GameObject BigRepairPart;
    public GameObject Broken;
    public GameObject Explosion;
    public Transform PartLauncher;

    public GameObject ExploderPart;
    public GameObject PBPart;
    public GameObject DeathRayPart;
    public GameObject LandMine;

    public Text Message;




    // Use this for initialization
    void Start()
    {

        lowerX = -42.5f;
        lowerZ = -42.5f;
        maxD = 80f;   //square 

        distance = 10f;
        Var = Variables.getVariable();

        for (int i = 0; i < 10; i++)
        {
            if (Var.VarArray[9, i] != 0)
            {
                RobotNumber = i;
            }
        }
        //Test
        RobotNumber = Var.VarArray[7, 0];

        
        for (int i = 0; i < 10; i++)
        {
            if (Var.VarArray[0, i] == 2)
            {
                ChosenFM[ChosenCount] = i;
                ChosenCount++;
            }
        }
    }
    void Update()
    {
        Victory = true;

        for (int i = 0; i < 10; i++)
        {
            if (Var.VarArray[1, i] > 0)
                Victory = false;
        }

        if (Victory && !hasEnded)
        {
            Var.VarArray[2, RobotNumber] = 1;
            print("Victory and has ended");
            StartCoroutine(EndMessage(1));
            Victory = false;
            hasEnded = true;
        }



        if (Health > 0)
        {
            moveForward = Input.GetKeyDown(KeyCode.W);
            moveBack = Input.GetKeyDown(KeyCode.S);
            moveLeft = Input.GetKeyDown(KeyCode.A);
            moveRight = Input.GetKeyDown(KeyCode.D);

            Move();

            switch (lastMove)
            {
                case 1:
                    LaunchRotation.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
                    break;
                case 2:
                    LaunchRotation.eulerAngles = new Vector3(0.0f, -90.0f, 0.0f);
                    break;
                case 3:
                    LaunchRotation.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                    break;
                case 4:
                    LaunchRotation.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                    break;
            }



            //Fire
            if (Input.GetKeyDown(KeyCode.J))
            {
                FireModeChooser(0);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                FireModeChooser(1);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                FireModeChooser(2);
            }
            if (Input.GetKeyDown(KeyCode.Semicolon))
            {
                FireModeChooser(3);
            }




        }
        else
        {
            if (!hasBroke)
            {
                BrokenAngle.eulerAngles = new Vector3(-90.0f, 0f, 0f);
                Instantiate(Broken, new Vector3(transform.position.x, transform.position.y - 5, transform.position.z + 5f), BrokenAngle);
                hasBroke = true;
                StartCoroutine(EndMessage(0));
            }

            if (Input.GetKeyDown(KeyCode.C))
                SceneManager.LoadScene("Project");
            if (Input.GetKeyDown(KeyCode.Q))
                SceneManager.LoadScene("Menu");
        }

    }

    private void FireBasicShot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + .25f;
            Instantiate(RepairPart, PartLauncher.position, LaunchRotation);
        }
    }

    private void FireBigShot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + .75f;
            StartCoroutine(BigShot());
        }
    }

    private void FireTrippleShot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + .25f;
            StartCoroutine(TrippleShot());
        }
    }

    private void FireRapidShot()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(RepairPart, PartLauncher.position, LaunchRotation);
        }
    }
    //NEW-------------------------------------------------------------------------------------------------------------------
    private void FireShotgun()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + 1f;
            for (int i = 0; i < 6; i++)
            {
                ExperimentalLaunchRotation.eulerAngles = new Vector3(LaunchRotation.eulerAngles.x, LaunchRotation.eulerAngles.y - 7.5f + 3f * i, LaunchRotation.eulerAngles.z);
                Instantiate(RepairPart, PartLauncher.position, ExperimentalLaunchRotation);
            }
        }
    }

    private void FireExploder()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + 1f;
            for(int i=0; i<8; i++)
            {
                ExperimentalLaunchRotation.eulerAngles = new Vector3(LaunchRotation.eulerAngles.x, LaunchRotation.eulerAngles.y + 45f * i, LaunchRotation.eulerAngles.z);
                Instantiate(ExploderPart, PartLauncher.position, ExperimentalLaunchRotation);
            }
        }
    }

    private void FirePaddleBall()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + .5f;
            Instantiate(PBPart, PartLauncher.position, LaunchRotation);
        }
    }

    private void FireDeathRay()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + 5f;
            if(LaunchRotation.eulerAngles.y==0)
                Instantiate(DeathRayPart, new Vector3(PartLauncher.position.x, PartLauncher.position.y + 50, PartLauncher.position.z + 30f), LaunchRotation);
            else if(LaunchRotation.eulerAngles.y == 90)
                Instantiate(DeathRayPart, new Vector3(PartLauncher.position.x+30, PartLauncher.position.y + 50f, PartLauncher.position.z), LaunchRotation);
            else if(LaunchRotation.eulerAngles.y ==180)
                Instantiate(DeathRayPart, new Vector3(PartLauncher.position.x, PartLauncher.position.y + 50f, PartLauncher.position.z - 30f), LaunchRotation);
            else
                Instantiate(DeathRayPart, new Vector3(PartLauncher.position.x - 30f, PartLauncher.position.y + 50f, PartLauncher.position.z), LaunchRotation);




        }
    }

    private void FireFBShot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + .25f;
            Instantiate(RepairPart, PartLauncher.position, LaunchRotation);
            ExperimentalLaunchRotation.eulerAngles = new Vector3(LaunchRotation.eulerAngles.x, LaunchRotation.eulerAngles.y +180f, LaunchRotation.eulerAngles.z);
            Instantiate(RepairPart, PartLauncher.position, ExperimentalLaunchRotation);
        }
    }

    private void FireLandMine()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + 1f;
            Instantiate(LandMine, PartLauncher.position, PartLauncher.rotation);
        }
    }

    private void FireModeChooser(int choice)
    {
        switch (ChosenFM[choice])
        {
            case 0:
                FireBasicShot();
                break;

            case 1:
                FireTrippleShot();
                break;

            case 2:
                FireShotgun();
                break;

            case 3:
                FireExploder();
                break;

            case 4:
                FirePaddleBall();
                break;

            case 5:
                FireDeathRay();
                break;

            case 6:
                FireLandMine();
                break;

            case 7:
                FireBigShot();
                break;

            case 8:
                FireFBShot();
                break;

            case 9:
                FireRapidShot();
                break;
        }
    }

    private IEnumerator TrippleShot() // Tripple shot code@@@@@@@@@
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(RepairPart, PartLauncher.position, LaunchRotation);
            yield return new WaitForSeconds(.04f);
        }
    }

    private IEnumerator BigShot()
    {
        Instantiate(BigRepairPart, PartLauncher.position, LaunchRotation);
        yield return new WaitForSeconds(.08f);
    }

    private void Move()
    {



        if (moveRight == true && (transform.position.x + distance) < (lowerX + maxD))
        {
            transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
            lastMove = 1;
            Face.eulerAngles = new Vector3(0f, 90f, 0f);
            transform.rotation = Face;
        }

        if (moveLeft == true && (transform.position.x - distance) > (lowerX))
        {
            transform.position = new Vector3(transform.position.x - distance, transform.position.y, transform.position.z);
            lastMove = 2;
            Face.eulerAngles = new Vector3(0f, 270f, 0f);
            transform.rotation = Face;
        }

        if (moveForward == true && (transform.position.z + distance) < (lowerZ + maxD))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + distance);
            lastMove = 3;
            Face.eulerAngles = new Vector3(0f, 0f, 0f);
            transform.rotation = Face;
        }

        if (moveBack == true && (transform.position.z - distance) > (lowerZ))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - distance);
            lastMove = 4;
            Face.eulerAngles = new Vector3(0f, 180f, 0f);
            transform.rotation = Face;
        }


    }

    //FixLater
    private IEnumerator EndMessage(int outcome)
    {
        if (outcome == 1)
        {
            Var.VarArray[8, RobotNumber] = 0;
            print(RobotNumber);
        }

        while (MessageCounter < 9)
        {
            if (MessageCounter % 2 == 0)
            {
                if (outcome == 0)
                    Message.text = "Repair Unsuccessful \nContinue(C) or Quit(Q)";
                else
                    Message.text = "Repair Successful";

            }
            else
                Message.text = "";

            if (MessageCounter > 7 && outcome == 1)
                SceneManager.LoadScene("Project");

            MessageCounter++;


            yield return new WaitForSeconds(.5f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WAttack" || other.tag == "RAttack")
        {
            Health--;
            if (Health > 0)
                Instantiate(Explosion, other.transform.position, other.transform.rotation);

            Destroy(other.gameObject);
        }
        else if (other.tag != "Part" && other.tag != "LargePart" && Health > 0)
        {
            Health--;
            switch (lastMove)
            {
                case 1:
                    if (transform.position.x - distance > lowerX)
                        transform.position = new Vector3(transform.position.x - distance, transform.position.y, transform.position.z);
                    break;
                case 2:
                    if (transform.position.x + distance < lowerX + maxD)
                        transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
                    break;
                case 3:
                    if (transform.position.z - distance > lowerZ)
                        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - distance);
                    break;
                case 4:
                    if (transform.position.z + distance < lowerZ + maxD)
                        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + distance);
                    break;
            }
        }


    }
}