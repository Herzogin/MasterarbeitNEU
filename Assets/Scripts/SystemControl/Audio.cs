using UnityEngine;


//Source: https://www.youtube.com/watch?v=QL29aTa7J5Q last accessed on 29.09.2021.
[System.Serializable]
public class Audio
{
    public AudioClip clip;
    public string name;

    [Range(0f, 1f)]
    public float volume;

    [HideInInspector]
    public AudioSource source;

    public float getVolume()
    {
        return volume;
    }

   public void setVolume(float newVolume)
    {
        volume = newVolume;
    }
}
