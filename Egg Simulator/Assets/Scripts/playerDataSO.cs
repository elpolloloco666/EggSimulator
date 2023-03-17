using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerDataSO")]
public class playerDataSO : ScriptableObject
{
    public float life;
    public float score;
    public bool isAttacking;
    public bool hasAProp;
    public bool hasKey;
    public UnityEvent OnDataChange;

    public void TakeDamage(float damage)
    {
        float currentLife = life - damage;
        if (currentLife <= 0) life = 0;
        else life -= damage;
        OnDataChange.Invoke();
    }

    public void TakeHeal(float heal)
    {
        float currentLife = life + heal;
        if (currentLife >= 100) life = 100;
        else life += heal;
        OnDataChange.Invoke();
    }
    
}
