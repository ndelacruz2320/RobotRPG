using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraFB : MonoBehaviour
{


        public float lookSpeedH = 2f;
        public float lookSpeedV = 2f;
        public float zoomSpeed = 2f;
        public float dragSpeed = 6f;

        private float yaw = 0f;
        private float pitch = 0f;

    public GameObject player;
    public GameObject launcher;
    public GameObject follower;

        void Update()
        {
            //Look around with Right Mouse
           // if (Input.GetMouseButton(1))
            //{
                yaw += lookSpeedH * Input.GetAxis("Mouse X");
                pitch -= lookSpeedV * Input.GetAxis("Mouse Y");

                transform.eulerAngles = new Vector3(pitch, yaw, 0f);
                player.transform.eulerAngles = new Vector3(pitch, yaw, 0f);
                launcher.transform.eulerAngles = new Vector3(pitch, yaw, 0f);
                follower.transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        //}


        //Zoom in and out with Mouse Wheel
        transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, Space.Self);
        }
}

