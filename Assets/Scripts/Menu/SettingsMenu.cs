﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer; //Allows for setting unique volumes for separate groups

    //Active based on condition
    public GameObject playerTwoShip;
    public GameObject playerTwoText;
    public GameObject playerTwo;

    //Keeps UI items consistent between saves
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle mapOfTheDayToggle;

    //Options menu on/off
    public GameObject mainMenu;
    public GameObject optionsMenu;

    //Sets volume on load
    public float currentMusicVolume;
    public float currentSFXVolume;

    //Controller Menu Support
    public GameObject mainFirstButton, optionsFirstButton, optionsClosedButton;

    private void Start()
    {
        //Sets values to match loaded prefs
        musicSlider.value = currentMusicVolume;
        sfxSlider.value = currentSFXVolume;
        mapOfTheDayToggle.isOn = GameManager.instance.isMapOfTheDay;

        //clear the selected firstbutton and set ours
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainFirstButton);

    }

    private void Update()
    {
        //Turns off second player if oneplayer true
        if (GameManager.instance.isOnePlayer == true)
        {
            playerTwoShip.SetActive(false);
            playerTwoText.SetActive(false);
            playerTwo.SetActive(false);
        }
        else //Turns on second player
        {
            playerTwoShip.SetActive(true);
            playerTwoText.SetActive(true);
            playerTwo.SetActive(true);
        }

        //Sets map of the day to mirror toggle setting
        GameManager.instance.isMapOfTheDay = mapOfTheDayToggle.isOn;

        //Playclipatpoint doesn't use mixers, so this holds all values above the minimum
        GameManager.instance.sfxAudio = currentSFXVolume + 80;

        //Allows the player to back out of the options menu without confirming
        if (Input.GetButtonDown("Cancel") && optionsMenu.activeInHierarchy)
        {
            OptionsMenu();
        }

    }

    //Activates/Deactivates menu based on button input
    public void OptionsMenu()
    {
        mainMenu.SetActive(!mainMenu.activeInHierarchy);
        optionsMenu.SetActive(!optionsMenu.activeInHierarchy);

        if (optionsMenu.activeInHierarchy)
        {
            //clear the selected firstbutton and set ours
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(optionsFirstButton);
        }
        else
        {
            //clear the selected firstbutton and set ours
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(optionsClosedButton);
        }

    }

    //Sets volume to match slider in main menu
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
        currentMusicVolume = volume;
    }

    //Sets volume to match slider in main menu
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
        currentSFXVolume = volume;
    }

    //Allows a button to update one player on gamemanager
    public void IsOnePlayer()
    {
        GameManager.instance.isOnePlayer = true;
    }

    //Allows a button to update one player on gamemanager
    public void IsTwoPlayer()
    {
        GameManager.instance.isOnePlayer = false;
    }

    //Exits the application
    public void Quit()
    {
        Application.Quit();
    }

}
