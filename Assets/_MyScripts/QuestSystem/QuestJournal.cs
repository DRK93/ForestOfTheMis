using System.Collections;
using System.Collections.Generic;
using _MyScripts.Player;
using UnityEngine;
using _MyScripts.Player;
using TMPro;

namespace _MyScripts.QuestSystem
{
    public class QuestJournal : MonoBehaviour, IStopPlayer
    {
        public GameObject journal;
        public GameObject journalQuestPanel;
        public GameObject journalDialoguePanel;
        public GameObject journalEventPanel;
        public GameObject eventTextPrefab;
        public GameObject questTextPartPrefab;
        public GameObject questButtonPrefab;
        [field: SerializeField] private QuestJournalDatabase journalDatabase;
        [field: SerializeField] private GameObject buttonsParent;
        [field: SerializeField] private GameObject questTitle;
        [field: SerializeField] private GameObject questCompletedToggle;
        [field: SerializeField] private GameObject questViewport;

        void Start()
        {
        
        }
        public void HideJournal()
        {
            journal.SetActive(false);
            LetPlayerMove();
        }

        // reference from buttons in journal
        public void ChangeMainPanel(int number)
        {
            switch (number)
            {
                case 1:
                    journalQuestPanel.SetActive(true);
                    journalEventPanel.SetActive(false);
                    journalDialoguePanel.SetActive(false);
                    break;
                case 2:
                    journalQuestPanel.SetActive(false);
                    journalEventPanel.SetActive(true);
                    journalDialoguePanel.SetActive(false);
                    
                    break;
                case 3:
                    journalQuestPanel.SetActive(false);
                    journalEventPanel.SetActive(false);
                    journalDialoguePanel.SetActive(true);
                    break;
                default:
                    break;
            }

            UpdateQuestJorunalLayout();
        }
        public void UpdateQuestJournalData()
        {
            
        }

        private void UpdateQuestJorunalLayout()
        {
            
        }

        public void JournalAfterLoad()
        {
            
        }

        public void ShowQuest(int questButtonIndex)
        {
            
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

