using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class MobileUI: MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    /// <summary>Rigidbody of the player.</summary>
    public Rigidbody rb;
    /// <summary>Rigidbody of the player.</summary>
    public PlayerController player;
    public bool horizontal;
    public bool vertical;
    public bool dir;
    private int direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // FixedUpdate
    void FixedUpdate(){
        if (vertical == true)
            rb.AddForce(0, 0, (direction * player.getSideWayForce()) * player.speed * Time.deltaTime);
        if (horizontal == true)
            rb.AddForce((direction * player.getSideWayForce()) * player.speed * Time.deltaTime, 0, 0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (dir == true){
            direction = 1;
        } else{
            direction = -1;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        direction = 0;
    }
}
