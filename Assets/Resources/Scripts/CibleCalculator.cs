using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CibleCalculator : MonoBehaviour
{
    private int totalScore = 0;
    private GameObject[] allRocks;
    public Collider2D[] colliderCibles;

    public void CalculateScore()
    {
        GetAllRocks();
        for (int i = 0; i < allRocks.Length; i++)
        {
            string layerTouchingName = "";
            for (int j = 0; j < colliderCibles.Length; j++)
            {
                layerTouchingName = GetColliderLayerName(allRocks[i], colliderCibles[j], layerTouchingName);
            }
            GetPointsFromCibles(layerTouchingName);
        }
        CalculateHighScore();
    }

    private void GetAllRocks()
    {
        allRocks = GameObject.FindGameObjectsWithTag("Rock");
    }

    private string GetColliderLayerName(GameObject rock, Collider2D collider, string lastLayerTouchingName)
    {
        bool isTouching = rock.GetComponent<Rigidbody2D>().IsTouching(collider);
        if (isTouching)
            return collider.name;
        else
            return lastLayerTouchingName;
    }

    private void GetPointsFromCibles(string cible)
    {
        switch (cible)
        {
            case "CENTER":
                totalScore += 1000;
                break;
            case "INNER":
                totalScore += 500;
                break;
            case "INNER2":
                totalScore += 200;
                break;
            case "OUTER":
                totalScore += 100;
                break;
            default: break;
        }
    }

    public void CalculateHighScore()
    {
        Debug.Log("TotalScore = " + totalScore);
        if (PlayerPrefs.GetInt("HighScore", 0) < totalScore)
        {
            PlayerPrefs.SetInt("HighScore", totalScore);
        }
    }
    public float GetScore()
    {
        return totalScore;
    }

}
