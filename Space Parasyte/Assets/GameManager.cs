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
        isPaused = (Time.timeScale == 0);
        //Debug.Log(Time.timeScale);
    }

    public void GoToScene(string scene)
    {
        SceneManager.LoadScene(scene);
        Utilities.ResetVariables();
        
            UnpauseGame();
        
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
            //isPaused = true;
            Debug.Log("PAUSE");
        }
        
        
    }

    public void UnpauseGame()
    {
        
        
            //unpause game
            Time.timeScale = 1;
            Utilities.pauseScreen.SetActive(false);
            Debug.Log("UNPAUSE");
            //isPaused = false;
        
    }

    public static void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
}
