using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    public float health;
    public float attackPower;
    public float initialAttackPower;
    public float Powerindex;
    public float maxAttackPower;
    public float minAttackPower;
    public EnemyState currentState;
    

    public void TakeDamage(float damage)
    {
        float currentLife = health - damage;
        if (currentLife <= 0) health = 0;
        else health -= damage;
        
    }

    public void IncreaseAttackPower()
    {
        float currentAttackPower = attackPower + Powerindex;
        if (currentAttackPower >= maxAttackPower) attackPower = maxAttackPower;
        else attackPower += Powerindex;
    }

    public void DecreaseAttackPower()
    {
        float currentAttackPower = attackPower - Powerindex;
        if (currentAttackPower <= minAttackPower) attackPower = minAttackPower;
        else attackPower -= Powerindex;
        
    }

    public void ResetAttackPower()
    {
        attackPower = initialAttackPower;
    }


}

public enum EnemyState
{
    REST,
    CHASE,
    ATTACK,
    RETREAT,
    EAT,
    DEAD
}
