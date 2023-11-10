using System;
using System.Collections;
using System.Collections.Generic;
using _MyScripts.Player;
using _MyScripts.QuestSystem;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

namespace _MyScripts.GameEventSystem
{
    public class EventPanel : MonoBehaviour, IStopPlayer
    {
        public GameObject eventPanel;
        public GameObject textObject;
        [SerializeField] private AllQuestsInOnePlace allQuestsBase;
        [SerializeField] private QuestManager questManager;
        public string eventText;
        public List<GameObject> eventOptionsButtons;
        public List<int> optionsQuestId;
        public int optionsNumber = 0;
        
        // there is a need for connection of eventData to eventManager, Quest Jouranl etc.

        private void Start()
        {
            optionsQuestId = new List<int>();
        }

        public void ShowEventPanel(EventScriptableObject myEventData)
        {
            int optinonsCount = myEventData.eventOptionsNumber;
            string eventText = myEventData.eventText;
            List<string> optionsButtonsTexts = myEventData.eventOptionText;
            optionsQuestId = myEventData.questIDs;
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
            optionButton.GetComponent<TextMeshProUGUI>().text = text;
        }

        public void EventButtonOptionCLick(int buttonNumber)
        {
            SendQuestId(buttonNumber);
            HideEventPanel();
        }

        private void SendQuestId(int index)
        {
            questManager.AddNewQuest(allQuestsBase.GetQuest(optionsQuestId[index]));
        }
        private void OnDisable()
        {
            ClearEventPanel();
        }

        public void ClearEventPanel()
        {
            for (int i = eventOptionsButtons.Count; i > 0; i--)
            {
                if(eventOptionsButtons[i].activeInHierarchy)
                    eventOptionsButtons[i].SetActive(false);
            }
        }

        public void HideEventPanel()
        {
            ClearEventPanel();
            eventPanel.SetActive(false);
            LetPlayerMove();
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

