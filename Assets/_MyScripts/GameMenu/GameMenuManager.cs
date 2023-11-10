using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using _MyScripts.Player;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using NGS.ExtendableSaveSystem;

namespace _MyScripts.GameMenu
{
    public class GameMenuManager : MonoBehaviour, IStopPlayer
{
    [field: SerializeField] public GameMaster GameMaster { get; private set; }

    public GameObject gameMenu;
    public GameObject journal;
    public GameObject inventory;
    public GameObject traitsPanel;
    public GameObject skillsPanel;
    public GameObject attacksPanel;
    public GameObject loadGamePanel;
    public GameObject saveGamePanel;
    public GameObject tipsPanel;
    
    public AudioSource buttonClick;
    
    [field: Header("Buttons That Open")] 
    [field: SerializeField] public Button TraitsButton { get; private set; }
    [field: SerializeField] public Button SkillsButton { get; private set; }
    [field: SerializeField] public Button AttacksButton { get; private set; }
    [field: SerializeField] public Button InventoryButton { get; private set; }
    [field: SerializeField] public Button JournalButton { get; private set; }
    [field: SerializeField] public Button MenuButton { get; private set; }

    [field: Header("Buttons That Close")] 
    [field: SerializeField] public Button CloseAttacksButton { get; private set; }
    
    public Action buttonClicked;
    public Action StopGame;
    // Start is called before the first frame update
    void Start()
    {
        buttonClicked += PlayClickAudio;
    }
    public void ShowAttacksPanel()
    {
        buttonClicked?.Invoke();
        attacksPanel.SetActive(true);
        StopPlayer();
    }
    public void HideAttacksPanel()
    {
        buttonClicked?.Invoke();
        attacksPanel.SetActive(false);
        LetPlayerMove();
    }

    public void ShowLoadGame()
    {
        buttonClicked?.Invoke();
        loadGamePanel.SetActive(true);
        gameMenu.SetActive(false);
    }

    public void LoadGameNumber(int number)
    {
        buttonClicked?.Invoke();
        TryToLoadGame(number);
    }
    public void HideLoadGame()
    {
        buttonClicked?.Invoke();
        loadGamePanel.SetActive(false);
        gameMenu.SetActive(true);
    }

    public void ShowSaveGame()
    {
        buttonClicked?.Invoke();
        saveGamePanel.SetActive(true);
        gameMenu.SetActive(false);
    }

    public void SaveGameNumber(int number)
    {
        buttonClicked?.Invoke();
        TryToSaveGame(number);
    }

    private void TryToSaveGame(int number)
    {
        switch (number)
        {
            case 1:
                GameMaster.SaveGame1();
                break;
            case 2:
                GameMaster.SaveGame2();
                break;
            case 3:
                GameMaster.SaveGame3();
                break;
            case 4:
                GameMaster.SaveGame4();
                break;
            case 5:
                GameMaster.SaveGame5();
                break;
            default:
                break;
        }
    }

    private void TryToLoadGame(int number)
    {
        switch (number)
        {
            case 1:
                GameMaster.LoadGame1();
                break;
            case 2:
                GameMaster.LoadGame2();
                break;
            case 3:
                GameMaster.LoadGame3();
                break;
            case 4:
                GameMaster.LoadGame4();
                break;
            case 5:
                GameMaster.LoadGame5();
                break;
            default:
                break;
        }
    }
    public void HideSaveGame()
    {
        buttonClicked?.Invoke();
        saveGamePanel.SetActive(false);
        gameMenu.SetActive(true);
    }
    public void ShowGameMenu()
    {
        StopGame?.Invoke();
        Time.timeScale = 0f;
        buttonClicked?.Invoke();
        gameMenu.SetActive(true);
        StopPlayer();
    }

    public void HideGameMenu()
    {
        Time.timeScale = 1f;
        buttonClicked?.Invoke();
        gameMenu.SetActive(false);
        LetPlayerMove();
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        buttonClicked?.Invoke();
        SceneManager.LoadScene(0);
    }

    public void ShowManual()
    {
        buttonClicked?.Invoke();
        tipsPanel.SetActive(true);
        gameMenu.SetActive(false);
    }

    public void HideManual()
    {
        buttonClicked?.Invoke();
        tipsPanel.SetActive(false);
        gameMenu.SetActive(true);
    }
    private void PlayClickAudio()
    {
        buttonClick.Play();
    }

    public void ShowJournal()
    {
        buttonClicked?.Invoke();
        journal.SetActive(true);
        StopPlayer();
    }

    public void StopPlayer()
    {
        GameObject.FindWithTag("Player").GetComponent<InputReader>().StopPlayerMovement();
    }

    public void LetPlayerMove()
    {
        GameObject.FindWithTag("Player").GetComponent<InputReader>().LetPlayerMove();
    }
}
}

