using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
	public static Walker me;
	public float mod;

	public Vector3 moveInput;//move the player to the left or right
	// Use this for initialization
	void Start ()
	{
		me = this;
		moveInput = Vector3.zero;
	}
	
	// Update is called once per frame
	/*void Update () {
		if (Input.GetKey(KeyCode.C))
		{
			moveInput = Vector3.left;
		}

		if (Input.GetKey(KeyCode.N))
		{
			moveInput = Vector3.right;
		}
	}*/

	void FixedUpdate()
	{
		
		transform.position -= transform.forward * mod;
		transform.position -= moveInput*mod;
		moveInput = Vector3.zero;
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9.45f, 11.8f), transform.position.y,
			transform.position.z);
	}
}
