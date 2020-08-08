using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHalt : MonoBehaviour
{
	public float secondsToWait;
	private void Start()
	{
		StartCoroutine(WaitForEnd());
	}

	IEnumerator WaitForEnd()
	{
		yield return new WaitForSeconds(secondsToWait);
		Destroy(gameObject);
	}
}
