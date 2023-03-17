using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    
    public playerDataSO playerData;

    private Transform playerTransform;
    private float distance;
    private Vector3 distanceVector;
    private Animator doorAnimator;
    public bool running = false;

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        distanceVector = transform.position - playerTransform.position;
        distance = distanceVector.magnitude;

        if ( distance <= 3.5 && playerData.hasKey)
        {
            
            StartCoroutine("TriggerAnimation");

        }



        if (running && distance > 3.5)
        {
            StopCoroutine("TriggerAnimation");
            running = false;
        }
    }

    IEnumerator TriggerAnimation()
    {
        
        running = true;
        yield return new WaitForSeconds(5f);
        doorAnimator.SetTrigger("open");
        running = false;
    }


}
