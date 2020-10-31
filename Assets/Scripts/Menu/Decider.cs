using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Decider : MonoBehaviour{
    
    public GameObject enemy_spawner;        // Drag EnemySpawner here???
    public GameObject house;        // Drag house GameObj here
    private float hp;       // Get house.hp property ???
    private int sum;        // add elements of EnemySpawner.waves[]
    private  int enemies_killed;        // get EnemySpawner.enemiesKilled property ???


    public GameObject victoryMenu;      
    public GameObject defeatMenu;       

    void Start(){

    }

    void Update(){

        if (this.hp <= 0){
            // set active stage defeat menu
            defeatMenu.SetActive(true);
        }

        // Max enemy spawn (calculate this) and number of killed enemies (calculate this? idk lol)
        if (this.sum - this.enemies_killed <= 0) {
             // set active stage victory menu
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
