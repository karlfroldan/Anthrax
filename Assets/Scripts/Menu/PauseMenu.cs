using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour{
    public bool isPaused = false;

    //public Canvas PauseMenuUI = GameObject.Find("PauseMenu").GetComponent<Canvas>();
    
    public GameObject PauseMenuUI;

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (this.isPaused){
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume(){
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        this.isPaused = false;
    }

    public void Pause(){
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        this.isPaused = true;
    }

    public void LoadMenu(){
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
