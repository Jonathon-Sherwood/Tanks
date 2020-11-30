using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Highscores : IComparable<Highscores>
{
    public string playerName;
    public float score;

    //Stacks scores based on highest
    public int CompareTo(Highscores other)
    {
        if (other == null)
        {
            return 1;
        }
        if (this.score > other.score)
        {
            return 1;
        }
        if (this.score < other.score)
        {
            return -1;
        }
        return 0;
    }
}

[System.Serializable]
public class HighScoreList
{
    public List<Highscores> highScores; //Holds a list of all highscores
}