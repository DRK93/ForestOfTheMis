using System;
using System.Collections;
using System.Collections.Generic;
using _MyScripts.GameEventSystem;
using NGS.ExtendableSaveSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace _MyScripts.QuestSystem
{
    public class QuestManager : MonoBehaviour, ISavableComponent
    {
        [field: SerializeField] private QuestJournal questJournal;
        [field: SerializeField] private GameEventManager gameEventManager;
        [field: SerializeField] private AllQuestsInOnePlace questBase;
        public List<QuestScriptbleObject> acceptedQuests;
        public List<QuestScriptbleObject> questsCompleted;
        public List<int> activeQuestsAmount;

        // where it place here or in questJournal?
        private List<int> _activeQuestJournalPartsID;
        private List<int> _activeEventPartsID;
        
        // for save system purpose only
        private List<int> _activeQuestsID;
        private List<int> _completedQuestsID;
        
        private int _uniqueID;
        private int _executionOrder;

        private void Awake()
        {
            acceptedQuests = new List<QuestScriptbleObject>();
            questsCompleted = new List<QuestScriptbleObject>();
        }
        
        public void AddNewQuest(QuestScriptbleObject quest)
        {
            if (!acceptedQuests.Contains(quest))
            {
                acceptedQuests.Add(quest);
                activeQuestsAmount.Add(0);
                if (quest.questType == QuestScriptbleObject.QuestType.UnOfficial)
                {
                    
                }
                else
                {
                    // questJournal.AddNewQuest(quest.questID)
                }
                
                // gameEventManager do something or not
            }
            else
            {
                
            }
        }

        public void UpdateQuestAmount(int numberToAdd, int checkQuestID)
        {
            for (int i = 0; i < acceptedQuests.Count; i++)
            {
                if (acceptedQuests[i].questID == checkQuestID)
                {
                    activeQuestsAmount[i] += numberToAdd;
                    if (activeQuestsAmount[i] >= acceptedQuests[i].amountForComplete)
                    {
                        questsCompleted.Add(acceptedQuests[i]);
                        QuestCompletedInJournal(acceptedQuests[i].questID);
                        QuestCOmpletedEvent(acceptedQuests[i]);
                        // remove from active qeust List etc.
                        //activeQuestsAmount.Remove(activeQuestsAmount[i]);
                        //acceptedQuests.Remove(acceptedQuests[i]);
                    }
                }
            }
        }

        private void QuestCOmpletedEvent(QuestScriptbleObject quest)
        {
            if (quest.questType == QuestScriptbleObject.QuestType.UnOfficial)
            {
                if (quest.eventAfter != null)
                {
                    PlayEvent();
                }
            }
            else
            {
                PlayCompletedNotification();
            }
        }

        private void PlayCompletedNotification()
        {
            
        }
        private void PlayEvent()
        {
            // event panel
            // spawn enemies
            // add new quest
        }
        public void QuestCompletedInJournal(int questID)
        {
            // questJournal.CompletedQuest(questID)
        }

        public int uniqueID => _uniqueID;

        public int executionOrder => _executionOrder;

        public ComponentData Serialize()
        {
            int index1 = 0;
            int index2 = 0;
            int index3 = 0;
            ExtendedComponentData data = new ExtendedComponentData();

            
            foreach (var questId in _activeQuestsID)
            {
                data.SetInt("activeQuest" + index1, questId);
                index1++;
            }
            foreach (var questAmount in activeQuestsAmount)
            {
                data.SetInt("activeQuestAmount" + index2, questAmount);
                index2++;
            }
            foreach (var questId in _completedQuestsID)
            {
                data.SetInt("completedQuest" + index3, questId);
                index3++;
            }
            data.SetInt("activeQuestCount", _activeQuestsID.Count);
            data.SetInt("completedQuestCount", _completedQuestsID.Count);
            
            return data;
        }

        private void CleanAllLists()
        {
            _activeQuestsID = new List<int>();
            _completedQuestsID = new List<int>();
            acceptedQuests = new List<QuestScriptbleObject>();
            questsCompleted = new List<QuestScriptbleObject>();
            activeQuestsAmount = new List<int>();

        }
        public void Deserialize(ComponentData data)
        {
            int activeQuestCount;
            int completedQuestCount;
            
            ExtendedComponentData unPacked = (ExtendedComponentData)data;

            activeQuestCount = unPacked.GetInt("activeQuestCount");
            completedQuestCount = unPacked.GetInt("completedQuestCount");

            CleanAllLists();

            if (activeQuestCount != 0)
            {
                for (int i = 0; i < activeQuestCount; i++)
                {
                    _activeQuestsID.Add(unPacked.GetInt("activeQuest" + i));
                    acceptedQuests.Add(FindQuestInBase(_activeQuestsID[i]));
                }
            }
            if (activeQuestCount != 0)
            {
                for (int i = 0; i < activeQuestCount; i++)
                {
                    activeQuestsAmount.Add(unPacked.GetInt("activeQuestAmount" + i));
                }
            }

            if (completedQuestCount != 0)
            {
                for (int i = 0; i < completedQuestCount; i++)
                {
                    _completedQuestsID.Add(unPacked.GetInt("completedQuest" + i));
                    questsCompleted.Add(FindQuestInBase(_completedQuestsID[i]));
                }
                
            }
            JournalAfterLoadUpdate();
        }

        private QuestScriptbleObject FindQuestInBase(int number)
        {
            QuestScriptbleObject questToReturn = null;
            foreach (var quest in questBase.allQuestsList)
            {
                if (number == quest.questID)
                    questToReturn = quest;
            }

            return questToReturn;
        }

        private void JournalAfterLoadUpdate()
        {
            // restoring journal 
            questJournal.JournalAfterLoad();
        }
    }
}

