using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public class catController : MonoBehaviour
{
    public Animator catAnimator;
    public NavMeshAgent catAgent;
    public Transform restPosition;
    public EnemyDataSO catData;
    public playerDataSO playerData;
    public PlayerStatsSO playerStats;

    private Transform playerTransform;
    private GameObject food;
    private float distance;
    private Vector2 distanceVector;
    private float distanceToChase = 10.0f;
    private float distanceToAttack = 1.4f;
    private float distanceToEat = 2.5f;
    private bool deathSoundPlayed = false;
    private float yAxisDistance;

    [SerializeField] UnityEvent deathEvent;

    void Start()
    {
        catAnimator = GetComponent<Animator>();
        catAgent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        catData.currentState = EnemyState.REST;
        catData.health = 100;
    }


    void Update()
    {

        if (food == null)
        {
            distanceVector = new Vector2(transform.position.x, transform.position.z) - new Vector2(playerTransform.position.x, playerTransform.position.z);
            distance = distanceVector.magnitude;
            yAxisDistance = Mathf.Abs(transform.position.y - playerTransform.position.y);

            if (catData.currentState == EnemyState.REST && distance <= distanceToChase)
            {
                catAnimator.SetBool("awake", true);

            }

            if (catData.currentState == EnemyState.CHASE && distance <= distanceToAttack && food == null)
            {
                catData.currentState = EnemyState.ATTACK;
            }

            if (catData.currentState == EnemyState.ATTACK && distance > distanceToAttack)
            {
                catData.currentState = EnemyState.CHASE;
            }

            if (catData.currentState == EnemyState.CHASE && distance > distanceToChase)
            {

                catData.currentState = EnemyState.RETREAT;

            }

            if (catData.currentState == EnemyState.RETREAT && distance < distanceToChase)
            {
                catData.currentState = EnemyState.CHASE;

            }

            if(catData.currentState == EnemyState.CHASE && playerData.isClimbing)
            {
                catData.currentState = EnemyState.RETREAT;
            }

            if (catData.currentState == EnemyState.CHASE && yAxisDistance > 1)
            {
                catData.currentState = EnemyState.RETREAT;
            }


        }
        else
        {
            distanceVector = transform.position - food.transform.position;
            distance = distanceVector.magnitude;

            if (food != null && catData.currentState == EnemyState.REST)
            {
                catAnimator.SetBool("awake", true);
            }

            if (catData.currentState == EnemyState.CHASE && distance <= distanceToEat && food != null)
            {
                catData.currentState = EnemyState.EAT;
            }
        }

        


        if (catData.health <= 0 && catData.currentState != EnemyState.DEAD)
        {
            catAnimator.SetTrigger("death");
            if (!deathSoundPlayed)
            {
                deathEvent.Invoke();
                deathSoundPlayed = true;
            }
            playerStats.AddDown();
            catData.currentState = EnemyState.DEAD;
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
            case EnemyState.REST:
                catAnimator.SetBool("chase", false);
                catAnimator.SetBool("attack", false);
                catAnimator.SetBool("rest", true);
                catAnimator.ResetTrigger("revived");
                catAgent.ResetPath();
                break;

            case EnemyState.CHASE:
                catAnimator.SetBool("rest", false);
                catAnimator.SetBool("attack", false);
                catAnimator.SetBool("chase", true);
                catAnimator.ResetTrigger("revived");
                catAnimator.ResetTrigger("onRestPosition");
                if(food != null) catAgent.SetDestination(food.transform.position);
                else catAgent.SetDestination(playerTransform.position);
                break;

            case EnemyState.ATTACK:
                catAnimator.SetBool("rest", false);
                catAnimator.SetBool("chase", false);
                catAnimator.SetBool("attack", true);
                catAnimator.ResetTrigger("revived");
                catAnimator.ResetTrigger("onRestPosition");
                break;

            case EnemyState.RETREAT:
                catAnimator.SetBool("chase", false);
                catAnimator.SetBool("attack", false);
                catAnimator.ResetTrigger("revived");
                
                catAgent.SetDestination(restPosition.position);
                
                Vector3 distanceVector = transform.position - restPosition.position;
                distance = distanceVector.magnitude; 
                if(distance < 0.1)
                {
                                     
                    catAnimator.SetTrigger("onRestPosition");
                    catData.currentState = EnemyState.REST;
                }
            
                break;
            case EnemyState.EAT:
                catAnimator.SetBool("rest", false);
                catAnimator.SetBool("chase", false);
                catAnimator.SetBool("attack", false);
                catAnimator.ResetTrigger("revived");
                catAnimator.ResetTrigger("onRestPosition");
                catAnimator.SetBool("eating", true);
                break;

            case EnemyState.DEAD:
                
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
        catData.currentState = EnemyState.CHASE; 
    }

    public IEnumerator WaitToRevive()
    {
        yield return new WaitForSeconds(15);
        catData.health = 100;
        catAnimator.SetTrigger("revived");
        catData.currentState = EnemyState.CHASE;
        deathSoundPlayed = false;
    }

    public void startChase()
    {
        catData.currentState = EnemyState.CHASE;
        catAnimator.SetBool("awake", false);
        catAnimator.SetBool("chase", true);
    }


}




