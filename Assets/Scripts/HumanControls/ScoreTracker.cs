using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    public float currentScore = 0; //Holds onto the player's score
    public TMP_Text currentScoreText;
    public TMP_Text currentLivesText;

    private void Update()
    {
        //Sets score on screen to mirror player's score
        if(currentScoreText == null) { return; }
        currentScoreText.text = "Score: " + currentScore;

        if (currentLivesText == null) { return; }
        int displayedLives = GetComponent<HumanController>().lives + 1;
        currentLivesText.text = "" + displayedLives;
    }
}
