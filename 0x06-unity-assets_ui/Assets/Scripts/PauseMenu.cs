using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Determines if the game is paused or not.
    private bool gamePaused;
    private void Start() {
        gamePaused = false;
    }
    private void Update() {
        // Pause the game when the user press the Cancel button.
        if (Input.GetButtonDown("Cancel")){
            if (gamePaused){
                Resume();
            } else{
                Pause();
            }
           
        }
    }
    /// <summary>
    /// Pause the game.
    /// </summary>
    public void Pause(){
        gamePaused = true;
        Time.timeScale = 0;
        this.GetComponent<Canvas>().enabled  = true;
    }
    /// <summary>
    /// Resume the game.
    /// </summary>
    public void Resume(){
        gamePaused = false;
        Time.timeScale = 1;
        this.GetComponent<Canvas>().enabled  = false;
    }
    /// <summary>
    /// Restart the game.
    /// </summary>
    public void Restart(){
        Scene currentLevel = SceneManager.GetActiveScene();
        Resume();
        SceneManager.LoadScene(currentLevel.buildIndex);
    }

    /// <summary>
    /// Load the main menu.
    /// </summary>
    public void MainMenu(){
        Resume();
        SceneManager.LoadScene("MainMenu");
    }
    /// <summary>
    /// Loads the options.
    /// </summary>
    public void Options(){
        Resume();
        SceneManager.LoadScene("Options");
    }
    
}
