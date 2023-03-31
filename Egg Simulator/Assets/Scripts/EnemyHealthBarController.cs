using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarController : MonoBehaviour
{
    public EnemyDataSO catData;
    public Image lifebar;
    void Update()
    {
        lifebar.fillAmount = catData.health / 100;
    }
}
