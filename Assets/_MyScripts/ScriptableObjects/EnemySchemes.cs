using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScheme", menuName = "ScriptableObjects/EnemySchemeScriptableObject", order = 2)]
public class EnemySchemes : ScriptableObject
{
    public enum FightState {
        Attack, 
        Block, 
        Dodge,
        Retreat
        }

    [field: SerializeField] public List<FightState> AttackTact {get; private set;}
}
