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
                foreach (var questIndex in questIDs)
                {
                    questManager.UpdateQuestAmount(1, questIndex);
                }
                
                questObjectTriggered = true;
                DeactiveQuestObject();
            }
        }
    }
}

