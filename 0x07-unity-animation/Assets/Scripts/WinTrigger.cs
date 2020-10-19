using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        // Detects when the player exit the TimeTrigger object
        if (other.name == "Player"){
            other.GetComponent<Timer>().TimerText.fontSize = 60;
            other.GetComponent<Timer>().TimerText.color = Color.green;
            if (other.GetComponent<Timer>().TimerText && other.GetComponent<Timer>().TimerText.enabled == true)
                other.GetComponent<Timer>().enabled = false;
            if (GameObject.Find("WinCanvas").GetComponent<Canvas>().enabled == false){
                GameObject.Find("Player").GetComponent<Timer>().Win();
                GameObject.Find("WinCanvas").GetComponent<Canvas>().enabled = true;
                Destroy(GameObject.Find("TimerText"));
            }

        }
    }
}
