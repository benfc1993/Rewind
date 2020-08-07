using UnityEngine.SceneManagement;
using UnityEngine;

public class Elevator : MonoBehaviour
{
	public bool levelEnd;
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player" && levelEnd)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}
}
