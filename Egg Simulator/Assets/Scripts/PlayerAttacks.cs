using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{

    public Animator playerAnimator;

    private float delay = 0.5f;
    private int clicks = 0;
    private float lastClick = 0; 

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (Time.time - lastClick > delay)
        {
            clicks = 0;
            playerAnimator.ResetTrigger("attack2");
            playerAnimator.ResetTrigger("attack3");

        }

        if (Input.GetMouseButton(0))
        {
            lastClick = Time.time; 
            clicks++; 

            if(clicks == 1)
            {
                playerAnimator.SetTrigger("attack1");
                
            }

            clicks = Mathf.Clamp(clicks, 0, 3);
            
        }

        

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
}
