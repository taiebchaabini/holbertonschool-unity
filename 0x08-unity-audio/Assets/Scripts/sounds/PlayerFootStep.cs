using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerFootStep : MonoBehaviour
{
    /// <summary>
    /// Used when the Player runs on a grassy platform, footsteps_running_grass should play
    /// </summary>
    public AudioSource runningGrass;
    /// <summary>
    /// Used when the Player hits the ground from falling off the platforms and restarting
    /// </summary>
    public AudioSource falling;

    // Occurs when player's feet touch the ground.
    private void Step(){
        runningGrass.Play();
    }

    // Occurs when player's feet stops touching the ground.
    private void StepStop(){
        if (runningGrass.isPlaying){
            runningGrass.Pause();
        }
    }
    // Occurs when player fall down from the sky and hit the ground.
    private void Falling(){
        falling.Play();
    }

}
