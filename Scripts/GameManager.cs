using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool isPaused;
    public static GameManager instance;
    public FloorManager floorManager;

    private void Awake()
    {
        
        instance = this;
        if (PlayerPrefs.GetInt("FloorNo",1) <= 1)
        {
        }
    }

    public void Start()
    {
        PlayerPrefs.SetInt("PlayerHealth", 50);
        PlayerPrefs.SetInt("FloorNo", 1);
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



    public IEnumerator GoToScene(string scene)
    {

        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(scene);

        if (Utilities.instance != null)
        {
            Utilities.instance.ResetVariables();
        }
        if(scene == "Menu")
        {
            PlayerPrefs.SetInt("FloorNo", 1);
            PlayerPrefs.SetInt("PlayerHealth", 50);
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
        if (Utilities.instance != null)
        {
            Utilities.instance.pauseScreen.SetActive(false);
        }
            Debug.Log("UNPAUSE");
            //isPaused = false;
        
    }

    public static void RestartGame()
    {
        PlayerPrefs.SetInt("PlayerInt", 50);
        PlayerPrefs.SetInt("FloorNo", 1);
        Transition.instance.GoToScene(SceneManager.GetActiveScene().name);
        
    }

    public IEnumerator LevelWon()
    {
        Debug.Log("PLAYER WON");
        //SetLevelNo();
        // Set variables
        PlayerPrefs.SetInt("PlayerHealth", Utilities.instance.player.health);
        PlayerPrefs.SetInt("FloorNo", (PlayerPrefs.GetInt("FloorNo", 0)+1) );
        yield return new WaitForSeconds(3f);
        //restart scene
        Transition.instance.GoToScene(SceneManager.GetActiveScene().name);


    }


}
