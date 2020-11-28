using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public float player1Score;
    public float player2Score;
    private int pauseTime = 10;
    private float pauseCountdown;
    private bool pauseOver = true;
    [HideInInspector]public bool isOnePlayer = true;

    //Death Canvases
    public GameObject player1DeathScreen;
    public GameObject player2DeathScreen;
    public Text player1ScoreText;
    public Text player2ScoreText;

    //Game Reset
    public int maxLives = 3;
    private int player1Lives = 3;
    private bool player1Dead = false;
    private int player2Lives = 3;
    private bool player2Dead = false;

    //Saves
    public SaveData saveData;

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

        saveData.LoadFromPlayerPrefs();
    }

    private void Start()
    {
        player1Lives = maxLives;
        player2Lives = maxLives;
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex != 1) //Stops the gamemanager from running game related scripts off the game scene
        {
            return;
        }

        InstantKill();
        HandleDeath();

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

    /// <summary>
    /// Contains the calls for respawning players and displaying score on death. Loads next scene on both max deaths.
    /// </summary>
    public void HandleDeath()
    {
        if (playerShipData == null && player1Lives > 0) //Respawns player on death
        {
            pauseCountdown = pauseTime;
            pauseOver = false;
            RespawnPlayer1();
            player1Lives--;
        }
        else if (playerShipData == null && player1Lives <= 0) //Locks player controls on max deaths
        {
            humanPlayers[0].controlType = HumanController.ControlType.Dead;
            player1Score = humanPlayers[0].GetComponentInChildren<ScoreTracker>().currentScore;
            player1Dead = true;
        }

        if (player1Dead && !isOnePlayer) //Displays final score on max deaths
        {
            player1DeathScreen.SetActive(true);
            player1ScoreText.text = "Final Score: " + player1Score;
        }

        if (player2Dead) //Displays final score on max deaths
        {
            player2DeathScreen.SetActive(true);
            player2ScoreText.text = "Final Score: " + player2Score;
        }

        if (!isOnePlayer) //If 2 player
        {
            //Once both players die, load game over
            if (player1Dead && player2Dead && SceneManager.GetActiveScene().buildIndex != 2)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                player2DeathScreen.SetActive(false);
                player1DeathScreen.SetActive(false);
            }
        }
        else //If 1 player
        {
            if (player1Dead) //And player 1 max dies
            {
                //Load game over
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        if (!isOnePlayer) //If 2 player
        {
            if (player2ShipData == null && player2Lives > 0) //Respawns player 2 on death
            {
                pauseCountdown = pauseTime;
                pauseOver = false;
                RespawnPlayer2();
                player2Lives--;
            }
            else if (player2ShipData == null && player2Lives <= 0) //Locks controls on max deaths
            {
                player2Dead = true;
                humanPlayers[1].controlType = HumanController.ControlType.Dead;
                player2Score = humanPlayers[1].GetComponentInChildren<ScoreTracker>().currentScore;
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

            //Adjusts position of radar camera if two player mode
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

    public void InstantKill()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            foreach(HumanController player in humanPlayers)
            {
                Destroy(player.mover.gameObject);
            }
        }
    }

}
