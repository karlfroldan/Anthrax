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
    public GameObject WaveUI;
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

    //private int numEnemy = 0;
    public int numEnemy = 0; 
    public int enemiesKilled = 0;
    private int enemiesInWave = 0;

    // the current wave
    private int currWave = 0;
    private bool waveIsActive = false;
    private bool waveJustSetToFalse = false;
    private int totalEnemies;
    private bool spew = false;

    public GameObject destination;

    // Start is called before the first frame update
    void Start()
    {
        totalEnemies = waves.Sum();
        Debug.Log("total enemies: " + totalEnemies);
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

        
    }

    bool AllEnemiesInWaveAreDead(int wave) {
        int runningSum = 0;
        // get the total number of enemies in the waves
        foreach (int i in Enumerable.Range(0, wave)) {
            runningSum += waves[i];
        }
        return runningSum - enemiesKilled == 0;
    }

    void Update() {
        // Start the first wave
        if (!waveIsActive) {
            StartCoroutine(StartNewWave());
            //InvokeRepeating("addEnemy", spawnDelay, spawnTime);
            waveIsActive = true;
            waveJustSetToFalse = false;
            
        } else {
            // if a wave is currently active
                
            /// check to see if all the enemies in the wave are dead
            // prevent out of bounds exception
            if (currWave < waves.Length) {
                if (AllEnemiesInWaveAreDead(currWave) && !waveJustSetToFalse) {
                    Debug.Log("AllEnemiesInWaveAreDead?" + AllEnemiesInWaveAreDead(currWave));
                    
                    waveIsActive = false;
                    waveJustSetToFalse = true;
                    spew = false;

                    currWave++;
                }
            }
            
        }
    }

    IEnumerator StartNewWave() {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        if (currWave == 1 || currWave == 0) {
            yield return new WaitForSeconds(5);
        } else {
            WaveUI.GetComponent<TMPro.TextMeshProUGUI>().text = "Wave " + currWave;
            WaveUI.SetActive(true);

            yield return new WaitForSeconds(13);
            WaveUI.SetActive(false);
        }
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        Debug.Log("Reset enemiesInWave | " + Time.time);
        enemiesInWave = 0; // reset the counter

        
        spew = true;
        InvokeRepeating("addEnemy", spawnDelay, spawnTime);
    }

    void addEnemy()
    {
        // Instantiate a random enemy.
        if (enemiesInWave < waves[currWave - 1] && spew)
        {
            Debug.Log("From addEnemy enemies in wave: " + enemiesInWave + " |current wave max: " + waves[currWave - 1]);
            // we instantiate a random enemy(chosen from the Unity editor)
            int enemyIndex = Random.Range(0, enemies.Length);
            int enposIndex = Random.Range(0, enpositions.Count);

            GameObject newEnemy = (GameObject)Instantiate(enemies[enemyIndex],
                enpositions[enposIndex], transform.rotation);

            // Add the canMove property. The starter sprites
            // has no ability to move therefore, we have to set this to true
            // everytime we make a newEnemy object.
            newEnemy.GetComponent<Pathfinding.AIPath>().canMove=true;
            newEnemy.GetComponent<Pathfinding.AIDestinationSetter>().target = destination.transform; 

            newEnemy.AddComponent<Pathfinding.SimpleSmoothModifier>();
            // get the component
            //var simpleSmooth = newEnemy.GetComponent<Pathfinding.SimpleSmoothModifier>();

            enemiesInWave++;
            ++numEnemy;
        }
    }
}
