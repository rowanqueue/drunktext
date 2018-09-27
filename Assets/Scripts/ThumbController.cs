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
	public bool leftPressing;
	
	public GameObject thumbR;
	private Rigidbody rrb;
	public Vector2 rightInput;
	public Vector2 rightXConstraints;
	public Vector2 rightYConstraints;
	public bool rightPressing;

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
		HandleInput();
	}

	void FixedUpdate()
	{
		lrb.AddForce(leftInput*mod);
		rrb.AddForce(rightInput*mod);
		DrunkenShaking(thumbL,lrb,leftXConstraints,leftYConstraints,0.0f);
		DrunkenShaking(thumbR,rrb,rightXConstraints,rightYConstraints,5.0f);
		if (leftPressing)
		{
			//LERP THAT FUCKER!!
		}
	}

	void HandleInput()
	{
		//left thumb
		float x = Input.GetAxis("HorizontalLeft");
		float y = Input.GetAxis("VerticalLeft");
		leftInput = new Vector2(x,y).normalized*0.05f;

		float lPress = Input.GetAxis("FireLeft");
		if (lPress > 0)
		{
			if (leftPressing == false)
			{
				leftPressing = true;
			}
		}
		
		//right thumb
		x = Input.GetAxis("HorizontalRight");
		y = Input.GetAxis("VerticalRight");
		rightInput = new Vector2(x, y).normalized * 0.05f;

		float rPress = Input.GetAxis("FireRight");
	}
	void DrunkenShaking(GameObject thumb,Rigidbody rb, Vector2 xConstraints,Vector2 yConstraints,float offset)
	{
		//CHANGE THIS SHIT TO FORCES
		Vector2 perlin = new Vector2(Mathf.PerlinNoise(Time.time*2f+offset, 0),Mathf.PerlinNoise(0,Time.time*2f+offset));
		if (perlin.x < 0.5f)
		{
			perlin.x = -perlin.x;
		}

		if (perlin.y < 0.5f)
		{
			perlin.y = -perlin.y;
		}
		rb.AddForce(perlin.normalized*2f);
		if (thumb.transform.position.x < xConstraints.x)//push to the right!
		{
			rb.AddForce(Vector2.right,ForceMode.Impulse);
		}
		else if (thumb.transform.position.x > xConstraints.y)//push to the left!
		{
			rb.AddForce(Vector2.left,ForceMode.Impulse);
		}
		if (thumb.transform.position.y < yConstraints.x)//push up
		{
			rb.AddForce(Vector2.up,ForceMode.Impulse);
		}
		else if (thumb.transform.position.y > yConstraints.y)//push down
		{
			rb.AddForce(Vector2.down,ForceMode.Impulse);
		}
		rb.velocity = Vector3.ClampMagnitude(rb.velocity, 0.75f);
	}
}
