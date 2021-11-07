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
    private float countdownRefresh = 1f;
    private float currentCountDownTime;


    private void Start() {
        StartCountdown();
    }
    private void Update() {
        CountdownEnable();
    }
    private void CountdownEnable()
    {
        if (isCountdownEnable)
        {
            if (currentCountDownTime > 0)
            {
                currentCountDownTime -= countdownRefresh * Time.deltaTime;
                textTimer.text = ((int)currentCountDownTime + 1).ToString();
            }
        }
    }
    private void StartCountdown()
    {
        currentCountDownTime = startCountDownAt;
        isCountdownEnable = true;
    }
    private void StopCountdown()
    {
        isCountdownEnable = false;
        currentCountDownTime = 0;
    }
}
