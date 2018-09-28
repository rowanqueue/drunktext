using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
	public char key;
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Thumb")
		{
			Writer.me.TapKey(key);
		}
	}
}
