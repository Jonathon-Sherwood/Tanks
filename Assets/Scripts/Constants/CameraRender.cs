using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRender : MonoBehaviour
{

    //Stops this camera from processing fog
    private void OnPreRender()
    {
        RenderSettings.fog = false;
    }

    //Turns back on the fog for the rest of the game
    private void OnPostRender()
    {
        RenderSettings.fog = true;
    }
}
