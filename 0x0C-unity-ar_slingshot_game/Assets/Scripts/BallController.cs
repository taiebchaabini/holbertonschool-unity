using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{

    private Vector3 mousePosition;
    private Vector3 mouseReleasePos;

    public int ammo = 7;

    private Rigidbody rb;

    public float forceMultiplyer;
    [SerializeField]
    public SimulatePhysics PredictionLineManager;

    private LineRenderer line;
    private bool mouseClick = false;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        line = GetComponent<LineRenderer>();
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
            transform.position = new Vector3(0, -0.03f, 2.13f);
            rb.isKinematic = true;
            rb.isKinematic = false;
        }

    
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
