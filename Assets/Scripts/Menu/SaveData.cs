using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public HighScoreList highScores;
    public SettingsMenu settingsMenu;

    //Saves the list of player preferences to unity on call
    public void SaveToPlayerPrefs()
    {
        PlayerPrefs.SetString("MyData", HighScoreToString(highScores));
        PlayerPrefs.SetInt("OnePlayer", (GameManager.instance.isOnePlayer ? 1 : 0));
        PlayerPrefs.SetInt("PS4Controller", (GameManager.instance.isOnePlayer ? 1 : 0));
        PlayerPrefs.SetInt("MapOfTheDay", (GameManager.instance.isMapOfTheDay ? 1 : 0));
        if(settingsMenu == null) { return; }
        PlayerPrefs.SetFloat("MusicVolume", (settingsMenu.currentMusicVolume));
        PlayerPrefs.SetFloat("SFXVolume", (settingsMenu.currentSFXVolume));
    }

    //Called at at the beginning of the game for options and scores
    public void LoadFromPlayerPrefs()
    {
        highScores = JsonUtility.FromJson<HighScoreList>(PlayerPrefs.GetString("MyData"));
        GameManager.instance.isOnePlayer = (PlayerPrefs.GetInt("OnePlayer") != 0);
        GameManager.instance.isOnePlayer = (PlayerPrefs.GetInt("PS4Controller") != 0);
        GameManager.instance.isMapOfTheDay = (PlayerPrefs.GetInt("MapOfTheDay") != 0);
        settingsMenu.currentMusicVolume = (PlayerPrefs.GetFloat("MusicVolume"));
        settingsMenu.currentSFXVolume = (PlayerPrefs.GetFloat("SFXVolume"));
    }


    //Converts the different types of data for highscores into a JSON string
    public string HighScoreToString(HighScoreList input)
    {
        string temp = JsonUtility.ToJson(input);
        return temp;
    }
}
