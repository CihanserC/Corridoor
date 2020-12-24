using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEditorInternal;

public class GameManager : MonoBehaviour
{

    public GameObject finishline;
    private bool isPaused = false;
    public GameObject PauseMenu;
    public GameObject TapToPlayMenu;
    public GameObject DrawCntrl;
    //public GameObject WinMenu;

    private int CurrentSceneIndex = 0;
    private int SceneToContinue;

    private void Start()
    {
        LoadScene();
        DrawCntrl = GameObject.Find("Grid");
        DrawCntrl.SetActive(false);

        Application.targetFrameRate = 60;
        TapToPlayMenu = GameObject.Find("TapToPlayMenu");
    }


    public void ExitGame()
    {
        Debug.Log("You quit!");
        Application.Quit();
        //CurrentSceneIndex = 0;
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TapToPlay() // For Tap to Play button
    {
        Time.timeScale = 1;
        TapToPlayMenu.SetActive(false);
        DrawCntrl.SetActive(true);
        LoadScene();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GotoMenu()
    {
        Time.timeScale = 1;
        //Debug.Log("Go to main menu!");
        SceneManager.LoadScene("Menu");
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        if (isPaused == true)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0; // Freeze the game
            //isPaused = false;
        }
        else if (isPaused == false)
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1; // Continue to the game
            //isPaused = true;
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SaveScene();
    }

    public void SaveScene() // Saves the last scene player has played
    {
        CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex +1;
        PlayerPrefs.SetInt("SavedScene", CurrentSceneIndex);
        Debug.Log("CurrentSceneIndex:"+CurrentSceneIndex);
    }

    public void LoadScene() //Continues the game from the last saved point.
    {
        SceneToContinue = PlayerPrefs.GetInt("SavedScene");
        // If already in the same level that supposed to be loaded. Do not load it!!!
        if (SceneToContinue > SceneManager.GetActiveScene().buildIndex)
        {
            if (SceneToContinue == 10)
            {
                PlayerPrefs.SetInt("SavedScene", 0);               
            }
            SceneManager.LoadScene(SceneToContinue);
        }
    }


}
