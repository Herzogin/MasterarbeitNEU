using System;
using UnityEngine;

//Source: https://www.youtube.com/watch?v=QL29aTa7J5Q Abgerufen zuletzt 29.09.2021.
public class AudioManager : MonoBehaviour
{
    public Audio[] audios;

    void Awake()
    {
        foreach (Audio a in audios)
        {
            a.source = gameObject.AddComponent<AudioSource>();
            a.source.clip = a.clip;
            a.source.volume = a.volume;
        }
    }

    public void PlayAudio(string name)
    {
        Audio a = Array.Find(audios, audio => audio.name == name);
        a.source.Play();
    }

    public void PauseAudio(string name)
    {
        Audio a = Array.Find(audios, audio => audio.name == name);
        a.source.Pause();
    }

    public void UnPauseAudio(string name)
    {
        Audio a = Array.Find(audios, audio => audio.name == name);
        a.source.UnPause();
    }

    public void LoopAudio(string name)
    {
        Audio a = Array.Find(audios, audio => audio.name == name);
        a.source.Play();
        a.source.loop = true;
    }

    public void Volume(string name)
    {
        Audio a = Array.Find(audios, audio => audio.name == name);
    }
}
