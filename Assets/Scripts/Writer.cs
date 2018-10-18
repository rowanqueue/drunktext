using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Writer : MonoBehaviour
{
	public static Writer me;

	public string written;
	public List<string> archive;
	public TextMesh display;
	public TextMesh yourNextLine;
	public TextMesh[] log = new TextMesh[6];
	private string[] logLines;
	public Transform can;
	private bool cursorDisplayed;
	private float cursorTime;

	public float doubt;
	public float loseAmount;
	public bool sheKnows;
	
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
		foreach (TextMesh textDisplay in log)
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
					//log[i].alignment = TextAnchor.MiddleRight;
					log[i].transform.localPosition = new Vector3(-2.65f, log[i].transform.localPosition.y,log[i].transform.localPosition.z);
					log[i].anchor = TextAnchor.UpperLeft;
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
					//log[i].alignment = TextAnchor.MiddleLeft;
					log[i].transform.localPosition = new Vector3(2.65f, log[i].transform.localPosition.y,log[i].transform.localPosition.z);
					log[i].anchor = TextAnchor.UpperRight;
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
		if (sheKnows)
		{
			yourNextLine.text = "";
		}
		else
		{
			yourNextLine.text = yourTexts[yourInt];	
		}
	}

	public void MomParse(string submit, string actualLine)
	{
		int numRight = 0;
		List<char> usedAlready = new List<char>();
		foreach (char c in submit)
		{
			if (actualLine.Contains(c.ToString()))
			{
				if (!usedAlready.Contains(c))
				{
					numRight++;
					usedAlready.Add(c);
				}
			}
		}

		if (numRight >= 2)
		{
			doubt += 0.05f;
		}
		else
		{
			doubt += 0.35f;
		}
	}
	public void Submit()
	{
		written = "@" + written;
		archive.Add(written);
		writeLog(written);
		written = "";
		MomParse(written,yourTexts[yourInt]);
		momInt++;
		if (doubt > loseAmount)//mom knows!!!!
		{
			writeLog("Wait... are you drunk?");
			sheKnows = true;
		}
		else
		{
			writeLog(momTexts[momInt]);
			yourInt++;
		}
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
			    if(written.Length < 19)
			    {
			        written +=c;
			    }
				break;
		}
	}
}
