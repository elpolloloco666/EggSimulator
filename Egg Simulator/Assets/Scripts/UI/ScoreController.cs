using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public TMP_Text ScoreText;


    private void Update()
    {
        if(GameManager.instance.currentState == GameState.WON )
        {
            int currentScore = GameManager.instance.calculateScore(true);
            int highScore = GameManager.instance.getScores(true)[0];

            if(currentScore >= highScore)
            {
                ScoreText.text = "NEW HIGHSCORE: " + currentScore.ToString();
            }
            else
            {
                ScoreText.text = "SCORE: " + currentScore.ToString();
            }

        }

        if (GameManager.instance.currentState == GameState.LOST)
        {
            int currentScore = GameManager.instance.calculateScore(false);
            int highScore = GameManager.instance.getScores(true)[0];

            if (currentScore >= highScore)
            {
                ScoreText.text = "NEW HIGHSCORE: " + currentScore.ToString();
            }
            else
            {
                ScoreText.text = "SCORE: " + currentScore.ToString();
            }

        }
    }


    
}
