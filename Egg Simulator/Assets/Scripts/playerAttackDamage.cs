using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttackDamage : MonoBehaviour
{
    public EnemyDataSO enemyData;
    public playerDataSO playerData;

  

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("enemy") && playerData.isAttacking)
        {
            enemyData.TakeDamage(10);

        }
        if (other.transform.CompareTag("prop"))
        {
            //Debug.Log("prop");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("prop"))
        {
            //Debug.Log("prop");
        }
    }

   
}
