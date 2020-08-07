using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Time.timeScale = 1f;
        DontDestroyOnLoad(gameObject);
        print("loaded");
    }

    public void StartLevel()
    {
        FindObjectOfType<LevelManager>().SwapLights();
        FindObjectOfType<AudioManager>().ChangeSong(0);
    }

    public void NextScene()
    {
        //Transition
        FindObjectOfType<Transition>().FadeOUt();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
