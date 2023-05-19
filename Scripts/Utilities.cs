using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

using UnityEngine.Audio;

public class Utilities : MonoBehaviour
{
    public  GameObject pauseScreen;
    public static Camera MainCam;
    public  Player player;
    public static PlayerMovement playerMovement;
    public GameObject blood;
    public  AudioSource enemyHit;
    public static Utilities instance;
    public static CinemachineVirtualCamera vCam;
    public  GameObject BossSkull;
    public  GameObject PopupText;
    public int roomBudget;


    private void Start()
    {
        instance = this;
        enemyHit = GetComponent<AudioSource>();


        ResetVariables();
    }

    public void ResetVariables()
    {
        //pauseScreen = GameObject.FindGameObjectWithTag("PauseScreen");

        if (pauseScreen != null)
        {
            pauseScreen.SetActive(false);
        }
        MainCam = Camera.main;
        vCam = FindObjectOfType<CinemachineVirtualCamera>();
        //player = FindObjectOfType<Player>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        
    }

    public void EnemyHit()  
    {
        if (!enemyHit.isPlaying)
        {
            enemyHit.Play();
        }
    }

    //public void PlaySound(AudioClip audio)
    //{

    //    AudioSource[] sources = GetComponents<AudioSource>();
    //    foreach (AudioSource source in sources)
    //    {
    //        if(source.clip != audio)
    //        {
    //            AudioSource player =gameObject.AddComponent<AudioSource>();
    //            if (player.isPlaying!)
    //            {
    //                player.Play();
                    
    //            }
    //        }
    //    }
    //    Debug.Log("DOne");
            
    //}

}

