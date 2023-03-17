using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropController : MonoBehaviour
{
    public Vector3 grabbedPosition;
    public Vector3 grabbedRotation;
    public float attackPower;
    public EnemyDataSO catData;
    public playerDataSO playerData;
    public bool isFood;
    public bool isKey;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("enemy") && playerData.isAttacking)
        {
            catData.TakeDamage(attackPower);
        }
    }

    
}
