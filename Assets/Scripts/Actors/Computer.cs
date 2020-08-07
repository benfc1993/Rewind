using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
	bool hasExploded = false;
	public void Explode(ParticleSystem DeathEffect)
	{
		if(!hasExploded)
		{
			hasExploded = true;
			StartCoroutine(Death(DeathEffect));
		}
	}
	IEnumerator Death(ParticleSystem DeathEffect)
	{
		float delay = Random.Range(0,1.5f);
		yield return new WaitForSeconds(0);
		GetComponent<AudioSource>().Play();
		Destroy(Instantiate(DeathEffect.gameObject, transform.position, transform.rotation) as GameObject, DeathEffect.main.startLifetimeMultiplier);
		GetComponent<MeshRenderer>().enabled = false;
	}
}
