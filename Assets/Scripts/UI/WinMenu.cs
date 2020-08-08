using UnityEngine.SceneManagement;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
	void Start()
	{
		Cursor.visible = true;
	}
	public void Leave()
	{
		SceneManager.LoadScene(0);
	}
}
