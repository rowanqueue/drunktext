using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHold : MonoBehaviour {

	public char key;
	public float delay;
	private float lastPress;
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Thumb" && Time.time > lastPress+delay)
		{
			Writer.me.TapKey(key);
			lastPress = Time.time;
		}
	}
}
