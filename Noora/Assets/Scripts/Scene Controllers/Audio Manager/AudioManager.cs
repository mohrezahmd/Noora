using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

   // Sound[] musicSounds, sfxSounds;
    [SerializeField] AudioSource musicSource, sfxSource;
    [SerializeField] AudioClipFiles musicClipFiles, sfxClipFiles;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
            instance.musicSource.Play();
            instance.sfxSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string songName)
    {
        Sound s = Array.Find<Sound>(musicClipFiles.clips, x => x.name == songName);

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
        s = musicClipFiles.clips[songIndex];

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
        Sound s = Array.Find(sfxClipFiles.clips, x => x.name == sFXName);

        if(s == null)
        {
            Debug.Log("sfx not found");
        }
        else if(sfxSource.pitch < 1.5)
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void PlaySFX(string sFXName, float pitch)
    {
        Sound s = Array.Find(sfxClipFiles.clips, x => x.name == sFXName);

        if (s == null)
        {
            Debug.Log("sfx not found");
        }
        else if (sfxSource.pitch < 1.5)

        {
            float tmpPitch = sfxSource.pitch;
            sfxSource.pitch = pitch;
            sfxSource.PlayOneShot(s.clip);

        }
    }

    public void activateAudioObject(bool isActive)
    {
       musicSource.gameObject.SetActive(isActive);
       sfxSource.gameObject.SetActive(isActive);
    }

}
