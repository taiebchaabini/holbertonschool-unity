using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Camera>().orthographicSize = 1.5f;
        transform.position = new Vector3(-1.53f, 0 ,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > transform.localPosition.x)
            transform.localPosition += new Vector3(0.016f, 0, 0);
    }
}
