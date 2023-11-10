using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventJournalData", menuName = "ScriptableObject/EventJournalDataScriptableObject", order = 1)]
public class EventInJournalScriptableObject : ScriptableObject
{
    [field: SerializeField] public string eventInJournalText;
    [field: SerializeField] public int eventId;
}
