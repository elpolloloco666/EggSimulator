using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    public float health;
    public catState currentState;

    public void TakeDamage(float damage)
    {
        float currentLife = health - damage;
        if (currentLife <= 0) health = 0;
        else health -= damage;
        
    }

    public void TakeHeal(float heal)
    {
        float currentLife = health + heal;
        if (currentLife >= 100) health = 100;
        else health += heal;
        
    }

}

public enum catState
{
    REST,
    CHASE,
    ATTACK,
    RETREAT,
    EAT,
    DEAD
}
