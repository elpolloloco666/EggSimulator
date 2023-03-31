using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidController : MonoBehaviour
{
    public playerDataSO playerData;
    public float healMount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (playerData.life < 100)
            {
                playerData.TakeHeal(healMount);
                Destroy(this.gameObject);
            }
        }
    }

    
}
