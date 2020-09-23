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
       } else{
           int nextLevel = int.Parse(currentLevel) + 2;
           SceneManager.LoadScene(nextLevel);
       }
       
    }
}
