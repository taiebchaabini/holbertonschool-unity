using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    /// <summary>
    /// Loads the last scene
    /// </summary>
    public void Back(){
        var lastScene = PlayerPrefs.GetString("__lastScene__");
        SceneManager.LoadScene(lastScene);
    }
}
