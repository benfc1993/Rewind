using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartCutscene ()
    {
        StartCoroutine(cutsceneManager());
        StartCoroutine(cutsceneEnd());

    }

    // Update is called once per frame
    IEnumerator cutsceneManager()
    {
        yield return new WaitForSeconds(5.1f);
        GetComponent<AudioSource>().Play();
    }

    IEnumerator cutsceneEnd()
    {
        yield return new WaitForSeconds(59);
        FindObjectOfType<GameManager>().NextScene();
    }
}
