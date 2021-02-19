﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulatePhysics : MonoBehaviour
{
    /// <summary>
    /// Object to be simulate, used for COPY
    /// </summary>
    public GameObject subject;
    /// <summary>
    /// Used to draw the prediction line / subject's line renderer
    /// </summary>
    public LineRenderer line;
    // GameObject used for the physic simulation, as a copy of the subject
    private GameObject ball;
    // Current game scene
    private Scene currentScene;
    // Prediction scene, used to simulate physics
    private Scene predictionScene;
    // Current game scene physics 
    private PhysicsScene currentScenePhysics;
    // Prediction game scene physics
    private PhysicsScene predictionScenePhysics;
    // Prediciton Scene parameters
    private CreateSceneParameters sceneParams; 

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        currentScenePhysics = currentScene.GetPhysicsScene();

        sceneParams = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        predictionScene = SceneManager.CreateScene("Prediction", sceneParams);
        
        predictionScenePhysics = predictionScene.GetPhysicsScene();
    }

    private void start()
    {
        line = subject.GetComponent<LineRenderer>();
    }


    private void FixedUpdate()
    {
        if (currentScenePhysics.IsValid())
            currentScenePhysics.Simulate(Time.fixedDeltaTime);
        
    }

    public void Launch(Vector3 dir, GameObject target)
    {
        target.GetComponent<Rigidbody>().AddForce(dir, ForceMode.Impulse);
        target.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 2f), ForceMode.Impulse);
    }
    public void LinePrediction(Vector3 direction, Vector3 currentPosition)
    {
        if (ball == null)
        {
            ball = Instantiate(subject);
            SceneManager.MoveGameObjectToScene(ball, predictionScene);
        }

       Launch(direction, ball);
       
       int step = 50;
       line.positionCount = step;
       line.SetPosition(0, ball.transform.localPosition);

       for (int i = 1; i < step; i++)
       {
           predictionScenePhysics.Simulate(Time.deltaTime);
           line.SetPosition(i, ball.transform.localPosition);
       }
       Destroy(ball);
    }
}
