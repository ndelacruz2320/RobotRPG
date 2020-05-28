using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGRotation : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        transform.eulerAngles = new Vector3(0.0f, transform.position.x * transform.position.y, 0.0f);
	}
	
}
