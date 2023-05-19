using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    ParticleSystem partciles;
    public AudioClip sound;

    private void Start()
    {
        partciles = GetComponent<ParticleSystem>();
        if (sound != null)
        {
            SoundManager.instance.PlaySound(sound);
        }
    }

    private void Update()
    {
        if (!partciles.isEmitting)
            Destroy(gameObject);
    }
}
