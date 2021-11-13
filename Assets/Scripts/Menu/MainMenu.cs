using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void play_game() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void play_tutorial(){
        SceneManager.LoadScene(7);
    }

    public void quit_game() {
        //Debug.Log("Nag quit na po hehehe @_@");
        Application.Quit();
    }

}
