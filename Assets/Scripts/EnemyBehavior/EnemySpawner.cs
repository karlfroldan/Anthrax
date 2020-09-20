﻿/**
 * Author: Karl Frederick Roldan
 * Course: BS Computer Science
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // how many enemies do we spawn in th elevel
    public int maxEnemy;
    public float spawnTime = 2f;

    // the amount of time before spawning starts
    public float spawnDelay = 5f;

    // collection of enemy prefabs
    public GameObject[] enemies;
    // the attached waypoint we have.
    public GameObject attachedWaypoint;
    private Vector3 enposition = new Vector3(0, 0, 0);

    private int numEnemy = 0;

    public float waypointDistanceThreshold = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        // set the position of the enposition
        enposition = gameObject.transform.position;
        Debug.Log("Current Position: " + gameObject.transform.position);
        // start calling the Spawn function repeatedly after a delay
        InvokeRepeating("addEnemy", spawnDelay, spawnTime);
    }

    void addEnemy()
    {
        // Instantiate a random enemy.
        if(numEnemy < maxEnemy)
        {
            // we instantiate a random enemy(chosen from the Unity editor)
            int enemyIndex = Random.Range(0, enemies.Length);
            GameObject newEnemy = (GameObject) Instantiate(enemies[enemyIndex], enposition, transform.rotation);

            SpawnerWaypoint newEnemyWaypoint = newEnemy.AddComponent<SpawnerWaypoint>();
            // attach waypoint to the spawner
            newEnemyWaypoint.waypoint = attachedWaypoint;

            GenericEnemyMove enemyMove = newEnemy.AddComponent<GenericEnemyMove>();
            enemyMove.waypointDistanceThreshold = waypointDistanceThreshold;
            enemyMove.Move();
            // attach the game object to the new enemy

            ++numEnemy;
            Debug.Log("numEnemy: " + numEnemy);
        }
    }
}
