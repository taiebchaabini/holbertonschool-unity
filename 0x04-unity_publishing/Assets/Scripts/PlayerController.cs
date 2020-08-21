using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    /// <summary>Speed movement of the player.</summary>
    public float speed = 20f;
    // Force used with player movement.
    private float sideWayForce = 100f;
    // Current player score.
    private int score = 0;
    /// <summary>Health of the player.</summary>
    public int health = 5;
    /// <summary>Rigidbody of the player.</summary>
    public Rigidbody rb;
    /// <summary>Used to display the score in UI, works with the function SetScoreText().</summary>
    public Text ScoreText;
    /// <summary>Used to display the score in UI, works with the function SetHealthText().</summary>
    public Text HealthText;
    // Shows victory or lose
    private GameObject WinLoseBG;
    // Start is called before the first frame update
    void Start()
    {
      WinLoseBG = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
    }

    // Updates each frame rate
    void Update(){
        if (health == 0){
            WinLoseBG.SetActive(true);
            WinLoseBG.transform.GetChild(0).GetComponent<Text>().text = "Game Over!";
            WinLoseBG.transform.GetChild(0).GetComponent<Text>().color = Color.white;
            StartCoroutine(LoadScene(3));
        }
        SetScoreText();
        SetHealthText();
    }
    // FixedUpdate has the frequency of the physics system; it is called every fixed frame-rate frame
    void FixedUpdate()
    {
        if ( Input.GetKey("w")){
            rb.AddForce(0, 0, sideWayForce * speed * Time.deltaTime);
        }
        if ( Input.GetKey("s")){
            rb.AddForce(0, 0, -sideWayForce * speed * Time.deltaTime);
        }
        if ( Input.GetKey("d")){
            rb.AddForce(sideWayForce * speed * Time.deltaTime, 0, 0);
        }
        if ( Input.GetKey("a")){
            rb.AddForce(-sideWayForce * speed * Time.deltaTime, 0, 0);
        }
    }

    // Increments the value of score when the Player touches an object tagged Pickup
    void OnTriggerEnter(Collider other){
        if (other.tag == "Pickup"){
            score += 1;
            Destroy(other.gameObject);
        }
        if (other.tag == "Trap"){
            health -= 1;
        }
        if (other.tag == "Goal"){
            WinLoseBG.SetActive(true);
            WinLoseBG.transform.GetChild(0).GetComponent<Text>().text = "You Win!";
            WinLoseBG.transform.GetChild(0).GetComponent<Text>().color = Color.black;
            WinLoseBG.GetComponent<Image>().color = Color.green;
            StartCoroutine(LoadScene(3));
        }
    }

    /// <summary>
    /// Updates the score in the UI.
    /// </summary>
    void SetScoreText()
    {
        this.ScoreText.text = $"Score: {score}";
    }

    /// <summary>
    /// Updates the score in the UI.
    /// </summary> 
    void SetHealthText()
    {
        this.HealthText.text = $"Health: {health}";
    }
    /// <summary>
    /// Coroutine to reload the scene after number of seconds
    /// </summary>
    /// <param name="seconds">Number of seconds before reloading the scene</param>
    /// <returns></returns>
    IEnumerator LoadScene(float seconds){
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
