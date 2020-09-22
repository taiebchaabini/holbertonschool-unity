using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private void Start() {
        if (PlayerPrefs.GetString("__isInverted__") == "true"){
            GameObject.Find("InvertYToggle").GetComponent<Toggle>().isOn = true;
        }
    }
    /// <summary>
    /// Loads the last scene
    /// </summary>
    public void Back(){
        var lastScene = PlayerPrefs.GetString("__lastScene__");
        SceneManager.LoadScene(lastScene);
    }
    /// <summary>
    /// Saves changes in the option menu.
    /// </summary>
    public void Apply(){
        bool isInverted = GameObject.Find("InvertYToggle").GetComponent<Toggle>().isOn;
        PlayerPrefs.SetString("__isInverted__", "false");
        if (isInverted){
            PlayerPrefs.SetString("__isInverted__", "true");
        }
    }
}
