using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public Animator transition;
    public static Transition instance;
    public AudioSource player;

    private void Start()
    {
        instance = this;
    }

    public void GoToScene(string scene)
    {
        Debug.Log("HERE");
        transition.SetTrigger("Close");

        StartCoroutine(GameManager.instance.GoToScene(scene));

    }
}
