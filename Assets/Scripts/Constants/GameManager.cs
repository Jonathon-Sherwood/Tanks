using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //Allows all scripts to call this.
    public ShipData playerShipData;
    public List<Controller> players;
    public List<ShipData> shipList; //This list is attached to ship objects that will fill this list.

    private void Awake()
    {
        //Turns the gamemanager into a singleton.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        foreach (ShipData ship in FindObjectsOfType<ShipData>()) //Adds all ships in the game to a list
        {
            shipList.Add(ship);
            print(ship.gameObject.name);
        }
    }

    private void Update()
    {

    }
}
