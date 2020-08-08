using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour
{
	GameManager gameManager;
	private void Start()
	{
		gameManager = FindObjectOfType<GameManager>();
	}

	public void Back()
	{
		GetComponent<CanvasGroup>().alpha = 0;
		GetComponent<CanvasGroup>().interactable = false;
		GetComponent<CanvasGroup>().blocksRaycasts = false;

	}

	public void LoadLevelOne()
	{
		gameManager.LoadLevel(1);
	}
	public void LoadLevelTwo()
	{
		gameManager.LoadLevel(2);
	}
	public void LoadLevelThree()
	{
		gameManager.LoadLevel(3);
	}
	public void LoadLevelFour()
	{
		gameManager.LoadLevel(4);
	}
	public void LoadLevelFive()
	{
		gameManager.LoadLevel(5);
	}
	public void LoadLevelSix()
	{
		gameManager.LoadLevel(6);
	}
}
