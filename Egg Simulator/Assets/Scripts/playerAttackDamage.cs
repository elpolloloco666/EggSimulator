using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttackDamage : MonoBehaviour
{
    public EnemyDataSO catData;
    public EnemyDataSO ratData;
    public playerDataSO playerData;

  

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("enemy") && playerData.isAttacking)
        {
            if(other.gameObject.name == "gato")
            {
                catData.TakeDamage(10);
            }
            
            if(other.gameObject.name == "rata")
            {
                ratData.TakeDamage(10);
            }
            

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
