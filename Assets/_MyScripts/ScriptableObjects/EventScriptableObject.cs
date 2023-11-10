using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventData", menuName = "ScriptableObject/EventDataScriptableObject", order = 1)]
public class EventScriptableObject : ScriptableObject
{
    [field: SerializeField] public string eventText;
    [field: SerializeField] public int eventOptionsNumber;
    [field: SerializeField] public List<string> eventOptionText;
    [field: SerializeField] public List<int> questIDs;

}
