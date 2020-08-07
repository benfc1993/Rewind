using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public Intro intro;
	public Light light;
	public Animator[] animators;

	private void Start()
	{
		FindObjectOfType<AudioManager>().Play("BaseSong");
		StartCoroutine(bulletSounds());
	}

	public void StartGame()
	{
		foreach(Animator a in animators)
		{
			a.SetTrigger("Start");
		}
		intro.StartCutscene();
		light.intensity = 12;
		StopAllCoroutines();
		FindObjectOfType<AudioManager>().Pause();
	}
	IEnumerator bulletSounds()
	{
		yield return new WaitForSeconds(0.3f);
		FindObjectOfType<AudioManager>().Play("PistolShot");
		StartCoroutine(bulletDelay());
	}
	IEnumerator bulletDelay()
	{
		yield return new WaitForSeconds(3.6f);
		StartCoroutine(bulletSounds());
	}
}
