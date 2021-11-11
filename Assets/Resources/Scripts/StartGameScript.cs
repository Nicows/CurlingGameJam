using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    public Timer timerScript;
    public PlayerMovements playerMovements;

    public TMP_Text textHighScore;
    public GameObject panelEndGame;
    public GameObject panelPause;
    public CibleCalculator cibleCalculator;


    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        playerMovements.resetPosition();
        timerScript.StartCountdown();
    }
    public void RestartGame()
    {
        // panelEndGame.SetActive(false);
        // panelPause.SetActive(false);
        // timerScript.StartCountdown();
        // playerMovements.resetPosition();
        // rocksInteractions.RestartInstatiateRocks();

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }
    public void Resume()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Pause(){
        panelPause.SetActive(true);
        Time.timeScale = 0f;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void DisplayEndGame()
    {
        cibleCalculator.CalculateScore();
        textHighScore.text = cibleCalculator.GetScore().ToString();
        panelEndGame.SetActive(true);
        Time.timeScale = 0f;
    }
}
