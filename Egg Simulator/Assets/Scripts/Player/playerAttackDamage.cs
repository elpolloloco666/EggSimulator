using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class playerAttackDamage : MonoBehaviour
{
    public EnemyDataSO catData;
    public EnemyDataSO ratData;
    public playerDataSO playerData;

    [SerializeField] UnityEvent hitEvent;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("enemy") && playerData.isAttacking && !playerData.hasAProp)
        {
            hitEvent.Invoke();
            if(other.gameObject.name == "gato")
            {
                catData.TakeDamage(10);
            }
            
            if(other.gameObject.name == "rata")
            {
                ratData.TakeDamage(10);
            }
            

        }
        
    }

    
    

   
}
