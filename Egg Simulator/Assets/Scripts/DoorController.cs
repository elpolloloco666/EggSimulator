using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class DoorController : MonoBehaviour
{
    
    public playerDataSO playerData;
    
    public Animator CountDownAnimator;
    public TMP_Text countDown;

    private Transform playerTransform;
    private float distance;
    private Vector3 distanceVector;
    private Animator doorAnimator;
    private int time = 5;
    private float timeToOpen = 5f;
    private bool running = false;
    private bool isOpen = false;

    [SerializeField] UnityEvent openEvent;
    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        distanceVector = transform.position - playerTransform.position;
        distance = distanceVector.magnitude;

        if ( distance <= 3.5 && playerData.hasKey && !isOpen)
        {
           
            
            if (timeToOpen > 0)
            {
                CountDownAnimator.SetBool("show", true);
                running = true;
                timeToOpen = timeToOpen - Time.deltaTime;
                countDown.text = Mathf.Round(timeToOpen).ToString();
            }
            else
            {
                doorAnimator.SetTrigger("open");
                openEvent.Invoke();
                running = false;
                CountDownAnimator.SetBool("show", false);
                isOpen = true;
                
            }

        }



        if (running && distance > 3.5)
        {
            
            running = false;
            timeToOpen = 5f;
            CountDownAnimator.SetBool("show", true);
            
        }
    }

    


}
