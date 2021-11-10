using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuInteraction : MonoBehaviour
{
    public TMP_Text textHighScore;
    private void Start() {
        DisplayHighScore();
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    private void DisplayHighScore()
    {
        textHighScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
