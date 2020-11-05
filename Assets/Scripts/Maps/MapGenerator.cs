using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int rows;
    public int cols;
    public float tileWidth = 2500.0f;
    public float tileHeight = 2500.0f;
    public Room[,] rooms;

    public int seed;

    public bool useSeed;
    public bool isMapOfTheDay;

    public Room[] roomTemplates;

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateMap()
    {
        //If we have a seed, seed the random number generator
        if (useSeed)
        {
            if (isMapOfTheDay)
            {
                DateTime today = DateTime.Today;
                int dateNumber = (int)today.Ticks;
                seed = dateNumber;
            }

            UnityEngine.Random.InitState(seed);
        }

        //Create rooms array
        rooms = new Room[cols,rows];

        //For every row that needs to be created
        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            //For every col that needs to be created
            for(int currentCol = 0; currentCol < cols; currentCol++)
            {
                // Create a new random room
                GameObject tempRoom = Instantiate(GetRandomRoom().gameObject, Vector3.zero, Quaternion.identity) as GameObject;
                // Move the room to the appropriate place
                tempRoom.transform.position = new Vector3(currentCol * tileWidth, 0.0f, currentRow * -tileHeight);
                //Name the room in a way we can recognize it
                tempRoom.name = "Room (" + currentCol + ", " + currentRow + ")";
                //Make this room a child of THIS object
                tempRoom.transform.parent = this.transform;
                // Save that room in the correct location in the array
                rooms[currentCol, currentRow] = tempRoom.GetComponent<Room>();
                // Open the appropriate doors 
                if(currentRow != 0) //If not on the top row
                {
                    rooms[currentCol, currentRow].doorNorth.SetActive(false);
                }
                if (currentRow != rows - 1) //If not on the bottom row
                {
                    rooms[currentCol, currentRow].doorSouth.SetActive(false);
                }
                if (currentCol != 0) //If not on the first column
                {
                    rooms[currentCol, currentRow].doorWest.SetActive(false);
                }
                if (currentCol != cols - 1) //If not on the bottom row
                {
                    rooms[currentCol, currentRow].doorEast.SetActive(false);
                }
            }
        }

    }

    public Room GetRandomRoom()
    {
        int randomNumber = UnityEngine.Random.Range(0, roomTemplates.Length);
        return roomTemplates[randomNumber];
    }

}
