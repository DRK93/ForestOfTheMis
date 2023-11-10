using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockingData", menuName = "ScriptableObjects/BlockingDataScriptableObject", order = 2)]
public class BlockingAnimScriptableObject : ScriptableObject
{
    [field: SerializeField] public string AnimationName{get; private set;}
    // blocking number - to blocking logic new, what weapon/shield is used while blocking state
    [field: SerializeField] public int BlockingNumber{get; private set;}
    [field: SerializeField] public float TransisionDuration{get; private set;}
    [field: SerializeField] public float RotateTowardsEnemyTimer {get; private set;}
    [field: SerializeField] public float StartBlockingTime{get; private set;}
    // animation block ends with lowering guard, so at the end of animation trigger 
    //for end blocking logic go without specific timer setted here
    //[field: SerializeField] public float EndBlockingTime {get; private set;} 
    // if counter is possible at all it will be able after passing this normalizeTime point
    [field: SerializeField] public float StartCounterWindow{get; private set;}
}
