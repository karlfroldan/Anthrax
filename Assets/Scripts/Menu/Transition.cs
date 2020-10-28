using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Transition : MonoBehaviour{

    void Update(){
        Actor actor = GameObject.Find("Actor").GetComponent<Actor>();
        EnemySpawner enemy_spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        GameObject mycanvas = GameObject.Find("StarterMenu");
    
        // Debug.Log(mycanvas);
        // if(mycanvas != null){
        //     Debug.Log("NOT Null");
        // }

        if (actor.health <= 0){
            // set active stage defeat menu
            Debug.Log("Victory");
            mycanvas.SetActive(true);
        }

        // Max enemy spawn (calculate this) and number of killed enemies (calculate this? idk lol)
        if (enemy_spawner.enemiesKilled - enemy_spawner.numEnemy == 0) {
             // set active stage vitory menu
            Debug.Log("Defeat"); 
            mycanvas.SetActive(true);
        }
        
    }
    
    public void victory_replay() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void victory_next_scene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void defeat_replay() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void defeat_main_menu() {
        SceneManager.LoadScene(0);
    }
}
