using UnityEngine.SceneManagement;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
	public void Leave()
	{
		SceneManager.LoadScene(0);
	}
}
