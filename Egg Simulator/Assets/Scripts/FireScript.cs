using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    public playerDataSO playerData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player")) playerData.TakeDamage(20);
    }
}
