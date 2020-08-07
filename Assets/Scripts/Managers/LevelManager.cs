using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject houseLights;
    public GameObject colourLights;
    public Animator failed;
    public GameObject FailedScreen;

    public void SwapLights()
    {

        StartCoroutine(DimHouseLights());
    }
    IEnumerator DimHouseLights()
    {
        yield return new WaitForSeconds(1f);
        colourLights.SetActive(true);
        houseLights.SetActive(false);
    }
    public void levelFailed()
    {
        FailedScreen.SetActive(true);
        failed.SetTrigger("Start");
    }
}

