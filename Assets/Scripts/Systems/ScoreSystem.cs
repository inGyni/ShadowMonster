using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private int score;

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        DisplayScore();
    }

    private void DisplayScore()
    {

    }
}
