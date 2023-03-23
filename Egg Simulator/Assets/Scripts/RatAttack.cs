using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAttack : MonoBehaviour
{
    public playerDataSO playerdata;
    public EnemyDataSO ratData;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player") && ratData.currentState == EnemyState.ATTACK)
        {
            playerdata.TakeDamage(2.5f);
        }

    }
}
