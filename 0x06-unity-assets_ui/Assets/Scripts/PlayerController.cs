using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Controls player speed
    /// </summary>
    public float speed = 10.0f;
    // Rigidbody of the player
    private Rigidbody rb;
    /// <summary>
    /// Force of player jump
    /// </summary>
    public float jumpForce = 16.0f;
    // Checks if the player is grounded or not (used for jump)
    private bool isGrounded;
    Vector3 direction;

   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = true;
    }


    // Update is called each frame
    void Update(){
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (transform.position.y < -20f){
            transform.position = (new Vector3(0,20f,0));
            transform.rotation = Quaternion.identity;
            isGrounded = false;
        }
      
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space)){
            //Debug.Log("Space used");
            if (isGrounded){
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
         
 
           
    }


    // FixedUpdate is called according to framerate
    void FixedUpdate(){
        MoveCharacter(direction);
      
        if (!isGrounded){
            Vector3 newVel = rb.velocity;
            newVel.y -= (jumpForce * 2) * Time.deltaTime;
            rb.velocity = newVel;
        }
            
    }

    void MoveCharacter(Vector3 movement){
        //rb.rotation = CameraController.GameObject.GetComponent<CameraController>().transform.localRotation;
        movement = transform.TransformDirection(movement);
        rb.MovePosition(transform.position + (movement * speed * Time.deltaTime));
    }
    // Fired when the player collide with another object
    void OnCollisionEnter(Collision collision)
    {

        if (isGrounded == false){
            isGrounded = true;
        }
    }
    

    // Fired when the player gets out of a collission
    void OnCollisionExit(Collision collision) {
   
            isGrounded = false;
    }



  
}
