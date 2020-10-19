using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Player GameObject
    private GameObject player;
    // Mouse input
    private Vector2 mouseInput;
    // using Angles for rotations
    private Vector3 angles;
    // Camera default position
    private Vector3 cameraPos;
    // Camera new position
    private Vector3 cameraNewPos;
    /// <summary>
    /// Inverts mouse direction on the y.
    /// </summary>
    public bool isInverted;
    // Used for rotation on Y
    private float rotationY;
    // New position on Y
    private float mouseX;
    // New position on Z
    private float mouseY;
    // Getting 3D player model
    private GameObject model;
    // User for rotation
    private Quaternion rotation;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        cameraPos = transform.localPosition;
 
        if (PlayerPrefs.GetString("__isInverted__") == "true"){
          isInverted = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
       
      if (Input.GetMouseButton(1)){
            
            mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            if (isInverted){
              mouseInput.y = -mouseInput.y;
            }
   
            if (mouseInput.x != 0){
              mouseX += mouseInput.x * 2;
            }
            
            if (mouseInput.y != 0){
              mouseY += mouseInput.y * 2;
            }
            
      }
      mouseY = Mathf.Clamp(mouseY, -24f, 24f);
    }

    void LateUpdate(){
      rotation = Quaternion.Euler(new Vector3 (mouseY, mouseX, 0));
      cameraNewPos = player.transform.position + rotation * new Vector3(0.0f, 2.00f, -6.25f);
      transform.position = cameraNewPos;
      transform.LookAt(player.transform);
      
    }

    
}
