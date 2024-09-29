using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    // Variável estática para verificar se o jogo está pausado
    public static bool GameIsPaused = false;

    // Referências para os diferentes menus da UI
    public GameObject pauseMenuUI;
    public GameObject configurationsUI;
    public GameObject bestiaryUI;

    // Método para retomar o jogo
    public void Resume()
    {
        // Desativa todos os menus
        pauseMenuUI.SetActive(false);
        configurationsUI.SetActive(false);
        bestiaryUI.SetActive(false);

        // Retoma o tempo do jogo
        Time.timeScale = 1f;
        GameIsPaused = false;

        // Desativa o objeto do script
        gameObject.SetActive(false);
    }
    public void GameOver(){
        // Retorna para a Scene inicial
        SceneManager.LoadScene("Proficiencia");
    }
    // Método para pausar o jogo
    public void Pause()
    {
        // Ativa o menu de pausa
        pauseMenuUI.SetActive(true);

        // Ativa o objeto do script
        gameObject.SetActive(true);

        // Pausa o tempo do jogo
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    // Método para abrir a UI de configurações
    public void OpenConfigurationsUI()
    {
        // Desativa o menu de pausa e ativa o menu de configurações
        pauseMenuUI.SetActive(false);
        configurationsUI.SetActive(true);
    }

    // Método para abrir a UI do bestiário
    public void OpenBestiaryUI()
    {
        // Desativa o menu de pausa e ativa o menu do bestiário
        pauseMenuUI.SetActive(false);
        bestiaryUI.SetActive(true);
    }

    // Método para salvar o jogo
    public void SaveGame()
    {
        // Implementar lógica de salvar jogo
    }

    // Método para sair do jogo
    public void QuitGame()
    {
        Application.Quit();
    }
}
