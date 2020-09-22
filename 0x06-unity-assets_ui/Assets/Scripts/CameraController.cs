using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector2 mouseInput;
    private Vector3 angles;
    private Vector3 cameraPos;
    /// <summary>
    /// Inverts mouse direction on the y.
    /// </summary>
    public bool isInverted;
    private float rotationY;
    private float newPosY;
    private float newPosZ;
    // Start is called before the first frame update
    void Start()
    {
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
          
            angles = transform.localRotation.eulerAngles;
            // Rotation on the Y
            if (isInverted){
              mouseInput.y = -mouseInput.y;
            }
            rotationY = (angles.x + 180f) % 360f - 180f;
            rotationY = rotationY - mouseInput.y * 1;
            rotationY = Mathf.Clamp (rotationY, 3, 24f);
            transform.localRotation = Quaternion.Euler(new Vector3 (rotationY, angles.y, angles.z));

            // Limitation on the Y pos of the camera
            newPosY = Mathf.Clamp(cameraPos.y * (rotationY / 12), 0, 6f);
          
            // Limitation on the Z pos of the camera (zoom effect)
            newPosZ = Mathf.Clamp(newPosY - cameraPos.z, -4, -6);
            transform.localPosition = new Vector3(cameraPos.x, newPosY, newPosZ);

          

                          // Rotation on the X
            transform.parent.Rotate(Vector3.up, mouseInput.x * 1);
      }

    }

    
}
