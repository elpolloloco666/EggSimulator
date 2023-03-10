using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAttack : MonoBehaviour
{
    public playerDataSO playerdata;
    public EnemyDataSO catData;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player") && catData.currentState == catState.ATTACK)
        {
            playerdata.TakeDamage(10);
        }
        
    }
}
