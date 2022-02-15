using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool isPaused;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void GoToScene(string scene)
    {
        SceneManager.LoadScene(scene);
        Utilities.ResetVariables();
        if(Time.timeScale<1) // if the game is paused when we switch scenes unpause game
            PauseGame();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QUIT");
    }



    public void PauseGame()
    {
        if (!isPaused)
        {
            //pause game
            Time.timeScale = 0;
            Utilities.pauseScreen.SetActive(true);
            isPaused = true;
        }
        else if (isPaused)
        {
            //unpause game
            Time.timeScale = 1;
            Utilities.pauseScreen.SetActive(false);

            isPaused = false;
        }
    }

    public static void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
