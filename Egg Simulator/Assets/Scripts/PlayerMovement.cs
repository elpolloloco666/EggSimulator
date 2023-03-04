using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public Animator playerAnimator;
    public Rigidbody rb;

    private bool isJumping = false;
    private bool applyForce = false;
    
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        

        playerAnimator.SetFloat("speed",Input.GetAxis("Vertical"));
        playerAnimator.SetFloat("direction",Input.GetAxis("Horizontal"));

        if (Input.GetKey(KeyCode.LeftShift)) playerAnimator.SetBool("running", true); 
        else playerAnimator.SetBool("running", false);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            playerAnimator.SetTrigger("jump");
            isJumping = true;
        }

        Physics.gravity = Vector3.up * playerAnimator.GetFloat("gravity");
        
        if(applyForce ) transform.position = transform.position + transform.forward * Time.deltaTime * 2.5f;

    }

    public void jumpforce()
    {
        applyForce = true;
               
    }

    private void OnCollisionEnter(Collision collision)
    {
        isJumping = false;
        applyForce = false;
    }
}
