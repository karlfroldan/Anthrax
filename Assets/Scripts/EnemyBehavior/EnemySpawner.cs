/**
 * Author: Karl Frederick Roldan
 * Course: BS Computer Science
 */

using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // The number of waves per level.
    // This is an array because each wave has the number of enemies defined in it.
    public int[] waves;
    public float spawnTime = 2f;

    // the amount of time before spawning starts
    public float spawnDelay = 5f;

    // randomizing distances between spawn points
    public float spawnDistance = 1f;

    // collection of enemy prefabs
    public GameObject[] enemies;
    // the attached waypoint we have.
    private Vector3 enposition = new Vector3(0, 0, 0);
    private List<Vector3> enpositions = new List<Vector3>();

    private int numEnemy = 0;

    // the current wave
    private int currWave;

    // Start is called before the first frame update
    void Start()
    {
        // set the position of the enposition
        enposition = gameObject.transform.position;
        

        // create 5 random enpositions
        foreach (int value in Enumerable.Range(0, 5))
        {
            float randx = Random.Range(0f, spawnDistance);
            float randy = Random.Range(0f, spawnDistance);

            Vector3 newEnposition = new Vector3(enposition.x + randx, enposition.y + randy, 0);

            enpositions.Add(newEnposition);
        }


        // Start the first wave
        foreach (int wave in Enumerable.Range(0, waves.Length))
        {
            currWave = wave;
            MakeWave();
        }
    }

    // Create a wave. Resets the enemy count
    void MakeWave()
    {
        Debug.Log("Wave " + (currWave + 1));

        // start calling the Spawn function repeatedly after a delay
        InvokeRepeating("addEnemy", spawnDelay, spawnTime);
    }

    void addEnemy()
    {
        // Instantiate a random enemy.
        if (numEnemy < waves[currWave])
        {
            // we instantiate a random enemy(chosen from the Unity editor)
            int enemyIndex = Random.Range(0, enemies.Length);
            int enposIndex = Random.Range(0, enpositions.Count);

            GameObject newEnemy = (GameObject)Instantiate(enemies[enemyIndex],
                enpositions[enposIndex], transform.rotation);

            // Add the canMove property. The starter sprites
            // has no ability to move therefore, we have to set this to true
            // everytime we make a newEnemy object.
            newEnemy.GetComponent<Pathfinding.AIPath>().canMove=true;
            ++numEnemy;
        }

    }
}
