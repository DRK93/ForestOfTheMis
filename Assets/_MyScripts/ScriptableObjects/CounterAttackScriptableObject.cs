using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CounterData", menuName = "ScriptableObjects/CounterDataScriptableObject", order = 1)]
public class CounterAttackScriptableObject : ScriptableObject
{
    [field: SerializeField] public string AnimationName{get; private set;}
    [field: SerializeField] public float TransisionDuration{get; private set;}
    [field: SerializeField] public float RotateTowardsEnemyTimer {get; private set;}
    [field: SerializeField] public float ArmAttackTime {get; private set;}
    [field: SerializeField] public float DisarmAttackTime {get; private set;}
    [field: SerializeField] public float Force {get; private set;}
    [field: SerializeField] public List<float> MoveForces {get; private set;}
    [field: SerializeField] public float Knockback {get; private set;}
    [field: SerializeField] public List<float> Knockbacks {get; private set;}
    [field: SerializeField] public int Damage{get; private set;}
    [field: SerializeField] public List<int> Damages{get; private set;}
    [field: SerializeField] public float CriticChance{get; private set;}
    [field: SerializeField] public AnimationCurve AnimationSpeedCurve{get; private set;}
}
