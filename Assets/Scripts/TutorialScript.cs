using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    private PauseMenu pauseMenu;
    private EnemySpawner enemySpawner;
    private bool check = false;

    public GameObject bg;
    public GameObject ins2;
    public GameObject ins3;

    void Start(){
        pauseMenu = GameObject.Find("PCanvas").GetComponent<PauseMenu>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        pauseMenu.isPaused = true;
        Time.timeScale = 0f;
    }

    void Update(){
        int currentWave = enemySpawner.GetCurrentWave();
        int enemiesInWave = enemySpawner.waves[currentWave];
        int enemiesKilled = enemySpawner.enemiesKilled;

        if (currentWave == 1 && enemiesInWave == enemiesKilled && !check){
            bg.SetActive(true);
            ins2.SetActive(true);
            check = true;
        }

        if (currentWave == 2 && 5 == enemiesKilled && check){
            bg.SetActive(true);
            ins3.SetActive(true);
            check = false;
        }
        
    }

    public void loadMenu(){
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
