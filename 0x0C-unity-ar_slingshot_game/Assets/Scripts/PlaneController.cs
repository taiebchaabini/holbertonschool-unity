using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using TMPro;
public class PlaneController : MonoBehaviour
{
    // public GameObject ARRaycastManager;

    /// <summary>
    /// AR Session, raycast manager
    /// </summary>
    [SerializeField]
    ARRaycastManager m_RaycastManager;
    /// <summary>
    /// AR Session, plane manager
    /// </summary>
    [SerializeField]
    ARPlaneManager m_ARPlaneManager;
    /// <summary>
    /// Bottom text user for UI
    /// </summary>
    public TMP_Text bottomText;
    /// <summary>
    /// Start button used to init the game
    /// </summary>
    public GameObject StartPanel;
    /// <summary>
    /// Plane on which the game will be played, also used for placing targets in gameController script
    /// </summary>
    public static ARPlane gamePlane = null;
    /// <summary>
    /// Main ball of the game
    /// </summary>
    public GameObject ammo;
    // Used with raycasting for plane selection
    private List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
    // List of available planes
    private List<ARPlane> currentPlanes = new List<ARPlane>();
    // Determines if one or more planes are detected since the game has start
    private bool planeDetected = false;


    // Start is called before the first frame update
    void Start()
    {
        bottomText.text = "Searching for a surface...";
    }

    void Update()
    {
        if (!planeDetected && m_ARPlaneManager.trackables.count > 0)
        {
            // Changing UI text when a plane is selectable for a better user experience
            planeDetected = true;
            bottomText.text = "SELECT A PLANE";
            // Sets bottomPanel color to green
            bottomText.transform.parent.GetComponent<Image>().color = new Color(0.3176471f, 0.7490196f, 0.2784314f, 0.8f);
        }

        if (Input.touchCount == 0)
            return;

        if (!GameController.gameStarted && m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits, TrackableType.PlaneWithinPolygon))
        {
            // Removes bottom panel text
            bottomText.text = "";
            // Disable the bottom text UI for better UX
            bottomText.transform.parent.gameObject.SetActive(false);
            // Enables start feature here
            StartPanel.SetActive(true);
            // Get the selected gamePlane
            gamePlane = m_ARPlaneManager.GetPlane(m_Hits[0].trackableId);
            // Disables collision between the ball and ARplane
            Physics.IgnoreCollision(gamePlane.GetComponent<Collider>(), ammo.GetComponent<Collider>());
            // Disables other planes
            foreach (var plane in m_ARPlaneManager.trackables)
            {
                if (plane != gamePlane)
                    plane.gameObject.SetActive(false);
            }
            // Disables plane detection
            m_ARPlaneManager.enabled = false;
        }

    }
}
