using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    AudioManager audioManager;
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

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.disabled = true;
    }

    public void StartLevel()
    {
        FindObjectOfType<LevelManager>().SwapLights();
        audioManager.disabled = false;
        audioManager.ChangeSong(FindObjectOfType<PlayerController>().currentEquipped);
    }

    public void NextScene()
    {
        //Transition
        FindObjectOfType<Transition>().FadeOUt();
        audioManager.Pause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevel(int Level)
    {
        FindObjectOfType<Transition>().FadeOUt();
        audioManager.Pause();
        SceneManager.LoadScene(Level);
    }

    public void Restart()
    {
        FindObjectOfType<Transition>().FadeOUt();
        audioManager.Pause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        //Transition
        FindObjectOfType<Transition>().FadeOUt();
        audioManager.Pause();
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        //Close game
    }
}
