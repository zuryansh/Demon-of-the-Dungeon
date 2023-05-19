using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private void Start()
    {
        instance = this;
    }

    public void PlaySound(AudioClip sound)
    {
        AudioSource[] sources = GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            if (source.clip == sound)
            {

                if (!source.isPlaying)
                {
                    source.Play();
                }
            }
        }
        //Debug.Log("DOne");
    }
}
