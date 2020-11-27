using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    public float currentScore = 0; //Holds onto the player's score
    public TMP_Text currentScoreText;

    private void Update()
    {
        if(currentScoreText == null) { return; }
        currentScoreText.text = "Score: " + currentScore;
    }
}
