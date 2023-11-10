using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.GameEventSystem
{
    public class EventsDatabase : MonoBehaviour
    {
        public HashSet<EventScriptableObject> eventDataBase;

        public HashSet<EventInJournalScriptableObject> eventJournalParts;

        private void Awake()
        {
            eventDataBase = new HashSet<EventScriptableObject>();
            eventJournalParts = new HashSet<EventInJournalScriptableObject>();
        }
    }

}
