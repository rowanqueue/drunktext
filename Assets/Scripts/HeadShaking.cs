using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShaking : MonoBehaviour
{
	private Vector3 constantPos;

	public float mod;

	private Walker walker;
	// Use this for initialization
	void Start ()
	{
		constantPos = transform.position;
		walker = GameObject.FindObjectOfType<Walker>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		float xChange = Mathf.PerlinNoise(Time.time + 5f, 0);
		xChange = (xChange * 2f) - 1f;


		float walkerMod = 1.0f;//yourhead shakes less when you're not walking
		if (walker.moving == false)
		{
			walkerMod = 0.25f;
		}
		transform.position = constantPos + new Vector3(xChange, 0, 0)*mod*walkerMod;
	}
}
