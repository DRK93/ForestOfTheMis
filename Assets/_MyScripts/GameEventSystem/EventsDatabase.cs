using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.GameEventSystem
{
    public class EventsDatabase : MonoBehaviour
    {
        public HashSet<EventScriptableObject> eventDataBase;
        public HashSet<EventScriptableObject> doneEventDataBase;
        public HashSet<EventObject> eventObjectsToDo;
        public HashSet<EventObject> eventObjectsDone;
        public HashSet<EventInJournalScriptableObject> eventJournalParts;

        private void Awake()
        {
            eventDataBase = new HashSet<EventScriptableObject>();
            eventJournalParts = new HashSet<EventInJournalScriptableObject>();
        }

        public void RemoveEvent( EventScriptableObject eventDone)
        {
            eventDataBase.Remove(eventDone);
            doneEventDataBase.Add(eventDone);
        }
    }

}
