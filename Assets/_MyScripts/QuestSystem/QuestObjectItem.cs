using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.QuestSystem
{
    public class QuestObjectItem : QuestObject
    {
        private bool _allowInteraction;
        public void OnTriggerEnter(Collider other)
        {
            _allowInteraction = true;
        }

        public void OnTriggerExit(Collider other)
        {
            _allowInteraction = false;
        }

        public void InteractWithObject()
        {
            if (_allowInteraction)
                QuestThing();
        }
        // only when player click to interact with something
        public void QuestThing()
        {
            if (!questObjectTriggered)
            {
                questManager.UpdateQuestAmount(1, questObject.questID);
                questObjectTriggered = true;
                DeactiveQuestObject();
            }
        }
    }
}

