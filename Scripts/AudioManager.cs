using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource enemyHit;

    private void Start()
    {
        enemyHit = GetComponent<AudioSource>();
    }

    public void EnemyHit()
    {
        enemyHit.Play();
    }
}
