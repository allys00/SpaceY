using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        if (StateLevelController.currentLevelIndex == 0)
        {
            StateLevelController.currentLevelIndex = 1;
        }
        SceneManager.LoadScene(StateLevelController.currentLevelIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        StateLevelController.currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(0);
    }
}