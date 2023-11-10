using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NGS.ExtendableSaveSystem;
namespace _MyScripts.QuestSystem
{
    public class QuestObject : MonoBehaviour, ISavableComponent
    {
        public QuestScriptbleObject questObject;
        public QuestJournalPartsScriptableObjects questToJournal;
        public QuestManager questManager;
        public bool questObjectTriggered;
        [SerializeField] private int _uniqueID;
        [SerializeField] private int _executionOrder;

        // Start is called before the first frame update
        void Start()
        {
            GameObject.FindWithTag("QuestBase").GetComponent<AllQuestsInOnePlace>().allQuestsList.Add(questObject);
            questManager = GameObject.FindWithTag("QuestManager").GetComponent<QuestManager>();
        }

        // there could be many ways to trigger this:
        // onTriggerEnter
        // killed enemy
        // after eventPanel decision
        

        public int uniqueID => _uniqueID;

        public int executionOrder => _executionOrder;

        public ComponentData Serialize()
        {
            ExtendedComponentData data = new ExtendedComponentData();
            data.SetBool("triggeredQuestObject", questObjectTriggered);
            return data;
        }

        public void Deserialize(ComponentData data)
        {
            ExtendedComponentData unpacked = (ExtendedComponentData)data;
            questObjectTriggered = unpacked.GetBool("triggeredQuestObject");
            UpdateAfterLoad();
        }

        private void UpdateAfterLoad()
        {
            if (questObjectTriggered)
            {
                this.enabled = false;
            }
        }
    }
}

