using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Player speed
    /// </summary>
    public float speed;

    /// <summary>
    /// Handles player animations.
    /// </summary>
    public Animator animator;
    /// <summary>
    /// Player rigidbody 2D.
    /// </summary>
    public Rigidbody2D rb;
    // Handles player Horizontal input
    private float move;
    // Handles if the player jumped or not.
    private float jump;
    // Old rotation values
    private Quaternion oldRotation;
    // Previous transform position
    private Vector3 previous;
    // Actual velocity, based on TimedeltaTime;
    private float velocity;
    // checks if the player is grounded

    private bool isGrounded;

    private GameObject mainCamera;



    // Start is called before the first frame update
    void Start()
    {
        move = 10f;
        isGrounded = true;
        mainCamera = GameObject.Find("Main Camera"); 
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");
        if (transform.position.x - mainCamera.transform.position.x <= -2.59428 && move <= 0)
            move = 0;

        jump = Input.GetAxis("Jump");
    }

    void FixedUpdate(){
        // FixedUpdate is called depending on thed computer
              
        if (move < 0){
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else if (move > 0){
            transform.rotation = Quaternion.identity;
        }
        
        /*
        //Debug.Log($"Actual rotation: {transform.rotation.y} vs {oldRotation.y}");
        if (velocity > 0 && oldRotation.y != transform.rotation.y){
        
            StartCoroutine(StartSkid());
            
            Debug.Log("Skied on");
        } 
        oldRotation = transform.rotation;
        */

        if (!animator.GetBool("PlayerSkid")){
            animator.SetFloat("Horizontal", move);
        }

        if (animator.GetBool("Skid") == false && move != 0){
            //rb.MovePosition(transform.position + new Vector3(move, 0, 0) * Time.deltaTime * speed);
            transform.Translate(Vector2.right * move * Time.deltaTime * speed, 0);
        }

        if (isGrounded == true && jump != 0){
             rb.AddForce((new Vector2(0,1) * 5), ForceMode2D.Impulse);
        }
        velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
        previous = transform.position;

    }

    // Handles skid animation
    IEnumerator StartSkid(){
        animator.SetTrigger("PlayerSkid");
        animator.SetBool("Skid", true);
    
        yield return new WaitForSeconds(0.5f);

        animator.SetBool("Skid", false);
    }

    // Handles player collision
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exit");
        isGrounded = false;
    }
    // Handles player collision
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter");
        isGrounded = true;
    }


    


}
