using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireScript : MonoBehaviour
{
    public playerDataSO playerData;
    [SerializeField] UnityEvent damageEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            playerData.TakeDamage(20);
            damageEvent.Invoke();
        } 
    }
}
