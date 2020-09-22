using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generate : MonoBehaviour
{
    // Start is called before the first frame update
    // Define the first parent cube (where the player start)
    public GameObject pcube;
    // Used for future cubes
    private GameObject cube;
    private GameObject lastCube;
    // Used for last cube position on the Z
    float lastPos;
    float pcubeX;
    void Start()
    {
        // Generation of the right one
        for (int i = 0; i < 13; i++){
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.parent = pcube.transform.parent.transform;
            if (i == 0){
                lastPos = pcube.transform.position.z + -7f;
                pcubeX = pcube.transform.position.x + 7f;
            }
            else{
                lastPos = lastCube.transform.position.z;
            }
            cube.transform.position = new Vector3(pcubeX + (i * 2f), pcube.transform.position.y, lastPos);
            lastCube = cube;
        }
        // Generation of the left one
        for (int i = 0; i < 13; i++){
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.parent = pcube.transform.parent.transform;
            if (i == 0){
                lastPos = pcube.transform.position.z + 7f;
                pcubeX = pcube.transform.position.x + 7f;
            }
            else{
                lastPos = lastCube.transform.position.z;
            }
            cube.transform.position = new Vector3(pcubeX + (i * 2f), pcube.transform.position.y, lastPos);
            lastCube = cube;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
