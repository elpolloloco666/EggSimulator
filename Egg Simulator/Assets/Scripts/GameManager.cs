using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
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
    public PlayerStatsSO playerStats;
    public playerDataSO playerData;
    public string fileName;
    public EnemyDataSO catData;
    public EnemyDataSO ratData;

    private StreamWriter sw;
    private StreamReader sr;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentState = GameState.PLAYING;
        playerStats.resetStats();
        setDifficulty();
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

    public void Victory()
    {
        saveData(true);
        StartCoroutine(WaitToShow(GameState.WON,0.5f));
        
    }

    public void Defeat()
    {
        saveData(false);
        StartCoroutine(WaitToShow(GameState.LOST,4f));
    }

    IEnumerator WaitToShow(GameState state,float delay)
    {
        yield return new WaitForSeconds(delay);
        currentState = state;
        Time.timeScale = 0;
    }

    public int calculateScore(bool survived)
    {
        int score = 0;

        if (playerStats.CatDowns > 0)
        {
            score = score + playerStats.CatDowns * 250;
        }


        if (playerStats.RatDown)
        {
            score = score + 100;
        }

        if(survived)
        {
            if (playerStats.time < 60)
            {
                score = score + 1500;
            }
            else
            {
                if (playerStats.time < 180)
                {
                    score = score + 750;
                }
                else
                {
                    score = score + 300;
                }
            }
        }
        

        score = score + (int)playerData.life * 5;

        return score; 

    }

    private void saveData(bool survived)
    {
        sw = new StreamWriter(Application.persistentDataPath + "/" + fileName, true);        
        sw.WriteLine(Mathf.Round(calculateScore(survived)));
        sw.Close();
    }

    public List<int> getScores(bool ordered)
    {
        if (File.Exists(Application.persistentDataPath + "/Scores"))
        {
            string[] data;
            List<int> scores = new List<int>();
            sr = new StreamReader(Application.persistentDataPath + "/Scores");

            data = sr.ReadToEnd().Split('\n');

            foreach (string score in data)
            {
                int number;
                if (int.TryParse(score, out number))
                    scores.Add(number);

            }

            sr.Close();

            if (ordered)
            {
                scores.Sort();
                scores.Reverse();                
            }
            else
            {
                scores.Reverse();
            }

            return scores;
                    
        }
        else return null;
    }

    private void setDifficulty()
    {
        if(getScores(false) != null && getScores(false).Count >= 3)
        {
            List<int> scores = getScores(false);
            
            if(scores[0]>1500 && scores[1] > 1500 && scores[2] > 1500)
            {
                catData.IncreaseAttackPower();
                ratData.IncreaseAttackPower();
            }

            if (scores[0] < 500 && scores[1] < 500 && scores[2] < 500)
            {
                catData.DecreaseAttackPower();
                ratData.DecreaseAttackPower();
            }
        }
        else
        {
            catData.ResetAttackPower();
            ratData.ResetAttackPower();
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
