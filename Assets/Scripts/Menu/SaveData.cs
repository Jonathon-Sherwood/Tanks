using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public HighScoreList highScores;

    public void SaveToPlayerPrefs()
    {
        PlayerPrefs.SetString("MyData", HighScoreToString(highScores));
    }

    public void SavePlayerPrefsToDisk()
    {

    }

    public void LoadFromPlayerPrefs()
    {
        highScores = JsonUtility.FromJson<HighScoreList>(PlayerPrefs.GetString("MyData"));
    }

    public string HighScoreToString(HighScoreList input)
    {
        string temp = JsonUtility.ToJson(input);
        return temp;
    }
}
