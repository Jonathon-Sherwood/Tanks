using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //Allows all scripts to call this.
    public GameObject playerPrefab; //Used for spawning player
    public GameObject[] aiPrefab; //Used for spawning AI
    public GameObject[] powerups; //Holds all powerups in the scene.
    public ShipData playerShipData;
    public ShipData player2ShipData;
    public List<HumanController> humanPlayers;
    public List<AIController> aiPlayers;
    public List<ShipData> shipList; //This list is attached to ship objects that will fill this list.
    public GameObject[] playerSpawnPoints;
    private int pauseTime = 10;
    private float pauseCountdown;
    private bool pauseOver = true;
    [HideInInspector]public bool isOnePlayer = true;

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
        if(SceneManager.GetActiveScene().buildIndex != 1) //Stops the gamemanager from running game related scripts off the game scene
        {
            return;
        }

        if (playerShipData == null) //Respawns player on death
        {
            pauseCountdown = pauseTime;
            pauseOver = false;
            RespawnPlayer1();
        }

        if (aiPlayers.Count == 0) //Spawns AI whenever the scene has none
        {
            SpawnAI();
        }

        if (pauseOver == false) { pauseCountdown--; } //Brief pause to allow player to populate before AI looks for it

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

        if(!isOnePlayer)
        {
            if (player2ShipData == null) //Respawns player on death
            {
                pauseCountdown = pauseTime;
                pauseOver = false;
                RespawnPlayer2();
            }

            if (pauseOver == false) { pauseCountdown--; } //Brief pause to allow player to populate before AI looks for it

            if (pauseCountdown < 0 && !pauseOver)
            {
                if (player2ShipData.gameObject != null)
                {
                    foreach (AIController controller in aiPlayers) //Sets each AI's target to the spawned player
                    {
                        controller.target = player2ShipData.gameObject;
                    }
                    pauseOver = true;
                }
            }
        }

        powerups = GameObject.FindGameObjectsWithTag("Powerup"); //Populates a detectable list of all powerups in the scene
    }

    public void RespawnPlayer1() //Hooks the newly spawned ship to the human controller
    {
        playerSpawnPoints = GameObject.FindGameObjectsWithTag("PlayerSpawn");
        Transform spawnLocation = playerSpawnPoints[Random.Range(0, playerSpawnPoints.Length)].transform;
        playerShipData = Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity).gameObject.GetComponent<ShipData>();
        playerShipData.owner = humanPlayers[0].gameObject;
        humanPlayers[0].mover = playerShipData.mover.GetComponent<ShipMover>();
        humanPlayers[0].shooter = playerShipData.shooter.GetComponent<ShipShooter>();

        if (!isOnePlayer) //Sets the newly spawned ship's camera to split screen if multiplayer
        {
            playerShipData.gameObject.transform.GetChild(2).GetComponentInChildren<Camera>().rect = new Rect(0, .5f, 1, 1);
            playerShipData.gameObject.transform.GetChild(3).GetComponentInChildren<Camera>().rect = new Rect(-0.02f, .5f, .2f, 0.2f);
        }
    }

    public void RespawnPlayer2() //Hooks the newly spawned ship to the human controller
    {
        playerSpawnPoints = GameObject.FindGameObjectsWithTag("PlayerSpawn");
        Transform spawnLocation = playerSpawnPoints[Random.Range(0, playerSpawnPoints.Length)].transform;
        player2ShipData = Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity).gameObject.GetComponent<ShipData>();
        player2ShipData.owner = humanPlayers[1].gameObject;
        humanPlayers[1].mover = player2ShipData.mover.GetComponent<ShipMover>();
        humanPlayers[1].shooter = player2ShipData.shooter.GetComponent<ShipShooter>();
        player2ShipData.gameObject.transform.GetChild(2).GetComponentInChildren<Camera>().rect = new Rect(0, 0, 1, .5f); //Sets the newly spawned ship's camera to half
        //player2ShipData.gameObject.transform.GetChild(3).GetComponentInChildren<Camera>().rect = new Rect(0, 0, 0, 0);
    }

    public void SpawnAI() //Creates one of each type of unique ai in random locations
    {
        GameObject[] aiSpawnPoints = GameObject.FindGameObjectsWithTag("Waypoint");
        foreach (GameObject ai in aiPrefab)
        {
            Transform spawnLocation = aiSpawnPoints[Random.Range(0, aiSpawnPoints.Length)].transform;
            Instantiate(ai, new Vector3(spawnLocation.position.x, 12, spawnLocation.position.z), Quaternion.identity);   
        }
    }

    public void IsOnePlayer()
    {
        isOnePlayer = true;
        
    }

    public void IsTwoPlayer()
    {
        isOnePlayer = false;
    }

}
