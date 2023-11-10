using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestJournalParts", menuName = "ScriptableObject/QuestJournalPartsScriptableObject", order = 1)]
public class QuestJournalPartsScriptableObjects : ScriptableObject
{
    [field: SerializeField] public int questID;
    [field: SerializeField] public string journalText;
    [field: SerializeField] public int partIndex;
}
