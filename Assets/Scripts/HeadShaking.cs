using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeadShaking : MonoBehaviour
{
	private Vector3 constantPos;
	public float mod;
	private Walker walker;
	private Vector3 originalPos;
	public Transform loseCameraEndPos;
	public Transform winCameraEndPos;
	private float durationMove;
	private float duration;
	
	
	private bool knocked;

	public MeshRenderer cover;
	// Use this for initialization
	void Start ()
	{
		constantPos = transform.position;
		walker = GameObject.FindObjectOfType<Walker>();
		originalPos = transform.position;
		cover.material.color = new Color(0, 0, 0, 0f);
		cover.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update ()
	{
		float xChange = Mathf.PerlinNoise(Time.time + 5f, 0);
		xChange = (xChange * 2f) - 1f;


		if (Writer.me.sheKnows)
		{
			transform.position = Vector3.Lerp(originalPos, loseCameraEndPos.position, durationMove);
			durationMove += Time.deltaTime*1.25f;
			if (durationMove > 1.2f)
			{
				if (Input.anyKey)
				{
					SceneManager.LoadScene(0);
				}
			}
		}else if (Writer.me.win)
		{
			transform.position = Vector3.Lerp(originalPos, winCameraEndPos.position, durationMove);
			durationMove += Time.deltaTime * 1.25f;
			if (durationMove > 1.2f)
			{
				Walker.me.mod = 0;
				if (Input.anyKey)
				{
					SceneManager.LoadScene(0);
				}
			}
		}
		else
		{
			float walkerMod = 1.0f;//yourhead shakes less when you're not walking
			if (knocked)
			{
				walkerMod *= 3f;
			}
			transform.position = constantPos + new Vector3(xChange, 0, 0)*mod*walkerMod;
		}

		if (knocked)
		{
			duration += Time.deltaTime*2f;
			if (duration < 1)//fade in
			{
				cover.material.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), duration+0.2f);
			}
			else//fade out
			{
				cover.material.color = Color.Lerp(new Color(0, 0, 0, 1), new Color(0, 0, 0, 0), duration-1-0.4f);
			}

			if (duration > 2.4f)
			{
				knocked = false;
			}
		}
	}

	public void KnockOut()
	{
		mod += mod* 0.1f;
		duration = 0;
		knocked = true;
	}
}
