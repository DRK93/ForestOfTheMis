using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using NGS.ExtendableSaveSystem;
using _MyScripts.QuestSystem;

namespace _MyScripts.GameEventSystem
{
    public class EventObject : MonoBehaviour, ISavableComponent
    {
        [field: SerializeField] private EventScriptableObject myEventData;
        [field: SerializeField] private EventInJournalScriptableObject myEventToJournalData;
        public bool eventObjectDone;
        private int _uniqueID;
        private int _executionOrder;

        private void Start()
        {
            GameObject.FindWithTag("QuestBase").GetComponent<EventsDatabase>().eventDataBase.Add(myEventData);
        }

        private void OnTriggerEnter(Collider other)
        {
            // set data to EventPanel
            //GameObject.FindWithTag("EventPanel").GetComponent<EventPanel>().ShowEventPanel(myEventData);
        }

        public void EventStart()
        {
            GameObject.FindWithTag("EventPanel").GetComponent<EventPanel>().ShowEventPanel(myEventData);
        }

        private void OnDisable()
        {
            // this check for the SaveSystem
            eventObjectDone = true;
            // set something to event manager that event object ended its purpose
        }

        public int uniqueID => _uniqueID;

        public int executionOrder => _executionOrder;

        public ComponentData Serialize()
        {
            throw new NotImplementedException();
        }

        public void Deserialize(ComponentData data)
        {
            throw new NotImplementedException();
        }
    }
}

