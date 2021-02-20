using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class BallController : MonoBehaviour
{
    [SerializeField]
    public SimulatePhysics PredictionLineManager;

    public int ammo = 7;
    public float forceMultiplyer;

    private LineRenderer line;
    private bool mouseClick = false;
    private Vector3 mousePosition;
    private Vector3 mouseReleasePos;

    private Rigidbody rb;
    private GameObject Manager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        line = GetComponent<LineRenderer>();
        Manager = GameObject.Find("Manager");
    }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition;
        mouseClick = true;
    }

    public void FixedUpdate()
    {
        if (rb.velocity.z > 0f)
            line.positionCount = 0;

        if (transform.position.y < -3f)
        {
            // transform.position = new Vector3(0, -0.03f, 2.13f);
        }
    }

    public void OnCollisionEnter(Collision collision){
        if (collision.gameObject.name == "Target(Clone)")
        {
            GameController.score += 10;
            UIController.scoreText.text = GameController.score.ToString();
        }
        rb.isKinematic = true;
        transform.position = new Vector3(999, 999, 999);
        ammo -= 1;
        Destroy(UIController.list.transform.GetChild(ammo).gameObject);
    }

    public void Update()
    {
         if (mouseClick)
            PredictionLineManager.LinePrediction( (mousePosition - Input.mousePosition) * forceMultiplyer, transform.localPosition);
    }

    private void OnMouseUp()
    {
        mouseClick = false;
        PredictionLineManager.Launch((mousePosition - Input.mousePosition) * forceMultiplyer, transform.gameObject);
    }


}
