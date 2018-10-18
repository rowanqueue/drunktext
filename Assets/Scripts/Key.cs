using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
	public char key;

	void Awake()
	{
		TextMesh display = GetComponentInChildren<TextMesh>();
		display.text = "" + key;
		if (key == '.')
		{
			display.text = "^";
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Thumb")
		{
			Writer.me.TapKey(key);
			Debug.Log(key);
		}
	}
}
