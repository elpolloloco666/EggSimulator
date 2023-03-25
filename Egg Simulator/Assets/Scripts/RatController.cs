using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public class RatController : MonoBehaviour
{
    public Animator RatAnimator;
    public NavMeshAgent ratAgent;
    public EnemyDataSO ratData;

    private Transform playerTransform;
    private float distance;
    private Vector2 distanceVector;
    private float yAxisDistance;

    [SerializeField] UnityEvent deathEvent;

    void Start()
    {
        ratData.health = 100;
        ratData.currentState = EnemyState.REST;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

   
    void Update()
    {

        if (ratData.health > 0)
        {

        
        distanceVector = new Vector2(transform.position.x,transform.position.z) - new Vector2(playerTransform.position.x, playerTransform.position.z);
        distance = distanceVector.magnitude;
        yAxisDistance = Mathf.Abs(transform.position.y - playerTransform.position.y);

        if(distance < 7 && ratData.currentState == EnemyState.REST)
        {
            ratData.currentState = EnemyState.CHASE;
        }

        if(ratData.currentState == EnemyState.CHASE && distance < 1.2)
        {
            ratData.currentState = EnemyState.ATTACK;
        }

        if (ratData.currentState == EnemyState.ATTACK && distance > 1.2)
        {
            ratData.currentState = EnemyState.CHASE;
        }

        if(ratData.currentState == EnemyState.CHASE && yAxisDistance > 1)
        {
            ratData.currentState = EnemyState.REST;
        }

        

        }

        if (ratData.health == 0)
        {
            
            ratData.currentState = EnemyState.DEAD;
        }

        setState();
    }


    void setState()
    {
        switch (ratData.currentState)
        {
            case EnemyState.REST:
                RatAnimator.SetBool("chase", false);
                RatAnimator.SetBool("attack", false);
                ratAgent.ResetPath();             
                break;

            case EnemyState.CHASE:
                RatAnimator.SetBool("chase", true);
                RatAnimator.SetBool("attack", false);
                ratAgent.SetDestination(playerTransform.position);
                
                break;
            case EnemyState.ATTACK:
                RatAnimator.SetBool("chase", false);
                RatAnimator.SetBool("attack", true);
                break;
            case EnemyState.DEAD:
                RatAnimator.SetBool("chase", false);
                RatAnimator.SetBool("attack", true);
                ratAgent.ResetPath();
                RatAnimator.SetTrigger("Death");
                StartCoroutine("waitToDestroy");
                break;
        }
    }


    IEnumerator waitToDestroy()
    {
        deathEvent.Invoke();
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    
}
