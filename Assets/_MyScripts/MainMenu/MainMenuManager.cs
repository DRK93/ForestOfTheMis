using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _MyScripts.MainMenu
{
    public class MainMenuManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject statrMissionsPanel;
    public GameObject loadGamePanel;
    public GameObject tipsPanel;
    public GameObject missionNotification;
    public GameObject loadGameNotification1;
    public GameObject loadGameNotification2;
    public AudioSource buttonClick;
    public Action buttonClicked;

    private void Start()
    {
        buttonClicked += PlayClickAudio;
    }
    private void OnDestroy()
    {
        buttonClicked -= PlayClickAudio;
    }
    public void ShowMissions()
    {
        buttonClicked?.Invoke();
        menuPanel.SetActive(false);
        statrMissionsPanel.SetActive(true);
    }
    public void ShowMissonsNotifiaction()
    {
        missionNotification.SetActive(true);
        LeanTween.alpha(missionNotification, 255f, 0.2f);
        LeanTween.alpha(missionNotification, 0f, 4f).setOnComplete(HideMissionNotification);
    }
    public void HideMissionNotification()
    {
        missionNotification.SetActive(false);
    }
    public void StartMission(int number)
    {
        // lodaing mission scene
        buttonClicked?.Invoke();
        if (number == 1)
            SceneManager.LoadScene(number);
        else
        {
            ShowMissonsNotifiaction();
        }
    }
    public void BackFromMissions()
    {
        buttonClicked?.Invoke();
        statrMissionsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
    public void ShowTips()
    {
        buttonClicked?.Invoke();
        menuPanel.SetActive(false);
        tipsPanel.SetActive(true);
    }
    public void BackFromTips()
    {
        buttonClicked?.Invoke();
        tipsPanel.SetActive(false);
        menuPanel.SetActive(true);
        
    }
    public void ShowLoadGame()
    {
        buttonClicked?.Invoke();
        menuPanel.SetActive(false);
        loadGamePanel.SetActive(true);
    }
    public void LoadSavedGame(int number)
    {
        buttonClicked?.Invoke();
        if (number < 4)
        {
            ShowLoadNotification1();
        }
        else if (number < 6)
        {
            ShowLoadNotification2();
        }
        else
        {
            // LoadCorrect saved game
        }
    }
    public void ShowLoadNotification1()
    {
        loadGameNotification1.SetActive(true);
        LeanTween.alpha(loadGameNotification1, 255f, 0.2f);
        LeanTween.alpha(loadGameNotification1, 0f, 4f).setOnComplete(HidelLoadNotification1);
    }

    public void HidelLoadNotification1()
    {
        loadGameNotification1.SetActive(false);
    }

    public void ShowLoadNotification2()
    {
        loadGameNotification2.SetActive(true);
        LeanTween.alpha(loadGameNotification2, 255f, 0.2f);
        LeanTween.alpha(loadGameNotification2, 0f, 4f).setOnComplete(HideLoadNotification2);
    }

    public void HideLoadNotification2()
    {
        loadGameNotification2.SetActive(false);
    }
    public void BackFromLoadGame()
    {
        buttonClicked?.Invoke();
        loadGamePanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void ExitGame()
    {
        buttonClick.Play();
        Application.Quit();
    }
    private void PlayClickAudio()
    {
        buttonClick.Play();
    }
}
}

