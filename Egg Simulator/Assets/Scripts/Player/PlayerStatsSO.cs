using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/PlayerStatsSO")]
public class PlayerStatsSO : ScriptableObject
{

    public float time;
    public float healthLeft; 
    public int CatDowns;
    public bool RatDown; 

    public void resetStats()
    {
        time = 0;
        healthLeft = 0;
        CatDowns = 0;
        RatDown = false;
    }

    public void AddDown()
    {
        CatDowns++;
    }
     
}
