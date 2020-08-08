using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	public bool open = false;
	public CanvasGroup canvasGroup;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !open)
		{
			open = true;
			//GetComponent<Animator>().SetBool("Open", true);
			canvasGroup.alpha = 1;
			canvasGroup.interactable = true;
			canvasGroup.blocksRaycasts = true;
			FindObjectOfType<AudioManager>().Pause();
			Time.timeScale = 0f;
			Cursor.visible = true;
		} else if (Input.GetKeyDown(KeyCode.Escape) && open)
		{
			CloseWindow();
		}
	}

	void CloseWindow()
	{
		open = false;
		//GetComponent<Animator>().SetBool("Open", false);
		FindObjectOfType<AudioManager>().Resume();
		canvasGroup.alpha = 0;
		Time.timeScale = 1f;
		Cursor.visible = false;
		canvasGroup.interactable = false;
		canvasGroup.blocksRaycasts = false;
	}
	public void Resume()
	{
		CloseWindow();
	}

	public void Quit()
	{
		FindObjectOfType<GameManager>().MainMenu();
	}

}
