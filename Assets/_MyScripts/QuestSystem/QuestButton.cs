using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace _MyScripts.QuestSystem
{
    public class QuestButton : MonoBehaviour
    {
        [field: SerializeField] private TextMeshProUGUI questButtonName;
        private QuestManager _myQuestManager;
        private QuestJournal _myQuestJournal;
        private int _siblingNumber = 0;


        private void Start()
        {
            _myQuestManager = GameObject.FindWithTag("QuestManager").GetComponent<QuestManager>();
            _myQuestJournal = GameObject.FindWithTag("QuestJournal").GetComponent<QuestJournal>();
            _siblingNumber = transform.GetSiblingIndex();
            SetButtonText();
        }

        public void QuestButtonClicked()
        {
            _siblingNumber = transform.GetSiblingIndex();
            NumberOfButton(_siblingNumber);
        }

        public void NumberOfButton(int number)
        {
           _myQuestJournal.ShowQuest(number); 
        }

        private void SetButtonText()
        {
            questButtonName.text = _myQuestManager.acceptedQuests[_siblingNumber].questName;
        }
    }
}

