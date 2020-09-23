using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinMenu : MonoBehaviour
{
    // Access to main menu
    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    // Access to the next level
    public void Next(){
       var currentLevel = SceneManager.GetActiveScene().name.Replace("Level0", "");
       if (currentLevel == "3"){
           MainMenu();
       }
       // No need to indent, build's index starts from 0 and the scene name starts from 1.
       SceneManager.LoadScene(int.Parse(currentLevel));
    }
}
