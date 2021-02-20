using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;
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

    public  Material hide;
    public bool devMode = false;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        // GameObject.Find("StartPanel").SetActive(false);
        // Get the ARPlane, works only if dev mode is not active
        if (!devMode)
            plane = PlaneController.gamePlane.gameObject;
        
        
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
