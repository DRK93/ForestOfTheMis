using System;
using System.Collections;
using System.Collections.Generic;
using _MyScripts.Player;
using _MyScripts.QuestSystem;
using UnityEngine;
using TMPro;

namespace _MyScripts.GameEventSystem
{
    public class EventPanel : MonoBehaviour, IStopPlayer
    {
        public GameObject eventPanel;
        public GameObject textObject;
        [SerializeField] private AllQuestsInOnePlace allQuestsBase;
        [SerializeField] private QuestManager questManager;
        [SerializeField] private GameEventManager gameEventManager;
        public EventScriptableObject eventUnfold;
        public string eventText;
        public List<GameObject> eventOptionsButtons;
        public int optionsNumber = 0;
        public Action eventDone;
        
        public void ShowEventPanel(EventScriptableObject myEventData)
        {
            int optinonsCount = myEventData.eventOptionsNumber;
            string eventText = myEventData.eventText;
            List<string> optionsButtonsTexts = myEventData.eventOptionText;
            eventPanel.SetActive(true);
            EventText(eventText);
            SetOptionsNumber(optinonsCount, optionsButtonsTexts);
            StopPlayer();
        }

        public void EventText(string text)
        {
            eventText = text;
            textObject.GetComponent<TextMeshProUGUI>().text = eventText;
        }

        private void SetOptionsNumber(int number, List<string> optionsTexts)
        {
            for (int i = 0; i < number; i++)
            {
                eventOptionsButtons[i].SetActive(true);
                UpdateOneOption(eventOptionsButtons[i], optionsTexts[i]);
            }
        }

        private void UpdateOneOption(GameObject optionButton, string text)
        {
            optionButton.GetComponentInChildren<TextMeshProUGUI>().text = text;
        }

        public void EventButtonOptionCLick(int buttonNumber)
        {
            //DoEventFromOption(buttonNumber);
            // do something with choses option
            HideEventPanel();
        }

        private void DoEventFromOption(int numeber)
        {
            if (eventUnfold.eventOptionsNumber > numeber)
            {
                // safety check
            }
            else 
            {
                gameEventManager.AddEvent(eventUnfold);
                switch (eventUnfold.eventTypes[numeber])
                {
                    case EventScriptableObject.EventType.SpawnQuest:
                        if (eventUnfold.questIDs[numeber]!= 0)
                            questManager.AddNewQuest(allQuestsBase.GetQuest(eventUnfold.questIDs[numeber]));
                        break;
                    case EventScriptableObject.EventType.SpawnItem:
                        // safety check
                        if (eventUnfold.questIDs[numeber]!= 0)
                            gameEventManager.SpawnObject(eventUnfold.questIDs[numeber]);
                        break;
                    case EventScriptableObject.EventType.SpawnEnemy:
                        if (eventUnfold.questIDs[numeber]!= 0)
                            gameEventManager.SpawnEnemies(eventUnfold.questIDs[numeber]);
                        break;
                    case EventScriptableObject.EventType.Cutscene:
                        PlayCutscene();
                        break;
                    case EventScriptableObject.EventType.LocationChange:
                        if (eventUnfold.questIDs[numeber]!= 0)
                            gameEventManager.ChangeScenery(eventUnfold.questIDs[numeber]);
                        break;
                }
            }
        }
        
        private void SendQuestId(int questID)
        {
            
        }

        private void ReleaseEnemies()
        {
            
        }

        private void SpawnObject()
        {
            
        }

        private void PlayCutscene()
        {
            
        }

        private void ChangeScenery()
        {
            
        }
        private void OnDisable()
        {
            ClearEventPanel();
        }

        public void ClearEventPanel()
        {
            for (int i = eventOptionsButtons.Count-1; i > 0; i--)
            {
                if(eventOptionsButtons[i].activeInHierarchy)
                    eventOptionsButtons[i].SetActive(false);
            }
        }

        private void HideEventPanel()
        {
            eventDone?.Invoke();
            ClearEventPanel();
            LetPlayerMove();
            eventPanel.SetActive(false);
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

