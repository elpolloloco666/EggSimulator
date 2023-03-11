using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public Animator playerAnimator;
    public playerDataSO playerData;
    public Rigidbody rb;
    public GameObject hand;

    private bool isJumping = false;
    private bool applyForce = false;
    private bool isPickingUp = false;
    private float delay = 0.5f;
    private int clicks = 0;
    private float lastClick = 0;
    private GameObject prop;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerData.hasAProp = false;
        playerData.life = 100;
    }

    
    void Update()
    {
        ////////////////////////////////////////////////MOVEMENT//////////////////////////////////////////////////////
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

        if (applyForce) transform.position = transform.position + transform.forward * Time.deltaTime * 2f;

        if (Input.GetKeyDown(KeyCode.E) && !playerData.hasAProp)
        {
            playerAnimator.SetTrigger("pickUp");
            isPickingUp = true;
        } else
        if(Input.GetKeyDown(KeyCode.E) && playerData.hasAProp)
        {

            prop.transform.parent = null;
            prop.GetComponent<BoxCollider>().isTrigger = false;
            prop.GetComponent<Rigidbody>().isKinematic = false;
            playerData.hasAProp = false;
            prop = null;
            
        }

        if(Input.GetKeyDown(KeyCode.R) && playerData.hasAProp)
        {
            playerAnimator.SetTrigger("throw");
        }
        
        
        

        ////////////////////////////////////////////////ATTACKS/////////////////////////////////////////////////////////
        if (Time.time - lastClick > delay && !playerData.hasAProp)
        {
            clicks = 0;
            playerAnimator.ResetTrigger("attack2");
            playerAnimator.ResetTrigger("attack3");
            playerData.isAttacking = false;
        }

        if (Input.GetMouseButtonDown(0) && !playerData.hasAProp)
        {
            lastClick = Time.time;
            clicks++;
            playerData.isAttacking = true;

            
            if(clicks == 1)
            {
                playerAnimator.SetTrigger("attack1");
            }
            

            clicks = Mathf.Clamp(clicks, 0, 3);

        }

        if (Input.GetMouseButtonDown(0) && playerData.hasAProp)
        {
            playerAnimator.SetTrigger("sword-attack");
            
        }

    }

    public void jumpforce()
    {
        rb.AddForce(Vector3.up*5f , ForceMode.Impulse);
        applyForce = true;
    }

    public void attack2()
    {
        if (clicks >= 2)
        {
            playerAnimator.SetTrigger("attack2");
        }

    }

    public void attack3()
    {
        if (clicks >= 3)
        {
            playerAnimator.SetTrigger("attack3");
        }

    }

    public void resetCombo()
    {
        clicks = 0;
    }

    public void EndPickUp()
    {
        isPickingUp = false;
    }

    public void Throw()
    {
        prop.transform.parent = null;
        prop.GetComponent<BoxCollider>().isTrigger = false;
        prop.GetComponent<Rigidbody>().isKinematic = false;
        prop.GetComponent<Rigidbody>().AddForce(transform.forward * 8f + Vector3.up * 4f, ForceMode.Impulse);
        if (prop.GetComponent<PropController>().isFood) prop.tag = "food"; 
        playerData.hasAProp = false;
        prop = null;
    }

    public void StartPropAttack()
    {
        playerData.isAttacking = true;
    }

    public void EndPropAttack()
    {
        playerData.isAttacking = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isJumping = false;
        applyForce = false;
     
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.transform.CompareTag("prop") || other.transform.CompareTag("food")) && isPickingUp)
        {
            prop = other.gameObject;
            prop.tag = "prop";
            other.GetComponent<BoxCollider>().isTrigger = true;
            other.transform.SetParent(hand.transform);
            other.transform.localPosition = other.GetComponent<PropController>().grabbedPosition;
            other.transform.localRotation = Quaternion.Euler(other.GetComponent<PropController>().grabbedRotation);
            other.GetComponent<Rigidbody>().isKinematic = true;
            playerData.hasAProp = true;
        }
    }
}
