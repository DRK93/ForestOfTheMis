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
        private EventPanel _eventPanel;
        private EventsDatabase _eventsDatabase;
        private GameEventManager _gameEventManager;
        public bool eventObjectDone;
        private int _uniqueID;
        private int _executionOrder;

        private void Start()
        {
            _eventsDatabase = GameObject.FindWithTag("QuestBase").GetComponent<EventsDatabase>();
            _eventsDatabase.eventDataBase.Add(myEventData);
        }

        private void OnTriggerEnter(Collider other)
        {
            EventStart();
        }

        public void EventFromManager()
        {
            EventStart();
        }
        private void EventStart()
        {
            if (myEventData.thisEventType == EventScriptableObject.EventType.Panel)
            {
                _eventPanel = GameObject.FindWithTag("EventPanel").GetComponent<EventPanel>();
                _eventPanel.ShowEventPanel(myEventData);
                _eventPanel.eventDone += EventEnd;
            }
            else
            {
                _gameEventManager.AddEvent(myEventData);
                switch (myEventData.thisEventType)
                {
                    case EventScriptableObject.EventType.SpawnQuest:
                        //if (myEventData.questIDs[0]!= 0)
                            //questManager.AddNewQuest(allQuestsBase.GetQuest(myEventData.questIDs[0]));
                        break;
                    case EventScriptableObject.EventType.SpawnItem:
                        // safety check
                        if (myEventData.questIDs[0]!= 0)
                            _gameEventManager.SpawnObject(myEventData.questIDs[0]);
                        break;
                    case EventScriptableObject.EventType.SpawnEnemy:
                        if (myEventData.questIDs[0]!= 0)
                            _gameEventManager.SpawnEnemies(myEventData.questIDs[0]);
                        break;
                    case EventScriptableObject.EventType.Cutscene:
                        //layCutscene();
                        break;
                    case EventScriptableObject.EventType.LocationChange:
                        if (myEventData.questIDs[0]!= 0)
                            _gameEventManager.ChangeScenery(myEventData.questIDs[0]);
                        break;
                }
            }
        }

        public void EventEnd()
        {
            if (myEventData.thisEventType == EventScriptableObject.EventType.Panel)
            {
                _eventPanel.eventDone -= EventEnd;
            }
            
            eventObjectDone = true;
            _eventsDatabase.RemoveEvent(myEventData);
            this.gameObject.SetActive(false);
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
            ExtendedComponentData data = new ExtendedComponentData();
            data.SetBool("doneCheck", eventObjectDone);
            return data;
        }

        public void Deserialize(ComponentData data)
        {
            ExtendedComponentData unpacked = (ExtendedComponentData)data;
            eventObjectDone = unpacked.GetBool("doneCheck");
            if (eventObjectDone)
            {
                EventEnd();
            }
        }
    }
}

