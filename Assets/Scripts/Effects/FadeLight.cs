using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeLight : MonoBehaviour
{
    public float duration;
    Light lt;
    // Start is called before the first frame update
    void Start()
    {
        lt = GetComponent<Light>();
        StartCoroutine(fadeInAndOut(lt, duration));
    }

    // Update is called once per frame
    IEnumerator fadeInAndOut(Light lightToFade, float duration)
    {
        float minLuminosity = 0; // min intensity
        float maxLuminosity = 1; // max intensity

        float counter = 0f;

        //Set Values depending on if fadeIn or fadeOut
        float a, b;
            a = maxLuminosity;
            b = minLuminosity;
        while (counter < duration)
        {
            counter += Time.deltaTime;

            lightToFade.intensity = Mathf.Lerp(a, b, counter / duration);

            yield return null;
        }
    }
}
