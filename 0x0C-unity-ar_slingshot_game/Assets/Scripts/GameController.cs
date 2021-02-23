using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
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

    public Material hide;
    public bool devMode = false;
    public static bool gameStarted = false;

    // Plane mesh surface
    private NavMeshSurface PlaneNavMesh;

    // Start is called before the first frame update
    void Start()
    {
        if (devMode)
        {
            StartGame();
            plane.SetActive(true);
        }
    }

    public void RestartGame()
    {
        gameStarted = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayAgain()
    {
        // Resets game score to 0
        score = 0;
        // Resets ammo 
        GameObject.Find("Ammo").GetComponent<BallController>().ammo = 7;
        // Resets ammo UI
        foreach (Transform child in GameObject.Find("List").transform)
            child.gameObject.SetActive(true);
        // Cleaning up all targets
        foreach (Transform child in plane.transform)
            Destroy(child.gameObject);

        StartGame();
        GameObject.Find("PlayAgain").SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        gameStarted = true;
        // GameObject.Find("StartPanel").SetActive(false);
        // Get the ARPlane, works only if dev mode is not active
        if (!devMode)
            plane = PlaneController.gamePlane.gameObject;

        // Disables the startpanel
        GameObject.Find("StartPanel").SetActive(false);
        foreach (var UI in gameUI)
            UI.SetActive(true);
        // Adding NavMeshSurface to ARPlane
        PlaneNavMesh = plane.AddComponent<NavMeshSurface>();
        // Building Mesh on Runtime
        PlaneNavMesh.BuildNavMesh();

        // Disables ARplane rendering
        plane.GetComponent<MeshRenderer>().material = hide;

        for (int i = 0; i < targetNumbers; i++)
        {
            // 0.5f is the agent radius
            Vector3 randomDir = Random.insideUnitSphere * 0.6f;
            randomDir.x += plane.transform.position.x;
            randomDir.z += plane.transform.position.z;

            GameObject t = Instantiate(targetPrefab, Vector3.zero, Quaternion.identity, plane.transform) as GameObject;
            t.GetComponent<NavMeshAgent>().Warp(new Vector3(randomDir.x, plane.transform.position.y, randomDir.z));
            t.transform.localPosition += new Vector3(0, 0.1f, 0);
            t.GetComponent<NavMeshAgent>().enabled = true;
        }

    }
}
