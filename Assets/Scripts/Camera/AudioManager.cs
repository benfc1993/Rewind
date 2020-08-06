using UnityEngine.Audio;
using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public string[] songs;
    bool switching = false;
    public static AudioManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Audio file not found");
            return;
        }
        s.source.Play();
    }

    public void ChangeSong(int toPlay)
    {
        switching = false;
        if(!Array.Find(sounds, sound => sound.name == songs[toPlay]).source.isPlaying) {
            foreach (string song in songs )
            {
                Sound s = Array.Find(sounds, sound => sound.name == song);
                s.source.Stop();
            }
            Sound c = Array.Find(sounds, sound => sound.name == "CassetteChange");
            c.source.Play();
            StartCoroutine(delaySong(1f));
            
        }
    }

    IEnumerator delaySong(float delay)
    {
        switching = true;
        if(switching)
        {
            yield return new WaitForSeconds(delay);
            Sound s = Array.Find(sounds, sound => sound.name == songs[FindObjectOfType<PlayerController>().currentEquipped]);
            s.source.Play();
            StopAllCoroutines();
        }
        yield return null;
    }

    public void Pause()
    {
        Sound s = Array.Find(sounds, sound => sound.name == songs[FindObjectOfType<PlayerController>().currentEquipped]);
        s.source.Pause();
    }

    public void Resume()
    {
        Sound s = Array.Find(sounds, sound => sound.name == songs[FindObjectOfType<PlayerController>().currentEquipped]);
        s.source.UnPause();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
