using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
//NON ACTIVE SCRIPT
//pause menu responsible for pausing all elements of the game when escape is pressed

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
		
	}
    public void Resume()
    {//method to set the game active again
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {//the game would be set to timescale 1 therefore nothing will be able to move
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {//loads main menu scene
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {//quits application when quit button pressed
        Application.Quit();
    }
}
