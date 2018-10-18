using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//sounds!! put it on audiosource!!?!!
public class SoundS : MonoBehaviour
{
	private AudioSource sound;
	// Use this for initialization
	void Start ()
	{
		sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && sound.isPlaying == false)
		{
			sound.Play();
		}

		if (Input.GetKeyDown(KeyCode.B))
		{
			sound.PlayOneShot(sound.clip);
		}

		if (Input.GetKey(KeyCode.S))
		{
			if (sound.isPlaying == false)
			{
				sound.Play();
			}
		}
		else
		{
			sound.Stop();
		}
	}
}
