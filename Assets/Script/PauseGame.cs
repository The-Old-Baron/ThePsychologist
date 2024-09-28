using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject ConfigurationsUI;
    public GameObject BestiaryUI;

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        ConfigurationsUI.SetActive(false);
        BestiaryUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        gameObject.SetActive(false);
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void ConfigurationsUIOpen()
    {
        pauseMenuUI.SetActive(false);
        ConfigurationsUI.SetActive(true);
    }

    public void BestiaryUIOpen()
    {
        pauseMenuUI.SetActive(false);
        BestiaryUI.SetActive(true);
    }

    public void SaveGame()
    {
        //Save game
    }

    public void Sair()
    {
        Application.Quit();
    }
}
