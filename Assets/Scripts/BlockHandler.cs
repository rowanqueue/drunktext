using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//handle when to spawn new blocks
//handle spawning obstacles on these blocks
public class BlockHandler : MonoBehaviour
{
	public GameObject[] prefabs;
	void Awake () {
		for (int i = 0; i < 2; i++)
		{
			GameObject obj = prefabs[Random.Range(0, prefabs.Length)];
			obj.SetActive(true);

		}
	}
}
