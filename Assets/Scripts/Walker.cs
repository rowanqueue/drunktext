using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
	public float mod;

	public bool moving;
	// Use this for initialization
	void Start ()
	{
		moving = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.N))
		{
			moving = false;
		}

		if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.O))
		{
			moving = true;
		}
	}

	void FixedUpdate()
	{
		if (moving)
		{
			transform.position -= transform.forward * mod;	
		}
	}
}
