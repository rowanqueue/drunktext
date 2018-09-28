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
	public Text[] log;
	private string[] logLines;
	public Transform can;


	public List<string> momTexts;//what your mom says
	public int momInt;
	public List<string> yourTexts;//what youre supposed to say
	// Use this for initialization
	void Awake ()
	{
		me = this;
		momTexts = new List<string>();
		yourTexts = new List<string>();
		ReadTexts();
		logLines = new string[6];
		log = new Text[6];
		int i = 0;
		Rect pos = new Rect(0,160+251,160,20);
		foreach (string line in logLines)
		{
			GameObject textObj = Instantiate(Resources.Load("Log"), can) as GameObject;
			Text text = textObj.GetComponent<Text>();
			text.rectTransform.position = new Vector2(446,pos.position.y+(i*20));
			log[i] = text;
			i++;
		}
		writeLog(momTexts[momInt]);
	}

	void ReadTexts()
	{
		string path = "Assets/Resources/";
		//string path = "TemplateData/Seeds/";
		path+="DrunkText - Texts.csv";
		//Debug.Log(path);
		StreamReader reader = new StreamReader(path);
		string[] lines = reader.ReadToEnd().Split('\n');
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
		display.text = written;
		
	}

	public void Submit()
	{
		written = "@" + written;
		archive.Add(written);
		writeLog(written);
		written = "";
		momInt++;
		writeLog(momTexts[momInt]);
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
