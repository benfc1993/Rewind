using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
	public ParticleSystem Pickup;
	public void die()
	{
		Destroy(Instantiate(Pickup, transform.position, Quaternion.identity), 0.5f);
		Destroy(gameObject);
	}
}
