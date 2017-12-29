﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnsPool : MonoBehaviour {

    public GameObject columnPrefab;                                 //The column game object.
    public GameObject player;                                 //The player game object.
    public int columnPoolSize = 15;                                  //How many columns to keep on standby.
    public float spawnRate = 2f;                                    //How quickly columns spawn.
    public float columnMin = -1f;                                   //Minimum y value of the column position.
    public float columnMax = 3.5f;                                  //Maximum y value of the column position.
    public float balloonDIff = 2f;
    private GameObject[] columns;                                   //Collection of pooled columns.
    private int currentColumn = 0;                                  //Index of the current column in the collection.

    private Vector2 objectPoolPosition = new Vector2(-15, -25);     //A holding position for our unused columns offscreen.
    private float spawnYPosition = 10f;

    private float timeSinceLastSpawned;


    void Start()
    {
        timeSinceLastSpawned = 0f;
        spawnYPosition =  player.transform.position.y + 5f;
        //Initialize the columns collection.
        columns = new GameObject[columnPoolSize];
        //Loop through the collection... 
        for (int i = 0; i < columnPoolSize; i++)
        {
            //...and create the individual columns.
            columns[i] = (GameObject)Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity);
        }
    }


    //This spawns columns as long as the game is not over.
    void Update()
    {
        spawnYPosition = player.transform.position.y + 3f;
        timeSinceLastSpawned += 0.1f;//Time.deltaTime;
        //print(timeSinceLastSpawned);
        //if (GameControl.instance.gameOver == false && timeSinceLastSpawned >= spawnRate)
        if (timeSinceLastSpawned >= spawnRate)
        {
            timeSinceLastSpawned = 0f;

            // check in balloon exist
            bool isExistBalloon = false;
            for (int i = 0; i < columnPoolSize; i++)
            {
                float posistionObj = columns[i].transform.position.y;
                if ((posistionObj - balloonDIff) < spawnYPosition && (posistionObj + balloonDIff) > spawnYPosition) {
                    isExistBalloon = true;
                }
            }

            if (!isExistBalloon) {
                //Set a random y position for the column
                float spawnXPosition = Random.Range(columnMin, columnMax);

                //...then set the current column to that position.
                columns[currentColumn].transform.position = new Vector2(spawnXPosition, spawnYPosition);

                //Increase the value of currentColumn. If the new size is too big, set it back to zero
                currentColumn++;

                if (currentColumn >= columnPoolSize)
                {
                    currentColumn = 0;
                }
            }            
            
        }
    }
}
