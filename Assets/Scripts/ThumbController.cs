using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//usage: put it on a gamemanager
//intent: control them thumbs
public class ThumbController : MonoBehaviour
{
	public GameObject thumbL;
	private Rigidbody lrb;
	public Vector2 leftInput;
	public GameObject thumbR;
	Rigidbody rrb;
	public Vector2 rightConstant;

	public float mod;
	// Use this for initialization
	void Start ()
	{
		rightConstant = thumbR.transform.position;
		lrb = thumbL.GetComponent<Rigidbody>();
		rrb = thumbR.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Movement();
	}

	void FixedUpdate()
	{
		lrb.AddForce(leftInput*mod);
		DrunkenShaking();
	}

	void Movement()
	{
		//left thumb
		float x = Input.GetAxis("HorizontalLeft");
		float y = Input.GetAxis("VerticalLeft");
		leftInput = new Vector2(x,y).normalized*0.05f;
		
		//right thumb
		x = Input.GetAxis("HorizontalRight");
		y = Input.GetAxis("VerticalRight");
		rightConstant += new Vector2(x, y).normalized * 0.05f;
	}
	void DrunkenShaking()
	{
		//CHANGE THIS SHIT TO FORCES
		Vector2 perlin = new Vector2(Mathf.PerlinNoise(Time.time*2f, 0),Mathf.PerlinNoise(0,-Time.time*4f));
		if (perlin.x < 0.5f)
		{
			perlin.x = -perlin.x;
		}

		if (perlin.y < 0.5f)
		{
			perlin.y = -perlin.y;
		}
		lrb.AddForce(perlin.normalized*0.5f);
		lrb.velocity = Vector3.ClampMagnitude(lrb.velocity, 0.75f);
		//Debug.Log(lrb.velocity.magnitude);
		//thumbL.transform.position = leftConstant + new Vector2(Mathf.PerlinNoise(Time.time*3+10, Time.time*3+5),
		//	                            Mathf.PerlinNoise(-Time.time*2+1, Time.time*2+4))*0.5f;
		//thumbR.transform.position = rightConstant+ new Vector2(Mathf.PerlinNoise(Time.time*3+10, Time.time*3+5),
		//	                            Mathf.PerlinNoise(-Time.time*2+1, Time.time*2+4))*0.5f;
	}
}
