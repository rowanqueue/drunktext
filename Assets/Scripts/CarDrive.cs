using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDrive : MonoBehaviour
{

	public float mod;
	// Update is called once per frame
	void Awake()
	{
		transform.position -= transform.forward * (Random.Range(1f, 3f)*15f);
	}
	void Update ()
	{
		transform.position += transform.forward*mod;
	}
}
