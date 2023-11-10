using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.QuestSystem
{
    public class QuestObjectEnemy : QuestObject
    {
        public void TriggerQuestAfterEnemyDeath()
        {
            if (!questObjectTriggered)
            {
                questManager.UpdateQuestAmount(1, questObject.questID);
                questObjectTriggered = true;
            }
        }
    }
}

