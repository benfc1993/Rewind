using System.Collections;
using System.Collections.Generic;
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
    }

    public void StartLevel()
    {
        FindObjectOfType<LevelManager>().SwapLights();
        FindObjectOfType<AudioManager>().ChangeSong(0);
    }
}
