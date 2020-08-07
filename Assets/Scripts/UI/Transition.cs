using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    // Start is called before the first frame update
    public void FadeOUt()
    {
        GetComponent<Animator>().SetTrigger("Start");
    }
}
