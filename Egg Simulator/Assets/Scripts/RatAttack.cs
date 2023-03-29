using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RatAttack : MonoBehaviour
{
    public playerDataSO playerdata;
    public EnemyDataSO ratData;

    [SerializeField] UnityEvent damageEvent;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player") && ratData.currentState == EnemyState.ATTACK)
        {
            playerdata.TakeDamage(ratData.attackPower);
            damageEvent.Invoke();
        }

    }
}
