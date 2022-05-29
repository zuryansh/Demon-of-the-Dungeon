using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool isPaused;
    public static GameManager instance;
    [SerializeField]int currentLevelNo = 1;

    private void Awake()
    {
        
        instance = this;
        if (PlayerPrefs.GetInt("FloorNo") <= 1)
        {
            PlayerPrefs.SetInt("PlayerHealth", 50);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        isPaused = (Time.timeScale == 0);
        
    }

    //public int GetLevelNo()
    //{
    //    Debug.Log("LEVEL NUMBEr--" + levelNo);
    //    Debug.Log("TIME==" + Time.realtimeSinceStartup);
    //    return levelNo;
    //}

    public void GoToScene(string scene)
    {
        SceneManager.LoadScene(scene);
        Utilities.instance.ResetVariables();
        if(scene == "Menu")
        {
            FindObjectOfType<FloorManager>().floorNo = 1;

        }
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
            Utilities.instance.pauseScreen.SetActive(true);
            //isPaused = true;
            Debug.Log("PAUSE");
        }
        
        
    }

    public void UnpauseGame()
    {
        
        
            //unpause game
            Time.timeScale = 1;
            Utilities.instance.pauseScreen.SetActive(false);
            Debug.Log("UNPAUSE");
            //isPaused = false;
        
    }

    public static void RestartGame()
    {
        PlayerPrefs.SetInt("PlayerInt", 50);
        FindObjectOfType<FloorManager>().floorNo = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public IEnumerator LevelWon()
    {
        Debug.Log("PLAYER WON");
        //SetLevelNo();
        // Set variables
        PlayerPrefs.SetInt("PlayerHealth", Utilities.instance.player.health);
        FindObjectOfType<FloorManager>().floorNo++;
        yield return new WaitForSeconds(3f);
        //restart scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }


}
