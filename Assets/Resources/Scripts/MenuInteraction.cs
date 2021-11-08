using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuInteraction : MonoBehaviour
{
    public Timer timerScript;
    public PlayerMovements playerMovements;

    public TMP_Text textHighScore;
    public GameObject panelMenu;
    public GameObject panelEndGame;

    // Start is called before the first frame update
    void Start()
    {
        DisplayHighScore();
    }
    public void StartGame()
    {
        playerMovements.resetPosition();
        panelMenu.SetActive(false);
        timerScript.StartCountdown();
    }
    public void RestartGame()
    {
        playerMovements.resetPosition();
        panelEndGame.SetActive(false);
        timerScript.StartCountdown();
    }
    public void DisplayPause()
    {

    }

    private void DisplayHighScore()
    {
        textHighScore.text = "Highscore: \n" + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
