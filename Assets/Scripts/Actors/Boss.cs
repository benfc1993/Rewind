using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Boss : MonoBehaviour
{
	public ParticleSystem DeathEffect;
	public GameObject[] computers;
	public Light[] lights;
	public Slider slider;

	private void Start()
	{
		computers = GameObject.FindGameObjectsWithTag("Computer");
	}
	private void OnTriggerEnter(Collider other)
	{
		print("collision");
		if(other.tag == "Bullet")
		{
			StartCoroutine(Explode());
		}
	}
	IEnumerator Explode()
	{
		yield return new WaitForSeconds(0.1f);
		slider.value = 0;
		foreach (GameObject c in computers)
		{
			print(c);
			c.GetComponent<Computer>().Explode(DeathEffect);
		}
		GetComponent<AudioSource>().Play();
		Destroy(Instantiate(DeathEffect.gameObject, transform.position, transform.rotation) as GameObject, DeathEffect.main.startLifetimeMultiplier);
		GetComponent<MeshRenderer>().enabled = false;
		foreach (Light l in lights)
		{
			l.gameObject.SetActive(true);
		}
		StartCoroutine(Fade());
		StartCoroutine(Finish());
	}

	IEnumerator Fade()
	{
		float duration = 3f;
		float interval = 0.05f;

		while( duration > 0)
		{
			foreach (Light l in lights)
			{
				l.intensity = l.intensity * (duration / 3f);
			}
			duration -= interval;
		yield return new WaitForSeconds(0.01f);
		}
	}

	IEnumerator Finish()
	{
		yield return new WaitForSeconds(0.7f);
		Time.timeScale = 1f;
		StartCoroutine(ChangeScene());
	}

	IEnumerator ChangeScene()
	{
		yield return new WaitForSeconds(1.5f);
		FindObjectOfType<GameManager>().NextScene();
	}
}
