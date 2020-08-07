using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	public ParticleSystem DeathEffect;
	public GameObject[] computers;

	private void Start()
	{
		computers = GameObject.FindGameObjectsWithTag("Computer");
	}
	private void OnTriggerEnter(Collider other)
	{
		print("collision");
		if(other.tag == "Bullet")
		{
			foreach(GameObject c in computers)
			{
				print(c);
				c.GetComponent<Computer>().Explode(DeathEffect);
			}
			Time.timeScale = 0.5f;
			GetComponent<AudioSource>().Play();
			Destroy(Instantiate(DeathEffect.gameObject, transform.position, transform.rotation) as GameObject, DeathEffect.main.startLifetimeMultiplier);
			GetComponent<MeshRenderer>().enabled = false;
		}
	}
}
