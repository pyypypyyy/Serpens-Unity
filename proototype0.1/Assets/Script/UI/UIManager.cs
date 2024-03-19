using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI component")]
    public GameObject gameOverPanel;
    public GameObject gamePassPanel;
    public GameObject gameMenuPanel;

    public Button btnRestart;
    public Button btnContinue;
    public Button btnResume;
    public Button btnQuit;

    private bool isGamePaused = false;

    void Start()
    {
        btnRestart.onClick.AddListener(onRestartButtonClick);
        btnContinue.onClick.AddListener(onRestartButtonClick);
        btnResume.onClick.AddListener(ResumeGame);
        btnQuit.onClick.AddListener(ExitGame); 

        gameMenuPanel.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (EnemyManager.Instance.GetLastWave() && EnemyManager.Instance.enemyCount ==0)
        {
            gamePassPanel.SetActive(true);
        }
    }
    public void showGameOverPanel()
    {
        gameOverPanel.SetActive(true);
      
    }

    public void onRestartButtonClick()
    {
        SceneLoader.Instance.LoadMainScene();
    }

    public void onContinueButtonClick()
    {
        
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        gameMenuPanel.SetActive(true);
        isGamePaused = true;
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        gameMenuPanel.SetActive(false);
        isGamePaused = false;
    }

    
    void ExitGame()
    {
        Application.Quit(); 
    }
}




