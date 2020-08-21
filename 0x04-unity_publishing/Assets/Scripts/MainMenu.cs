using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Trap material
    /// </summary>
    public Material trapMat;
    /// <summary>
    /// Goal Material
    /// </summary>
    public Material goalMat;
    /// <summary>
    /// Color blind mode
    /// </summary>
    public Toggle colorblindMode;
    /// <summary>
    /// Starts the game 
    /// </summary>
    public void PlayMaze(){
        if (colorblindMode.isOn){
            trapMat.color = new Color32(255, 112, 0, 1);
            goalMat.color = Color.blue;
        } else{
            trapMat.color = Color.red;
            goalMat.color = Color.green;
        }
        SceneManager.LoadScene("maze");
    }
    /// <summary>
    /// Quits the game
    /// </summary>
    public void QuitMaze(){
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
