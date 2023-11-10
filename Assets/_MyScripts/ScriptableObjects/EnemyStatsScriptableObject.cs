using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatsData", menuName = "ScriptableObjects/EnemyStatsScriptableObject", order = 2)]
public class EnemyStatsScriptableObject : ScriptableObject
{
    public enum EnemyLocomotion
    {
        Shield,
        TwoHanded,
        Spear,
        Default
    }
    [field: SerializeField] public float EnemyHealth {get; private set;}
    [field: SerializeField] public float EnemyValue {get; private set;}
    [field: SerializeField] public float RunningSpeed {get; private set;}
    [field: SerializeField] public float WalkingSpeed {get; private set;}
    [field: SerializeField] public float DetectionRange {get; private set;}
    [field: SerializeField] public float CriticChance {get; private set;}
    [field: SerializeField] public int EnemyLevel { get; private set; }
    [field: SerializeField] public string EnemyName { get; private set; }
    [field: SerializeField] public int SwordmanshipLevel {get; private set;}
    [field: SerializeField] public int CounterRate {get; private set;}
    [field: SerializeField] public EnemyLocomotion enemyLocomotion {get; private set;}
    [field: SerializeField] public List<AttackScriptableObject> AttackList {get; private set;}
    [field: SerializeField] public List<BlockingAnimScriptableObject> BlockList {get; private set;}
    [field: SerializeField] public List<AttackScriptableObject> CounterList {get; private set;}
    [field: SerializeField] public List<AttackScriptableObject> SpecialAttack {get; private set;}
    [field: SerializeField] public List<EnemySchemes> EnemySchemes {get; private set;}

}
