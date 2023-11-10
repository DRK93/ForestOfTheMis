using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.QuestSystem
{
    public class AllQuestsInOnePlace : MonoBehaviour
    {
        public HashSet<QuestScriptbleObject> allQuestsList;
        public HashSet<QuestJournalPartsScriptableObjects> allQuestJorunalParts;

        private void Awake()
        {
            allQuestsList = new HashSet<QuestScriptbleObject>();
            allQuestJorunalParts = new HashSet<QuestJournalPartsScriptableObjects>();
        }

        public QuestScriptbleObject GetQuest(int questId)
        {
            QuestScriptbleObject questToReturn = null;
            foreach (var quest in allQuestsList)
            {
                if (questId == quest.questID)
                {
                    questToReturn = quest;
                }
            }

            return questToReturn;
        }
    }
}

