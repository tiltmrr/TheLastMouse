using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject pauseOverlay;

    void Start()
    {
        pausePanel.SetActive(false);
    }

    public void OpenPause()
    {
        Debug.Log("Пауза открыта!");
        pausePanel.SetActive(true);
        if (pauseOverlay != null) pauseOverlay.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ClosePause()
    {
        pausePanel.SetActive(false);
        if (pauseOverlay != null) pauseOverlay.SetActive(false);
        Time.timeScale = 1f; // возобновить игру
    }

    public void OpenSettings()
    {
        // пока пусто, добавим позже
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}