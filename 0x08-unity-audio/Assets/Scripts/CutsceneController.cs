using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    // Player GameObject
    private GameObject player;
    // Player main camera
    public GameObject camera;
    // TimerCanvas GameObject
    public GameObject timercanvas;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    /// <summary>
    /// Start game after animation
    /// </summary>
    public void startGame(){
        player.GetComponent<PlayerController>().enabled = true;
        camera.gameObject.SetActive(true);
        timercanvas.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
