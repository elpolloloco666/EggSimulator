using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject ScoreTablePanel;
    public AudioSource audioSource;
    public AudioClip buttonTrack;

    public void Play()
    {
        SceneManager.LoadScene(1);
        audioSource.PlayOneShot(buttonTrack);
    }

    public void Scores()
    {
        ScoreTablePanel.SetActive(true);
        MainMenuPanel.SetActive(false);
        audioSource.PlayOneShot(buttonTrack);
    }

    public void BackToMenu()
    {
        ScoreTablePanel.SetActive(false);
        MainMenuPanel.SetActive(true);
        audioSource.PlayOneShot(buttonTrack);
    }

    public void Exit()
    {
        audioSource.PlayOneShot(buttonTrack);
        Application.Quit();
    }

}
