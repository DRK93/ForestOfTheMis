using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventData", menuName = "ScriptableObject/EventDataScriptableObject", order = 1)]
public class EventScriptableObject : ScriptableObject
{
    public enum EventType
    {
        Panel,
        SpawnQuest,
        SpawnEnemy,
        SpawnItem,
        LocationChange,
        Cutscene
    }

    [field: SerializeField] public EventType thisEventType;
    [field: SerializeField] public int eventID;
    
    // this parameters are for event Panel object and further logic
    [field: SerializeField] public string eventText;
    [field: SerializeField] public int eventOptionsNumber;
    [field: SerializeField] public List<string> eventOptionText;
    [field: SerializeField] public List<EventType> eventTypes;
    // this one wiil be with just one parameter if it's not panel type
    [field: SerializeField] public List<int> questIDs;
    // 0 in list above will means nothing in events logic, just to make it easier from event panel script
}
