using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Decider : MonoBehaviour{
    
    public GameObject enemy_spawner;        // Drag EnemySpawner here???
    public GameObject house;        // Drag house GameObj here
    //private float hp;       // Get house.hp property ???
    //private int sum = 0;        // add elements of EnemySpawner.waves[]
    //private  int enemies_killed;        // get EnemySpawner.enemiesKilled property ???


    public GameObject victoryMenu;      
    public GameObject defeatMenu;
    public GameObject background;    

    Actor houseActor;
    EnemySpawner enemySpawner;   

    void Start(){
        houseActor = house.GetComponent<Actor>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    void Update(){
        float houseHp = houseActor.health;
        int sum = 0;
        foreach (var i in enemySpawner.waves){
            sum += i;
        }
        // Debug.Log("SUM IS"+ sum);
        // Debug.Log("HP IS"+ houseHp);
        if (houseHp <= 0){
            // set active stage defeat menu
            background.SetActive(true);
            defeatMenu.SetActive(true);
        }
        // Max enemy spawn (calculate this) and number of killed enemies (calculate this? idk lol)
        if (sum == enemySpawner.enemiesKilled) {
             // set active stage victory menu
            background.SetActive(true);
            victoryMenu.SetActive(true);
        }
        
    }
    
    public void replay() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void victory_next_scene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void defeat_main_menu() {
        SceneManager.LoadScene(0);
    }

}
