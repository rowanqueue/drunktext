using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtTrigger : MonoBehaviour
{
	public HeadShaking head;

	private bool alreadyHit;
	// Use this for initialization
	void Start ()
	{
		head = GameObject.FindObjectOfType<HeadShaking>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Knock" && alreadyHit == false)
		{
			alreadyHit = true;
			head.KnockOut();
			transform.parent.gameObject.SetActive(false);
		}
	}
}
