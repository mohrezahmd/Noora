using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            instance.musicSource.Play();
            instance.sfxSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
    }

    public void PlayMusic(string songName)
    {
        Sound s = Array.Find(musicSounds, x => x.name == songName);

        if(s == null)
        {
            Debug.Log("Song not found!");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlayMusic(int songIndex)
    {
        Sound s = new Sound();
        s = musicSounds[songIndex];

        if (s == null)
        {
            Debug.Log("Song not found!");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string sFXName)
    {
        sfxSource.pitch = 1;
        Sound s = Array.Find(sfxSounds, x => x.name == sFXName);

        if(s == null)
        {
            Debug.Log("sfx not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void PlaySFX(string sFXName, float pitch)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == sFXName);

        if (s == null)
        {
            Debug.Log("sfx not found");
        }
        else
        {
            float tmpPitch = sfxSource.pitch;
            sfxSource.pitch = pitch;
            sfxSource.PlayOneShot(s.clip);

        }
    }
}