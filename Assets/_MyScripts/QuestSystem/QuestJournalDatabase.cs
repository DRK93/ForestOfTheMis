using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _MyScripts.GameEventSystem;

namespace _MyScripts.QuestSystem
{
    public class QuestJournalDatabase : MonoBehaviour
    {
        public HashSet<QuestJournalPartsScriptableObjects> questJournalPartsList;
        public HashSet<EventInJournalScriptableObject> eventJournalPartsList;

        private void Awake()
        {
            questJournalPartsList = new HashSet<QuestJournalPartsScriptableObjects>();
            eventJournalPartsList = new HashSet<EventInJournalScriptableObject>();
        }

        public void BrowseActiveQuest(int index)
        {
            //return questJournalPartsList[index].questID;
        }
    }
}

