using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    /// <summary>
    /// Default timer for the game
    /// </summary>
    public Text TimerText;
    // Timer variable
    private float timer;
    private decimal minutes;
    private String seconds;
    private String milliseconds;


    /// // Start is called before the first frame update
    void Start()
    {
        minutes = 0;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // Get seconds
        seconds = Mathf.Round(timer).ToString("00");
        if (seconds == "60"){
            timer -= 60;
            // Every 60 seconds increment and reset seconds
            minutes += 1;
        }
        // Seconds + milliseconds
        milliseconds = timer.ToString("00.00");
        // Get milliseconds only
        milliseconds = milliseconds[3].ToString() + milliseconds[4].ToString();

        TimerText.text = $"{minutes}:{seconds}:{milliseconds}";

    }

    /// <summary>
    /// Updates the WinCanvas text when the payers win.
    /// </summary>
    public void Win(){
        GameObject.Find("FinalTime").GetComponent<Text>().text = TimerText.text;
    }
}
