using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "ScriptableObjects/AttackDataScriptableObject", order = 1)]
public class AttackScriptableObject : ScriptableObject
{
    // list is used because there could be combo attack animation in someanimation.
    // otherwise it will be using list first position on everything
    // If it is an attack from one combo animation this parameter will be able to get the moment to start particular attack
    // without cutting animation in editor, just from some numbers
    [field: SerializeField] public string AnimationName{get; private set;}
    [field: SerializeField] public int AttackGripNumber { get; private set; }
    [field: SerializeField] public float TransisionDuration{get; private set;}
    [field: SerializeField] public bool ComboAniamtion {get; private set;}
    [field: SerializeField] public bool BlockableAttack {get; private set;}
    [field: SerializeField] public List<bool> RightHand{get; private set;}
    [field: SerializeField] public float RotateTowardsEnemyTimer {get; private set;}
    [field: SerializeField] public float CriticChance{get; private set;}
    [field: SerializeField] public float NextAttackWindow{get; private set;}
    [field: SerializeField] public float NextAttackStartTime{get; private set;}
    [field: SerializeField] public float EndAttackTime {get; private set;} 
    [field: SerializeField] public float BlockCrossFadeDuration {get; private set;} 
    [field: SerializeField] public List<float> AttackRange { get; private set; }
    [field: SerializeField] public List<float> ForceTimers {get; private set;}
    [field: SerializeField] public List<float> ArmAttackTimes {get; private set;}
    [field: SerializeField] public List<float> DisarmAttackTimes {get; private set;}
    [field: SerializeField] public List<float> BlockedAnimTimers {get; private set;}
    [field: SerializeField] public List<float> ReDoBlockedAttackTimers {get; private set;}
    [field: SerializeField] public List<float> MoveForces {get; private set;}
    [field: SerializeField] public List<float> Knockbacks {get; private set;}
    [field: SerializeField] public List<int> Damages{get; private set;}
    [field: SerializeField] public List<int> BlockCost { get; private set; }
    [field: SerializeField] public int manaCost { get; private set; }
    [field: SerializeField] public int powerCost { get; private set; }
    [field: SerializeField] public string Speed { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public bool MultipleAttack { get; private set; }
    [field: SerializeField] public AnimationCurve AnimationSpeedCurve{get; private set;}
    // if there will be added some special effect to attack, it will go by animation curve 
    [field: SerializeField] public bool SpecialEffects{get; private set;}
    [field: SerializeField] public AnimationCurve AnimationSpecialEffectsCurve {get; private set;}
}
