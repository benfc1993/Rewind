using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHalt : MonoBehaviour
{
	private void Start()
	{
		StartCoroutine(WaitForEnd());
	}

	IEnumerator WaitForEnd()
	{
		yield return new WaitForSeconds(19);
		Destroy(gameObject);
	}
}
