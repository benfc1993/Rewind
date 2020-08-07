using UnityEngine.SceneManagement;
using UnityEngine;

public class Failed : MonoBehaviour
{
	public void retry()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void quit()
	{
		SceneManager.LoadScene("Home");
	}
}
