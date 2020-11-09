using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //Allows all scripts to call this.
    public GameObject playerPrefab; //Used for spawning player
    public GameObject[] aiPrefab; //Used for spawning AI
    public GameObject[] powerups; //Holds all powerups in the scene.
    public ShipData playerShipData;
    public List<HumanController> humanPlayers;
    public List<AIController> aiPlayers;
    public List<ShipData> shipList; //This list is attached to ship objects that will fill this list.
    public GameObject[] playerSpawnPoints;
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
        if (playerShipData == null) //Respawns player on death
        {
            pauseCountdown = pauseTime;
            pauseOver = false;
            RespawnPlayer();
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

        powerups = GameObject.FindGameObjectsWithTag("Powerup"); //Populates a detectable list of all powerups in the scene
    }

    public void RespawnPlayer() //Hooks the newly spawned ship to the human controller
    {
        playerSpawnPoints = GameObject.FindGameObjectsWithTag("PlayerSpawn");
        Transform spawnLocation = playerSpawnPoints[Random.Range(0, playerSpawnPoints.Length)].transform;
        playerShipData = Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity).gameObject.GetComponent<ShipData>();
        playerShipData.owner = humanPlayers[0].gameObject;
        humanPlayers[0].mover = playerShipData.mover.GetComponent<ShipMover>();
        humanPlayers[0].shooter = playerShipData.shooter.GetComponent<ShipShooter>();
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
}
