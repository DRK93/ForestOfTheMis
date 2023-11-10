using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "ScriptableObject/QuestDataScriptableObject", order = 1)]
public class QuestScriptbleObject : ScriptableObject
{
    public enum QuestType {
        Kill,
        Investigate,
        BringItem,
        Talk,
        Empty
    }
    
    [field: SerializeField] public string questName;
    [field: SerializeField] public int questID;
    [field: SerializeField] public int questReward;
    [field: SerializeField] public QuestType questType;
    [field: SerializeField] public int amountForComplete;
}
