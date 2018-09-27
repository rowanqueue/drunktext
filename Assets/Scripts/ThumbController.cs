using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

//usage: put it on a gamemanager
//intent: control them thumbs
public class ThumbController : MonoBehaviour
{
	public GameObject thumbL;
	private Rigidbody lrb;
	public Vector2 leftInput;
	public Vector2 leftXConstraints;
	public Vector2 leftYConstraints;
	public GameObject thumbR;
	Rigidbody rrb;
	public Vector2 rightInput;
	public Vector2 rightXConstraints;
	public Vector2 rightYConstraints;

	public float mod;
	// Use this for initialization
	void Start ()
	{
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
		rrb.AddForce(rightInput*mod);
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
		rightInput += new Vector2(x, y).normalized * 0.05f;
	}
	void DrunkenShaking()
	{
		//CHANGE THIS SHIT TO FORCES
		Vector2 perlin = new Vector2(Mathf.PerlinNoise(Time.time*2f, 0),Mathf.PerlinNoise(0,Time.time*2f));
		if (perlin.x < 0.5f)
		{
			perlin.x = -perlin.x;
		}

		if (perlin.y < 0.5f)
		{
			perlin.y = -perlin.y;
		}
		lrb.AddForce(perlin.normalized*2f);
		lrb.velocity = Vector3.ClampMagnitude(lrb.velocity, 0.75f);
		if (thumbL.transform.position.x < leftXConstraints.x)//push to the right!
		{
			lrb.AddForce(Vector2.right*0.25f,ForceMode.Impulse);
		}
		else if (thumbL.transform.position.x > leftXConstraints.y)//push to the left!
		{
			lrb.AddForce(Vector2.left*0.25f,ForceMode.Impulse);
		}
		if (thumbL.transform.position.y < leftYConstraints.x)//push up
		{
			lrb.AddForce(Vector2.up*0.25f,ForceMode.Impulse);
		}
		else if (thumbL.transform.position.y > leftYConstraints.y)//push down
		{
			lrb.AddForce(Vector2.down*0.25f,ForceMode.Impulse);
		}
		
		perlin = new Vector2(Mathf.PerlinNoise(Time.time, 0),Mathf.PerlinNoise(0,Time.time));
		if (perlin.x < 0.5f)
		{
			perlin.x = -perlin.x;
		}

		if (perlin.y < 0.5f)
		{
			perlin.y = -perlin.y;
		}
		rrb.AddForce(perlin.normalized*2f);
		rrb.velocity = Vector3.ClampMagnitude(rrb.velocity, 0.75f);
		if (thumbR.transform.position.x < rightXConstraints.x)//push to the right!
		{
			Debug.Log("h");
			rrb.AddForce(Vector2.right*0.25f,ForceMode.Impulse);
		}
		else if (thumbR.transform.position.x > rightXConstraints.y)//push to the left!
		{
			rrb.AddForce(Vector2.left*0.25f,ForceMode.Impulse);
		}
		if (thumbR.transform.position.y < rightYConstraints.x)//push up
		{
			rrb.AddForce(Vector2.up*0.25f,ForceMode.Impulse);
		}
		else if (thumbR.transform.position.y > rightYConstraints.y)//push down
		{
			rrb.AddForce(Vector2.down*0.25f,ForceMode.Impulse);
		}
		
	}
}
