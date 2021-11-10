using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header ("Timer")]
    public TMP_Text textTimer;
    [SerializeField] private float startCountDownAt = 30f; 
    private bool isCountdownEnable = false;
    private float countdownRefreshTime = 1f;
    private float currentCountDownTime;
    public StartGameScript startGameScript;

    private void Update() {
        CountdownEnable();
    }
    private void CountdownEnable()
    {
        if (isCountdownEnable)
        {
            if (currentCountDownTime > 0)
            {
                currentCountDownTime -= countdownRefreshTime * Time.deltaTime;
                textTimer.text = ((int)currentCountDownTime + 1).ToString();
            }
            if (currentCountDownTime <= 0)
            {
                StopCountdown();
                GameObject.FindObjectOfType<CibleCalculator>().GetScore();
            }
        }
    }
    public void StartCountdown()
    {
        textTimer.text = ((int)startCountDownAt).ToString();
        currentCountDownTime = startCountDownAt;
        isCountdownEnable = true;
    }
    private void StopCountdown()
    {
        isCountdownEnable = false;
        textTimer.text = ((int)currentCountDownTime).ToString();
        currentCountDownTime = 0;
        startGameScript.DisplayEndGame();
    }
    public void RestartCountDown()
    {
        StartCountdown();
    }
}
