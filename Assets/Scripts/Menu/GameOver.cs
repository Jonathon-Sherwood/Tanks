using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text playerScoreInput;
    public InputField playerNames;
    public SaveData saveData;
    public GameObject highScoreScreen;
    public HighScoreList highScoreList;
    private bool firstInput = true;
    [HideInInspector] public string player1Name;
    [HideInInspector] public string player2Name;

    public List<Text> highScorePlayerNames;
    public List<Text> highScorePlayerScores;

    public List<Highscores> scores; // create the scores List

    // Start is called before the first frame update
    void Start()
    {
        saveData = GameManager.instance.saveData;
        highScoreList = saveData.highScores;
    }

    public void Name()
    {
        if (firstInput)
        {
            if(playerNames.text == "") { return; }
            Highscores player1Score = new Highscores();
            player1Name = playerNames.text;
            firstInput = false;
            player1Score.playerName = player1Name;
            player1Score.score = GameManager.instance.player1Score;
            scores.Add(player1Score);

            if(GameManager.instance.isOnePlayer)
            {
                DisplayScore();
            }
            else
            {
                playerNames.text = "";
                playerScoreInput.text = "Input Player 2 Name";
            }
        }
        else
        {
            if (playerNames.text == "") { return; }
            Highscores player2Score = new Highscores();
            player2Name = playerNames.text;
            player2Score.playerName = player2Name;
            player2Score.score = GameManager.instance.player2Score;
            scores.Add(player2Score);
            DisplayScore();
        }
    }

    public void DisplayScore()
    {
        playerScoreInput.gameObject.SetActive(false);
        highScoreScreen.SetActive(true);

        scores.Add(highScoreList.highScores[0]);
        scores.Add(highScoreList.highScores[1]);
        scores.Add(highScoreList.highScores[2]);

        scores.Sort();
        scores.Reverse();
        scores.GetRange(0, 2);

        highScoreList.highScores.Clear();
        highScoreList.highScores.Add(scores[0]);
        highScoreList.highScores.Add(scores[1]);
        highScoreList.highScores.Add(scores[2]);

        highScorePlayerNames[0].text = scores[0].score.ToString();
        highScorePlayerScores[0].text = scores[0].playerName;

        highScorePlayerNames[1].text = scores[1].score.ToString();
        highScorePlayerScores[1].text = scores[1].playerName;

        highScorePlayerNames[2].text = scores[3].score.ToString();
        highScorePlayerScores[2].text = scores[3].playerName;

        saveData.HighScoreToString(highScoreList);
        saveData.SaveToPlayerPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
