using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTime : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Bullet")
		{
			Time.timeScale = 0.2f;
		}
	}
}
