using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    //Player Input
    public Text playerScoreInput;
    [HideInInspector] public string player1Name;
    [HideInInspector] public string player2Name;
    public InputField playerNames;
    private bool firstInput = true;

    //Highscore
    public SaveData saveData;
    public GameObject highScoreScreen;
    public HighScoreList highScoreList;
    public List<Text> highScorePlayerNames;
    public List<Text> highScorePlayerScores;
    public List<Highscores> scores;

    // Start is called before the first frame update
    void Start()
    {
        saveData = GameManager.instance.saveData;
        highScoreList = saveData.highScores;
    }

    public void Name()
    {
        if (firstInput) //Accepts the players name and associates it to their score
        {
            if(playerNames.text == "") { return; }
            Highscores player1Score = new Highscores();
            player1Name = playerNames.text;
            firstInput = false;
            player1Score.playerName = player1Name;
            player1Score.score = GameManager.instance.player1Score;
            scores.Add(player1Score);

            if(GameManager.instance.isOnePlayer) //Moves on to scoreboard if single player
            {
                DisplayScore();
            }
            else
            {
                playerNames.text = "";
                playerScoreInput.text = "Input Player 2 Name";
            }
        }
        else //Accepts second player name and associates it to their score
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

        //Turns off player name input and shows scoreboard
        playerScoreInput.gameObject.SetActive(false); 
        highScoreScreen.SetActive(true);

        if (scores != null)
        {
            //Adds the already existing high scores to the list
            scores.Add(highScoreList.highScores[0]);
            scores.Add(highScoreList.highScores[1]);
            scores.Add(highScoreList.highScores[2]);
        }
        else
        {
            scores = saveData.highScores.highScores;
        }

        //Sorts entire list including new player scores
        scores.Sort();
        scores.Reverse();
        scores.GetRange(0, 2);

        //Assigns the top 3 scores to the list
        if (highScoreList != null)
        {
            highScoreList.highScores.Clear();
        }
        highScoreList.highScores.Add(scores[0]);
        highScoreList.highScores.Add(scores[1]);
        highScoreList.highScores.Add(scores[2]);

        //Displays top 3 scores and player names on leaderboard
        highScorePlayerNames[0].text = scores[0].score.ToString();
        highScorePlayerScores[0].text = scores[0].playerName;

        highScorePlayerNames[1].text = scores[1].score.ToString();
        highScorePlayerScores[1].text = scores[1].playerName;

        highScorePlayerNames[2].text = scores[3].score.ToString();
        highScorePlayerScores[2].text = scores[3].playerName;

        //Saves high score list to player prefs
        saveData.HighScoreToString(highScoreList);
        saveData.SaveToPlayerPrefs();
    }

    public void PlayAgain()
    {
        Destroy(GameObject.FindObjectOfType<GameManager>());
        SceneManager.LoadScene(0);
    }
}
