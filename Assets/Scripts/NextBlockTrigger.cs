using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBlockTrigger : MonoBehaviour
{
	public float blockLength;//put next block twice this
	private Transform parent;
	private bool finished;

	void Start()
	{
		parent = transform.parent;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (finished == false)
		{
			GameObject block = Instantiate(Resources.Load("Block"), parent.position + new Vector3(0, 0, blockLength * 2f), parent.parent.rotation,
				transform.parent.parent) as GameObject;
			block.transform.rotation = parent.parent.rotation;
			block.transform.localPosition = new Vector3(0,0,parent.localPosition.z+blockLength);
			finished = true;
			if (parent.localPosition.z > 295)
			{
				Destroy(parent.parent.GetChild(0).gameObject);	
			}
		}
	}
}
