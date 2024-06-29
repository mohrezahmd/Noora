using System;
using System.Collections;
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
        Sound s = Array.Find(musicClipFiles.clips, x => x.name == songName);

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
        else if(sfxSource.pitch < 1.5f)
        {
            sfxSource.PlayOneShot(s.clip);
        }else if(sfxSource.pitch >= 1.5)
        {
            sfxSource.pitch = 1.5f;
        }
    }

    public void PlaySFX(string sFXName, float pitch)
    {
        Sound s = Array.Find(sfxClipFiles.clips, x => x.name == sFXName);

        if (s == null)
        {
            Debug.Log("sfx not found");
        }
        else if (sfxSource.pitch < .2f)

        {
            float tmpPitch = sfxSource.pitch;
            sfxSource.pitch = pitch;
            sfxSource.PlayOneShot(s.clip);

        }
        else if (sfxSource.pitch >= 1.5)
        {
            sfxSource.pitch = 1.5f;
        }
        Debug.Log("1.pitch2: " + sfxSource.pitch);
    }

    public void activateAudioObject(bool isActive)
    {
       musicSource.gameObject.SetActive(isActive);
       sfxSource.gameObject.SetActive(isActive);
    }

    public IEnumerator FadeOut(float FadeTime)
    {
         
        float startVolume = sfxSource.volume;

        while (sfxSource.volume > 0)
        {
            sfxSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        sfxSource.Stop();
        sfxSource.volume = startVolume;
    }
}
