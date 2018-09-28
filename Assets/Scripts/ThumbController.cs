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
	public float leftDuration;
	
	public GameObject thumbR;
	private Rigidbody rrb;
	public Vector2 rightInput;
	public Vector2 rightXConstraints;
	public Vector2 rightYConstraints;
	public bool rightPressing;

	public float mod;
	public float maxSpeed;
	public float regularZ;
	public float pressDepth;
	// Use this for initialization
	void Start ()
	{
		regularZ = thumbL.transform.position.z;
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
		//DrunkenShaking(thumbL,lrb,0.0f);
		//DrunkenShaking(thumbR,rrb,5.0f);
		Constrain(thumbL,lrb,leftXConstraints,leftYConstraints);
		Constrain(thumbR,rrb,rightXConstraints,rightYConstraints);
		
		if (leftPressing)
		{
			if (thumbL.transform.position.z < pressDepth + regularZ)
			{
				lrb.AddForce(new Vector3(0,0,1),ForceMode.Impulse);
			}
			else
			{
				lrb.AddForce(new Vector3(0,0,-1),ForceMode.Impulse);
				leftPressing = false;
			}
		}
		else
		{
			if (thumbL.transform.position.z <= regularZ)
			{
				lrb.velocity = new Vector3(lrb.velocity.x,lrb.velocity.y,0);
			}
		}
		if (rightPressing)
		{
			if (thumbR.transform.position.z < pressDepth + regularZ)
			{
				rrb.AddForce(new Vector3(0,0,1),ForceMode.Impulse);
			}
			else
			{
				rrb.AddForce(new Vector3(0,0,-1),ForceMode.Impulse);
				rightPressing = false;
			}
		}
		else
		{
			if (thumbR.transform.position.z <= regularZ)
			{
				rrb.velocity = new Vector3(rrb.velocity.x,rrb.velocity.y,0);
			}
		}
	}

	void HandleInput()
	{
		//left thumb
		float x = Input.GetAxis("HorizontalLeft");
		float y = Input.GetAxis("VerticalLeft");
		leftInput = new Vector2(x,y).normalized;

		//float lPress = Input.GetAxisRaw("FireLeft");
		if (Input.GetButtonDown("FireLeft"))
		{
			if (leftPressing == false)
			{
				leftPressing = true;
			}
		}
		
		//right thumb
		x = Input.GetAxis("HorizontalRight");
		y = Input.GetAxis("VerticalRight");
		rightInput = new Vector2(x, y).normalized;

		if (Input.GetButtonDown("FireRight"))
		{
			if (rightPressing == false)
			{
				rightPressing = true;
			}
		}
	}

	void Constrain(GameObject thumb, Rigidbody rb, Vector2 xConstraints, Vector2 yConstraints)
	{
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
		rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
	}
	void DrunkenShaking(GameObject thumb,Rigidbody rb,float offset)
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
		rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
	}
}
