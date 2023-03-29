using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class VictoryTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent victoryEvent;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player")) victoryEvent.Invoke();
    }
}
