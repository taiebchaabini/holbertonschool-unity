using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragController : MonoBehaviour, IDragHandler
{
    // Will be spawned at drag position
    public GameObject ammo;
    // MainCamera helps to determine the new drag position
    private Camera cam;
    private Vector3 newPos;
    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }
    public void OnDrag(PointerEventData eventData)
    {
        //  ammo.transform.position = camera.transform.position + camera.transform.forward;
        newPos = eventData.position;
        newPos.z = 3f;

        newPos = cam.ScreenToWorldPoint(newPos);
        // Debug.Log(eventData.position);
        // Debug.Log(newPos);
        ammo.GetComponent<Rigidbody>().isKinematic = true;
        ammo.transform.position = newPos;
    }


    // Update is called once per frame
    void Update()
    {
    }
}
