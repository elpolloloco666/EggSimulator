using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class catController : MonoBehaviour
{
    public Animator catAnimator;
    public NavMeshAgent catAgent;
    public Transform restPosition;
    public int catLife = 100;
    public EnemyDataSO catData;

    private Transform playerTransform;
    private float distance;
    private float distanceToChase = 8.0f;
    private float distanceToAttack = 1.4f;
    

    void Start()
    {
        catAnimator = GetComponent<Animator>();
        catAgent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        catData.currentState = catState.REST;
        catData.health = 100;
    }


    void Update()
    {
        Vector3 distanceVector = transform.position - playerTransform.position;
        distance = distanceVector.magnitude;

        if (catData.currentState == catState.REST && distance <= distanceToChase)
        {
            catAnimator.SetBool("awake",true);
            
        }

        if (catData.currentState == catState.CHASE && distance <= distanceToAttack)
        {
            catData.currentState = catState.ATTACK;
        }

        if (catData.currentState == catState.ATTACK && distance > distanceToAttack)
        {
            catData.currentState = catState.CHASE;
        }

        if (catData.currentState == catState.CHASE && distance > distanceToChase)
        {

            catData.currentState = catState.RETREAT;

        }

        if (catData.currentState == catState.RETREAT && distance < distanceToChase)
        {
            catData.currentState = catState.CHASE;

        }

        if (catLife <= 0 && catData.currentState != catState.DEAD)
        {
            catAnimator.SetTrigger("death");
            catData.currentState = catState.DEAD;
        }

        

        setState();
        
    }

    void setState()
    {
        switch (catData.currentState)
        {
            case catState.REST:
                catAnimator.SetBool("chase", false);
                catAnimator.SetBool("attack", false);
                catAnimator.SetBool("rest", true);
                catAnimator.ResetTrigger("revived");
                catAgent.ResetPath();
                break;

            case catState.CHASE:
                catAnimator.SetBool("rest", false);
                catAnimator.SetBool("attack", false);
                catAnimator.SetBool("chase", true);
                catAnimator.ResetTrigger("revived");
                catAnimator.ResetTrigger("onRestPosition");
                catAgent.SetDestination(playerTransform.position);
                break;

            case catState.ATTACK:
                catAnimator.SetBool("rest", false);
                catAnimator.SetBool("chase", false);
                catAnimator.SetBool("attack", true);
                catAnimator.ResetTrigger("revived");
                catAnimator.ResetTrigger("onRestPosition");
                break;

            case catState.RETREAT:
                catAnimator.SetBool("chase", false);
                catAnimator.SetBool("attack", false);
                catAnimator.ResetTrigger("revived");
                
                catAgent.SetDestination(restPosition.position);
                
                Vector3 distanceVector = transform.position - restPosition.position;
                distance = distanceVector.magnitude; 
                if(distance < 0.1)
                {
                                     
                    catAnimator.SetTrigger("onRestPosition");
                    catData.currentState = catState.REST;
                }
            
                break;

            case catState.DEAD:
                
                catAgent.ResetPath();
                StartCoroutine("WaitToRevive");
                
                break;
        }
    }

    public IEnumerator WaitToRevive()
    {
        yield return new WaitForSeconds(5);
        catLife = 100;
        catAnimator.SetTrigger("revived");
        catData.currentState = catState.CHASE;
    }

    public void startChase()
    {
        catData.currentState = catState.CHASE;
        catAnimator.SetBool("awake", false);
        catAnimator.SetBool("chase", true);
    }


}




