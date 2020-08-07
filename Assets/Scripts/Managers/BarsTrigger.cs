using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsTrigger : MonoBehaviour
{
	public Animator bars;
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			bars.SetTrigger("Start");
		}
	}
}
