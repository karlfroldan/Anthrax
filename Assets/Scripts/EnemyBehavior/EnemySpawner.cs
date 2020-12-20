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
    public GameObject boss;
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

    private bool isLastWave;
    private bool finishedSpawningAllEnemies = false;
    private bool hasSpawnedBoss = false;

    public GameObject destination;

    // Start is called before the first frame update
    void Start()
    {
        totalEnemies = waves.Sum();
        //Debug.Log("total enemies: " + totalEnemies);
        // set the position of the enposition
        enposition = gameObject.transform.position;
        Debug.Log("waves length:" + waves.Length);

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
        /* Check if last wave */
        isLastWave = waves.Length == currWave;
        finishedSpawningAllEnemies = isLastWave && (enemiesInWave == waves[waves.Length - 1]);
        Debug.Log("FinishedSpawningAllEnemies? " + finishedSpawningAllEnemies);

        /* Start the first wave */
        if (!waveIsActive) {
            StartCoroutine(StartNewWave());
            //InvokeRepeating("addEnemy", spawnDelay, spawnTime);
            waveIsActive = true;
            waveJustSetToFalse = false;
            
        } else {
            /* A Wave is currently active */

            /* Check to see if all the enemies in the wave are dead */
            /* Prevent out of bounds exception */
            if (currWave < waves.Length) {
                if (AllEnemiesInWaveAreDead(currWave) && !waveJustSetToFalse) {                  
                    waveIsActive = false;
                    waveJustSetToFalse = true;
                    spew = false;

                    currWave++;
                }
            }
            
        }

        if (finishedSpawningAllEnemies && !hasSpawnedBoss) {
            /* We can spawn the boss now */
            Debug.Log("We can spawn the boss now");
            int enposIndex = Random.Range(0, enpositions.Count);

            GameObject newBoss = (GameObject) Instantiate(boss,
                enpositions[enposIndex], transform.rotation);
            newBoss.GetComponent<Pathfinding.AIPath>().canMove=true;
            newBoss.GetComponent<Pathfinding.AIDestinationSetter>().target = destination.transform; 
            newBoss.AddComponent<Pathfinding.SimpleSmoothModifier>();

            hasSpawnedBoss = true;
        }
    }

    IEnumerator StartNewWave() {
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);
        if (currWave == 1 || currWave == 0) {
            yield return new WaitForSeconds(5);
        } else {
            WaveUI.GetComponent<TMPro.TextMeshProUGUI>().text = "Wave " + currWave;
            WaveUI.SetActive(true);

            yield return new WaitForSeconds(07.5f);
            WaveUI.SetActive(false);
        }
        //After we have waited 5 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        //Debug.Log("Reset enemiesInWave | " + Time.time);
        enemiesInWave = 0; // reset the counter

        
        spew = true;
        
        InvokeRepeating("AddEnemy", spawnDelay, spawnTime);
    }

    void AddEnemy() {
        // Instantiate a random enemy.
        if (enemiesInWave < waves[currWave - 1] && spew) {
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

    public int GetWave() {
        return currWave;
    }
}
