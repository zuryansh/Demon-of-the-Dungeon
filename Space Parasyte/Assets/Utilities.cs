using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;

public class Utilities : MonoBehaviour
{
    public static GameObject pauseScreen;
    public static Camera MainCam;
    public static PlayerMovement player;
    public GameObject blood;
    public  AudioSource enemyHit;
    public static Utilities instance;

    private void Start()
    {
        instance = this;
        enemyHit = GetComponent<AudioSource>();

        ResetVariables();
    }

    public static void ResetVariables()
    {
        pauseScreen = GameObject.FindGameObjectWithTag("PauseScreen");

        if(pauseScreen != null)
            pauseScreen.SetActive(false);

        MainCam = Camera.main;
        player = FindObjectOfType<PlayerMovement>();
        
    }

    public void EnemyHit()
    {
        if (!enemyHit.isPlaying)
        {
            enemyHit.Play();
        }
    }


}

