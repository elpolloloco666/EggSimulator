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
        if (currentAttackPower >= 35) attackPower = 35;
        else attackPower += Powerindex;
    }

    public void DecreaseAttackPower()
    {
        float currentAttackPower = attackPower - Powerindex;
        if (currentAttackPower <= 5) attackPower = 5;
        else attackPower -= Powerindex;
        
    }

    public void ResetAttackPower()
    {
        attackPower = initialAttackPower;
    }



    //public void TakeHeal(float heal)
    //{
    //    float currentLife = health + heal;
    //    if (currentLife >= 100) health = 100;
    //    else health += heal;

    //}

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
