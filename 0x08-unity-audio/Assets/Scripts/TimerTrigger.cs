using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    void OnTriggerExit(Collider other) {
        // Detects when the player exit the TimeTrigger object
        if (other.name == "Player"){
            if (other.GetComponent<Timer>().enabled == false)
                other.GetComponent<Timer>().enabled = true;
        }
    }
}
