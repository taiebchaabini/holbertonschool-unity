using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Gets animator of the player when the game starts
    static Animator anim;
    // Using camera gameobject
    public GameObject camera;

    /// <summary>
    /// Controls player speed
    /// </summary>
    public float speed = 10.0f;
    // Rigidbody of the player
    private Rigidbody rb;
    // Player 3D MODEL gameobject
    private GameObject model;
    /// <summary>
    /// Force of player jump
    /// </summary>
    public float jumpForce = 16.0f;
    // Checks if the player is grounded or not (used for jump)
    private bool isGrounded;
    Vector3 direction;
    // Model rotation value (y)
    private float modelRotationX;
    private float modelRotationZ;
   

    // Start is called before the first frame update
    void Start()
    {
        model = GameObject.Find("ty");
        rb = GetComponent<Rigidbody>();
        isGrounded = true;
        anim = GetComponent<Animator>();
    }


    // Update is called each frame
    void Update(){
        
      
         
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Getting Up") ||
        anim.GetCurrentAnimatorStateInfo(0).IsName("Falling Flat Impact") ||
        anim.GetCurrentAnimatorStateInfo(0).IsName("Falling"))
        {
            direction = Vector3.zero;
        }
        
        if (isGrounded){
            anim.SetBool("isFalling", false);
        }

        if (transform.position.y < -3f){
            anim.SetBool("isFalling", true);
        }

        if(anim.GetBool("isFalling") == true){
            direction = Vector3.zero;
            anim.SetBool("isIddle", false);
        }
      
        
        if (transform.position.y < -30f){
            transform.position = (new Vector3(0, 80f, 0));
            transform.rotation = Quaternion.identity;
            isGrounded = false;
        }
      
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space)){
            if (isGrounded){
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                anim.SetTrigger("isJumping");
            }

            
        }
      
         
 
           
    }

    void LateUpdate(){
     

    }



    // FixedUpdate is called according to framerate
    void FixedUpdate(){
        if (direction != Vector3.zero) {
                model.transform.rotation = Quaternion.LookRotation(Quaternion.Euler(0.0f, camera.transform.localEulerAngles.y, 0.0f) * direction);
        }

        if (direction.x != 0 || direction.z != 0){
            Vector3 v = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(v.x, camera.transform.rotation.eulerAngles.y, v.z);
        }

        MoveCharacter(direction);
      
        if (!isGrounded){
            Vector3 newVel = rb.velocity;
            newVel.y -= (jumpForce * 2) * Time.deltaTime;
            rb.velocity = newVel;
        }
            
    }

    void MoveCharacter(Vector3 movement){
        //rb.rotation = CameraController.GameObject.GetComponent<CameraController>().transform.localRotation;
        
        if (movement != Vector3.zero){
            anim.SetBool("isRunning", true);
            anim.SetBool("isIddle", false);
        } else{
            anim.SetBool("isRunning", false);
            anim.SetBool("isIddle", true);
        }

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
