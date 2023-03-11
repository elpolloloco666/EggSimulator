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
    public GameObject food;
    private float distance;
    private Vector3 distanceVector;
    private float distanceToChase = 8.0f;
    private float distanceToAttack = 1.4f;
    private float distanceToEat = 2.5f;

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
        if (food != null)
        {
            distanceVector = transform.position - food.transform.position;
        }else distanceVector = transform.position - playerTransform.position;
        distance = distanceVector.magnitude;


        if (food == null)
        {
            if (catData.currentState == catState.REST && distance <= distanceToChase)
            {
                catAnimator.SetBool("awake", true);

            }

            if (catData.currentState == catState.CHASE && distance <= distanceToAttack && food == null)
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

           

        }

        if (food != null && catData.currentState == catState.REST)
        {
            catAnimator.SetBool("awake", true);
            catData.currentState = catState.CHASE;
        }

        if (catData.currentState == catState.CHASE && distance <= distanceToEat && food != null)
        {
            catData.currentState = catState.EAT;
        }


        if (catLife <= 0 && catData.currentState != catState.DEAD)
        {
            catAnimator.SetTrigger("death");
            catData.currentState = catState.DEAD;
        }



        //////////////////////////////////////////////////////FOOD DETECTION///////////////////////////////////////////////////////////////
        if (food == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 15f);
            foreach (var collider in colliders)
            {
                if (collider.transform.CompareTag("food")) food = collider.gameObject;
            }
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
                if(food != null) catAgent.SetDestination(food.transform.position);
                else catAgent.SetDestination(playerTransform.position);
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
            case catState.EAT:
                catAnimator.SetBool("rest", false);
                catAnimator.SetBool("chase", false);
                catAnimator.SetBool("attack", false);
                catAnimator.ResetTrigger("revived");
                catAnimator.ResetTrigger("onRestPosition");
                catAnimator.SetBool("eating", true);
                break;

            case catState.DEAD:
                
                catAgent.ResetPath();
                StartCoroutine("WaitToRevive");
                
                break;
        }
    }

    public void Eat()
    {
        Destroy(food);
        food = null;
        catAnimator.SetBool("eating", false);
        catAgent.ResetPath();
        catData.currentState = catState.CHASE; 
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




