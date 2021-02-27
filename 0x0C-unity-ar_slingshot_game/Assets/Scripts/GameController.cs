﻿using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// Defines if the player is playing or not
    /// </summary>
    public static bool gameStarted = false;
    public static int score = 0;
    /// <summary>
    /// Defines the number of target to create when the game starts
    /// </summary>
    public int targetNumbers = 5;
    /// <summary>
    /// Plane used for dev purposes / at run assigned to the real ARPlane
    /// </summary>
    public GameObject plane;
    /// <summary>
    /// Target prefab used to init targets
    /// </summary>
    public GameObject targetPrefab;

    /// <summary>
    /// GameObject to be enabled when the game starts
    /// </summary>
    public List<GameObject> gameUI;
    /// <summary>
    /// Used to show the leaderboard when the game is over and contains an anchored button to play again without starting a new ARplane detection.
    /// </summary>
    public GameObject leaderBoard;

    /// <summary>
    /// Material used to make the ARPlane invisible
    /// </summary>
    public Material hide;
    /// <summary>
    /// Enables development mode which enable plane and ui for testing purpose
    /// </summary>
    public bool devMode = false;
    // Plane mesh surface
    private NavMeshSurface PlaneNavMesh;

    // Start is called before the first frame update
    void Start()
    {
        if (devMode)
        {
            // UI to block
            var filterUI = new List<string> { "LeaderBoard", "StartPanel", "BottomPanel" };
            // Simulates UI used when the game starts
            foreach (Transform child in GameObject.Find("UI").transform)
                child.gameObject.SetActive(!filterUI.Contains(child.gameObject.name));

            StartGame();
            plane.SetActive(true);

        }
    }

    public void PlayAgain()
    {
        // Resets game score to 0
        score = 0;
        GameObject.Find("Ammo").GetComponent<BallController>().ammo = 7;
        // Resets ammo UI
        foreach (Transform child in GameObject.Find("List").transform)
            child.gameObject.SetActive(true);
        // Cleaning up all targets
        foreach (Transform child in plane.transform)
            Destroy(child.gameObject);

        StartGame();
        leaderBoard.SetActive(false);
        GameObject.Find("StartSound").GetComponent<AudioSource>().Play();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        gameStarted = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame()
    {
        gameStarted = true;
        GameObject.Find("StartSound").GetComponent<AudioSource>().Play();
        // GameObject.Find("StartPanel").SetActive(false);
        // Get the ARPlane, works only if dev mode is not active
        if (!devMode && !gameStarted)
        {
            plane = PlaneController.gamePlane.gameObject;
            // Disables the startpanel
            GameObject.Find("StartPanel").SetActive(false);
            // Disables ARplane rendering
            plane.GetComponent<MeshRenderer>().material = hide;
            // Adding NavMeshSurface to ARPlane
            PlaneNavMesh = plane.AddComponent<NavMeshSurface>();
            // Building Mesh on Runtime
            PlaneNavMesh.BuildNavMesh();
        }
        
        // Position the ball in the center of the screen for first launch
        GameObject.Find("Ammo").GetComponent<BallController>().Reload(false);

        foreach (var UI in gameUI)
            UI.SetActive(true);

        initTargets();
    }

    public void initTargets()
    {
        for (int i = 0; i < targetNumbers; i++)
        {
            // 0.5f is the agent radius
            Vector3 randomDir = Random.insideUnitSphere * 1.6f;
            randomDir.x += plane.transform.position.x;
            randomDir.z += plane.transform.position.z;

            GameObject t = Instantiate(targetPrefab, Vector3.zero, Quaternion.identity, plane.transform) as GameObject;
            t.GetComponent<NavMeshAgent>().Warp(new Vector3(randomDir.x, plane.transform.position.y, randomDir.z));
            t.transform.localPosition += new Vector3(0, 0.1f, 0);
            t.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
