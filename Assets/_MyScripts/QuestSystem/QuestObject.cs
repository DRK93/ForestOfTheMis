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
        public AllQuestsInOnePlace questBase;
        public bool questObjectTriggered;
        public List<int> questIDs;
        [SerializeField] private int _uniqueID;
        [SerializeField] private int _executionOrder;

        public int uniqueID => _uniqueID;
        public int executionOrder => _executionOrder;
        // Start is called before the first frame update
        void Start()
        {
            questBase = GameObject.FindWithTag("QuestBase").GetComponent<AllQuestsInOnePlace>();
            questBase.allQuestsList.Add(questObject);
            questManager = GameObject.FindWithTag("QuestManager").GetComponent<QuestManager>();
        }
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

        public void DeactiveQuestObject()
        {
            this.gameObject.SetActive(false);
            this.enabled = false;
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

