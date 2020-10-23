using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    private void Update(){

    }
    private void Start() {
        if (PlayerPrefs.GetString("__isInverted__") == "true"){
            GameObject.Find("InvertYToggle").GetComponent<Toggle>().isOn = true;
        }
        if (PlayerPrefs.GetString("__BGMSlider__").Length != 0){
            GameObject.Find("BGMSlider").GetComponent<Slider>().value = float.Parse(PlayerPrefs.GetString("__BGMSlider__"));
        }
        if (PlayerPrefs.GetString("__SFXSlider__").Length != 0){
            GameObject.Find("SFXSlider").GetComponent<Slider>().value = float.Parse(PlayerPrefs.GetString("__SFXSlider__"));
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
        float BGMSlider = GameObject.Find("BGMSlider").GetComponent<Slider>().value;
        float SFXSlider = GameObject.Find("SFXSlider").GetComponent<Slider>().value;
        
        PlayerPrefs.SetString("__isInverted__", "false");
        PlayerPrefs.SetString("__BGMSlider__", BGMSlider.ToString());
        PlayerPrefs.SetString("__SFXSlider__", SFXSlider.ToString());
        if (isInverted){
            PlayerPrefs.SetString("__isInverted__", "true");
        }
    }
}
