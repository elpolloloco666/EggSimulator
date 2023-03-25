using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject HUDPanel;
    public GameObject PausePanel;
    public GameObject VictoryPanel;
    public GameObject LosePanel;
    public GameObject SettingsPanel;
    public static GameManager instance;  
    public GameState currentState;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentState = GameState.PLAYING;    
    }



    void Update()
    {
        if(currentState == GameState.PLAYING && Input.GetKeyDown(KeyCode.Escape))
        {
            currentState = GameState.PAUSED;
        }

        
        setState();

    }

    void setState()
    {
        switch (currentState)
        {
            case GameState.PLAYING:
                Time.timeScale = 1;
                HUDPanel.SetActive(true);
                VictoryPanel.SetActive(false);
                LosePanel.SetActive(false);
                PausePanel.SetActive(false);
                SettingsPanel.SetActive(false);
                break;

            case GameState.PAUSED:
                Time.timeScale = 0;
                HUDPanel.SetActive(false);
                VictoryPanel.SetActive(false);
                LosePanel.SetActive(false);
                PausePanel.SetActive(true);
                SettingsPanel.SetActive(false);
                break;

            case GameState.SETTINGS:
                HUDPanel.SetActive(false);
                VictoryPanel.SetActive(false);
                LosePanel.SetActive(false);
                PausePanel.SetActive(false);
                SettingsPanel.SetActive(true);
                break;

            case GameState.WON:
                HUDPanel.SetActive(false);
                VictoryPanel.SetActive(true);
                LosePanel.SetActive(false);
                PausePanel.SetActive(false);
                SettingsPanel.SetActive(false);
                break;

            case GameState.LOST:
                HUDPanel.SetActive(false);
                VictoryPanel.SetActive(false);
                LosePanel.SetActive(true);
                PausePanel.SetActive(false);
                SettingsPanel.SetActive(false);
                break;
        }
    }
}

public enum GameState
{
    PLAYING,
    PAUSED,
    SETTINGS,
    WON,
    LOST,
}
