using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public Intro intro;
	public Light purpleLight;
	public Animator[] animators;
	public CanvasGroup levelSelect;

	private void Start()
	{
		FindObjectOfType<AudioManager>().Play("BaseSong");
		StartCoroutine(bulletSounds());
	}

	public void OpenLevelSelect()
	{
		levelSelect.alpha = 1;
		levelSelect.interactable = true;
		levelSelect.blocksRaycasts = true;
	}

	public void StartGame()
	{
		foreach(Animator a in animators)
		{
			a.SetTrigger("Start");
		}
		intro.StartCutscene();
		purpleLight.intensity = 12;
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

	public void Exit()
	{
		Application.Quit();
	}
}
