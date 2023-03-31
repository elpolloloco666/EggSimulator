using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    
    public void Resume()
    {
        GameManager.instance.currentState = GameState.PLAYING;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Settings()
    {
        GameManager.instance.currentState = GameState.SETTINGS;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void backToPauseMenu()
    {
        GameManager.instance.currentState = GameState.PAUSED;
    }

    
}
