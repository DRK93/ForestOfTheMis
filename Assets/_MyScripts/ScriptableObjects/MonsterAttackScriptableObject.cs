using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterAttackData", menuName = "ScriptableObjects/MonsterAttackDataScriptableObject", order = 1)]
public class MonsterAttackScriptableObject : ScriptableObject
{
    [field: SerializeField] public string AnimationName{get; private set;}
    [field: SerializeField] public float TransisionDuration{get; private set;}
    [field: SerializeField] public bool BlockableAttack {get; private set;}
    [field: SerializeField] public float RotateTowardsEnemyTimer {get; private set;}
    [field: SerializeField] public float CriticChance{get; private set;}
    [field: SerializeField] public float NextAttackStartTime{get; private set;}
    [field: SerializeField] public float EndAttackTime {get; private set;}
    [field: SerializeField] public List<int> WeaponPlacement{get; private set;}
    [field: SerializeField] public List<float> AttackRange { get; private set; }
    [field: SerializeField] public List<float> ForceTimers {get; private set;}
    [field: SerializeField] public List<float> ArmAttackTimes {get; private set;}
    [field: SerializeField] public List<float> DisarmAttackTimes {get; private set;}
    [field: SerializeField] public List<float> MoveForces {get; private set;}
    [field: SerializeField] public List<float> Knockbacks {get; private set;}
    [field: SerializeField] public List<int> Damages{get; private set;}
    [field: SerializeField] public List<int> BlockCost { get; private set; }
    [field: SerializeField] public int ManaCost { get; private set; }
    [field: SerializeField] public int PowerCost { get; private set; }
}
