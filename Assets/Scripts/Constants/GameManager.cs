using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //Allows all scripts to call this.
    public GameObject playerPrefab; //Used for spawning player
    public ShipData playerShipData;
    public List<HumanController> humanPlayers;
    public List<AIController> aiPlayers;
    public List<ShipData> shipList; //This list is attached to ship objects that will fill this list.
    private int pauseTime = 10;
    private float pauseCountdown;
    private bool pauseOver = true;

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

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) //Test for respawning player
        {
            pauseCountdown = pauseTime;
            pauseOver = false;
            RespawnPlayer();
        }

        if(pauseOver == false) { pauseCountdown--; } //Brief pause to allow player to populate before AI looks for it

        if(pauseCountdown < 0 && !pauseOver)
        {
            if (playerShipData.gameObject != null)
            {
                foreach (AIController controller in aiPlayers) //Sets each AI's target to the spawned player
                {
                    controller.target = playerShipData.gameObject;
                }
                pauseOver = true;
            }
        }
    }

    public void RespawnPlayer() //Hooks the newly spawned ship to the human controller
    {
        playerShipData = Instantiate(playerPrefab, new Vector3(0, 9, 0), Quaternion.identity).gameObject.GetComponent<ShipData>();
        //playerShipData.mover = humanPlayers[0].mover.GetComponent<ShipMover>();
        //playerShipData.shooter = humanPlayers[0].shooter.GetComponent<ShipShooter>();
        playerShipData.owner = humanPlayers[0].gameObject;
        humanPlayers[0].mover = playerShipData.mover.GetComponent<ShipMover>();
        humanPlayers[0].shooter = playerShipData.shooter.GetComponent<ShipShooter>();

    }
}
