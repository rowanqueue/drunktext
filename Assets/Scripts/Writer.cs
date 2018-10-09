using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Writer : MonoBehaviour
{
	public static Writer me;

	public string written;
	public List<string> archive;
	public Text display;
	public Text yourNextLine;
	public Text[] log = new Text[6];
	private string[] logLines;
	public Transform can;
	private bool cursorDisplayed;
	private float cursorTime;


	public TextAsset texts;
	public List<string> momTexts;//what your mom says
	public int momInt;
	public List<string> yourTexts;//what youre supposed to say
	public int yourInt;
	
	// Use this for initialization
	void Awake ()
	{
		me = this;
		momTexts = new List<string>();
		yourTexts = new List<string>();
		ReadTexts();
		//momTexts.Add("Do you know where the dog's pills are?");
		//momTexts.Add("Really?");
		//momTexts.Add("I swear you were the last one to see them.");
		//yourTexts.Add("uh tbh idk");
		//yourTexts.Add("wow");
		logLines = new string[6];
		//log = new Text[6];
		//int i = 0;
		//Rect pos = new Rect(0,160+251,160,20);
		
		writeLog(momTexts[momInt]);
	}

	void ReadTexts()
	{
		string[] lines = texts.text.Split('\n');
		int i = 0;
		foreach (string s in lines)
		{
			if (i == 0){i++;continue;}
			//Debug.Log(s);
			if (s[0] == '@')
			{
				yourTexts.Add(s.Split(',')[1]);
			}
			else
			{
				momTexts.Add(s.Split(',')[1]);
			}
			i++;
		}
	}

	void writeLog(string text)
	{
		for (int i = logLines.Length - 1; i > 0; i--)
		{
			logLines[i] = logLines[i - 1];
		}

		logLines[0] = text;
		foreach (Text textDisplay in log)
		{
			textDisplay.text = "";
		}

		bool doubleText = false;
		for (int i = logLines.Length - 1; i >= 0; i--)
		{
			if (logLines[i] != null && logLines[i] != "")
			{
				if (logLines[i][0] == '@')//you sent it
				{
					log[i].alignment = TextAnchor.MiddleRight;
					if (logLines[i].Length == 1)
					{
						log[i].text = " ";
					}
					else
					{
						log[i].text = logLines[i].Substring(1);
					}
				}
				else//your mom did
				{
					if (logLines[i].Substring(logLines[i].Length-2).Trim().Equals(";"))//another text should be sent
					{
						logLines[i] = logLines[i].Substring(0, logLines[i].Length - 2);
						log[i].text = logLines[i];
						doubleText = true;
					}
					else
					{
						log[i].text = logLines[i];
					}
					log[i].alignment = TextAnchor.MiddleLeft;
				}	
			}
		}

		if (doubleText)
		{
			momInt++;
			writeLog(momTexts[momInt]);
		}
	}
	// Update is called once per frame
	void Update ()
	{
		if (Time.time > cursorTime)
		{
			cursorTime = Time.time + 0.5f;
			cursorDisplayed = !cursorDisplayed;
		}
		string displayText = written;
		if (cursorDisplayed)
		{
			displayText += "|";
		}
		display.text = displayText;
		yourNextLine.text = yourTexts[yourInt];
	}

	public void Submit()
	{
		written = "@" + written;
		archive.Add(written);
		writeLog(written);
		written = "";
		momInt++;
		writeLog(momTexts[momInt]);
		yourInt++;
	}
	public void TapKey(char c)
	{
		switch(c)
		{
			case '.'://means enter
				Submit();
				break;
			case '\''://means backspace
				if (written != "")
				{
					written = written.Substring(0, written.Length - 1);	
				}
				break;
			default:
				written += c;
				break;
		}
	}
}
