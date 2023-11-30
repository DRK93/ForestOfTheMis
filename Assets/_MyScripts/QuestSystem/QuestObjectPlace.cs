using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.QuestSystem
{
    public class QuestObjectPlace : QuestObject
    {
        public void OnTriggerEnter(Collider other)
        {
            TriggerQuest();
        }

        public void TriggerQuest()
        {
            if (!questObjectTriggered)
            {
                //questManager.AddNewQuest(questObject);
                questManager.UpdateQuestAmount(1, questObject.questID);
                questObjectTriggered = true;
                DeactiveQuestObject();
            }
        }
    }
}

