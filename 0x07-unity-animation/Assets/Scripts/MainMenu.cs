using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Selects game level from the game menu.
    /// </summary>
    /// <param name="level">Number of level to load</param>
    public void LevelSelect(int level){
        SceneManager.LoadScene(level);
    }

    /// <summary>
    /// Quits the game
    /// </summary>
    public void Exit(){
        Debug.Log("Exited");
        Application.Quit();
    }
    /// <summary>
    /// Access to option menu.
    /// </summary>
    public void Options(){
        PlayerPrefs.SetString("__lastScene__", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Options");
    }


}
