using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public playerDataSO playerData;
    public Image lifebar;
    public Animator playerAnimator;
   
    void Update()
    {
        
        lifebar.fillAmount = playerData.life / 100;
        playerAnimator.SetFloat("health",playerData.life / 100);
        
    }

    
}
