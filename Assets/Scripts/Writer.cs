using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Writer : MonoBehaviour
{
	public static Writer me;

	public string written;
	public List<string> archive;
	public Text display;
	public Text red;
	public Text blue;
	// Use this for initialization
	void Awake ()
	{
		me = this;
	}
	
	// Update is called once per frame
	void Update ()
	{
		display.text = written;
		
	}

	public void Submit()
	{
		archive.Add(written);
		written = "";
	}
	public void TapKey(char c)
	{
		switch(c)
		{
			case '.'://means enter
				Submit();
				break;
			case '\''://means backspace
				written = written.Substring(0, written.Length - 1);
				break;
			default:
				written += c;
				break;
		}
	}
}
