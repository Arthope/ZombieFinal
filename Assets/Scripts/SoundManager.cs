using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource Music;

    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");
    }

    public void SetMusicEnabled(bool value)
    {
        Music.enabled = value;
        if (!value)
        {
            PlayerPrefs.SetFloat("Volume", 0);
        }
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("Volume", value);

    }
}
