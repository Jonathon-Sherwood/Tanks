using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButton : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject optionsMenu;

    //Activates/Deactivates menu based on button input
    public void OptionsMenu()
    {
        mainMenu.SetActive(!mainMenu.activeInHierarchy);
        optionsMenu.SetActive(!optionsMenu.activeInHierarchy);
    }

}
