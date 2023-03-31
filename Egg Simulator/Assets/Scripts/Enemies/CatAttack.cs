using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CatAttack : MonoBehaviour
{
    public playerDataSO playerdata;
    public EnemyDataSO catData;
    public ParticleSystem damageParticle;

    [SerializeField] UnityEvent damageEvent;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player") && catData.currentState == EnemyState.ATTACK)
        {
            playerdata.TakeDamage(catData.attackPower);
            damageParticle.Play();
            damageEvent.Invoke();
        }
        
    }
}
