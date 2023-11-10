using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "WeaponStats", menuName = "ScriptableObjects/WeaponStatsScriptableObject", order = 1)]
public class WeaponStatsScriptableObject : ScriptableObject
{
    public enum WeaponType {
        OneHandedSword,
        OneHandedAxe,
        OneHandedMace,
        Shield,
        TwoHandedSword,
        TwoHandedMace,
        TwoHandedAxe,
        Spear,
        Halberd,
        Bow,
        Empty
    }
    [field: SerializeField] public WeaponType ThisWeaponType {get; private set;}
    [field: SerializeField] public float WeaponRange {get; private set;}
    [field: SerializeField] public int WeaponDamage {get; private set;}
    [field: SerializeField] public float WeaponWeight {get; private set;}
    [field: SerializeField] public string WeaponName {get; private set;}
}
