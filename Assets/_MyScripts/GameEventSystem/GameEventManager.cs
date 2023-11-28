using System.Collections;
using System.Collections.Generic;
using _MyScripts.QuestSystem;
using UnityEngine;

namespace _MyScripts.GameEventSystem
{
    public class GameEventManager : MonoBehaviour
    {
        [field: SerializeField] private QuestJournal questJournal;
        [field: SerializeField] private EventsDatabase eventBase;

        public List<EventScriptableObject> eventsUnfold;

        public List<EventScriptableObject> eventCompleted;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void SpawnEnemies(int questId)
        {
            
        }

        public void SpawnObject(int questId)
        {
            
        }

        public void ChangeScenery(int questId)
        {
            
        }

        public void SendEventToJournal(EventScriptableObject eventHappend)
        {
            
        }
    }
}

