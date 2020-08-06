﻿using System.Collections;
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
        DontDestroyOnLoad(gameObject);
    }

            // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("BaseSong");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
