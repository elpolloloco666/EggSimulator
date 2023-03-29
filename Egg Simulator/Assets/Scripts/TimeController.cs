using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    public TMP_Text timer;
    public PlayerStatsSO playerStats;

    private int minutes = 0;
    private int seconds = 0;
    private float playtime = 0f;

    void Update()
    {
        if(GameManager.instance.currentState == GameState.PLAYING)
        {
            playtime += Time.deltaTime;
            playerStats.time = Mathf.Round(playtime);
            minutes = Mathf.FloorToInt(playtime / 60f);
            seconds = Mathf.FloorToInt(playtime % 60f);
            timer.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
        

    }


}
