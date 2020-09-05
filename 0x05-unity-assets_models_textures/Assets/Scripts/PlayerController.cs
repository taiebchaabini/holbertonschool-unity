using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Controls player speed
    public float speed = 10.0f;
    // Rigidbody of the player
    private Rigidbody rb;
    // Force of player jump
    public float jumpForce = 16.0f;
    // Checks if the player is grounded or not (used for jump)
    private bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = true;
    }


    // Update is called each frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){
            if (isGrounded){
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }        
           
    }


    // FixedUpdate is called according to framerate
    void FixedUpdate()
    {
        float translationX = Input.GetAxis("Horizontal") * speed;
        float translationZ = Input.GetAxis("Vertical") * speed;


        translationX *= Time.deltaTime;
        translationZ *= Time.deltaTime;

        if (translationX != 0 || translationZ != 0){
            transform.Translate(translationX, 0, translationZ);
        }
        if (!isGrounded){
            Vector3 newVel = rb.velocity;
            newVel.y -= (jumpForce * 2) * Time.deltaTime;
            rb.velocity = newVel;
        }
            
    }


    // Fired when the player collide with another object
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.parent.name == "Platforms"){
            isGrounded = true;
        }
    }
    

    // Fired when the player gets out of a collission
    void OnCollisionExit(Collision collision) {
            isGrounded = false;
    }

  
}
