using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void Play()
    {
        //Inrcreases scene index by 1, going from menu to gameplay
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
