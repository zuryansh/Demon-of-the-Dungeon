using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    float previousMasterVolume;
    public AudioSource player;


    public void ToggleMute(bool val)
    {

        if (val == false)
        {
            audioMixer.SetFloat("MasterVolume", 0);
        }
        else if(val == true)
        {

            audioMixer.SetFloat("MasterVolume", -80);
        }

    }

    public void SetMasterVolume(float val)
    {
        audioMixer.SetFloat("MasterVolume", val);
    }

    public void SetFullscreenMode(bool val)
    {
        // DO SOMETHING
        if (val == true)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }

    }
}
