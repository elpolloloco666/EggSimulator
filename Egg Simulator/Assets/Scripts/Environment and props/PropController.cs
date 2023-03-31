using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PropController : MonoBehaviour
{
    public Vector3 grabbedPosition;
    public Vector3 grabbedRotation;
    public float attackPower;
    public EnemyDataSO catData;
    public EnemyDataSO ratData;
    public playerDataSO playerData;
    public bool isFood;
    public bool isKey;

    [SerializeField] UnityEvent hitEvent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("enemy") && playerData.isAttacking)
        {
            hitEvent.Invoke();

            if (other.gameObject.name == "gato")
            {
                catData.TakeDamage(attackPower);
                
            }

            if (other.gameObject.name == "rata")
            {
                ratData.TakeDamage(attackPower);
            }
            
        }
        
    }

    
}
