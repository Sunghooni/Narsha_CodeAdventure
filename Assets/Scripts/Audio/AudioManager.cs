﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public AudioClip[] Sounds;

    public static AudioManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        AudioSource bgm;
        if (gameObject.GetComponent<AudioSource>())
        {
            bgm = gameObject.GetComponent<AudioSource>();
            bgm.volume = OptionValues.GetVolume();
        }
    }

    public void PlaySounds(string audioIndex)
    {
        AudioSource audioPlayer = StaticObject.GetInstance().GetComponent<AudioSource>();
        if(audioPlayer == null)
        {
            audioPlayer = StaticObject.GetInstance().gameObject.AddComponent<AudioSource>();
        }
        foreach(AudioClip audio in Sounds)
        {
            if(audio.name.Equals(audioIndex))
            {
                audioPlayer.clip = audio;
                audioPlayer.volume = OptionValues.GetVolume();
                audioPlayer.Play();
            }
        }
    }

    public static void VolumeSize(float size)
    {
        AudioSource audioPlayer = StaticObject.GetInstance().GetComponent<AudioSource>();
        if (audioPlayer == null)
        {
            audioPlayer = StaticObject.GetInstance().gameObject.AddComponent<AudioSource>();
        }
        audioPlayer.volume = size;
    }
}