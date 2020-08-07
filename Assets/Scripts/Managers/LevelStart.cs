using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
	bool levelStarted;
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player" && !levelStarted)
		{
			levelStarted = true;
			FindObjectOfType<GameManager>().StartLevel();
		}
	}
}
