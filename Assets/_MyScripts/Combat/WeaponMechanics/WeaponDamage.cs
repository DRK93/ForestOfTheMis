using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace _MyScripts.Combat.WeaponMechanics
{
    [RequireComponent(typeof(WeaponLayerSetter))]
public class WeaponDamage : MonoBehaviour
{
    
    [SerializeField] private Collider myCollider;
    //Temporaly SerializeField
    [SerializeField] private int damage;
    [SerializeField] private float knockback;
    [SerializeField] private bool blockable;
    [SerializeField] private int blockCost;
    
    private List<Collider> _alreadyColliderWith = new List<Collider>();
    private WeaponLayerSetter _weaponLayerSetter;
    
    /*[SerializeField] private List<Transform> pointsForRayCast = new List<Transform>();
    [SerializeField] private LayerMask _layerMask;*/
    public event Action SwingWeapon;

    private void Start()
    {
        _weaponLayerSetter = GetComponent<WeaponLayerSetter>();
    }

    private void OnEnable()
    {
        myCollider.enabled = true;
        _alreadyColliderWith.Clear();
        SwingWeapon?.Invoke();
    }
    
    private void OnDisable()
    {
        myCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) {return;}
        if(_alreadyColliderWith.Contains(other)) {}
        else
        {
            _alreadyColliderWith.Add(other);
        }
        
        if(other.TryGetComponent<Damageable>(out Damageable damageable))
        {
            if(damageable.blocking)
            {
                Vector3 direction = other.transform.position - _weaponLayerSetter.WeaponParentTransform.position;
                direction.Normalize();
                float dotProd = Vector3.Dot(direction, other.transform.forward);
                if ( dotProd < -0.6f)
                {
                    bool blockBroken;
                    if (damageable.Power() > blockCost)
                    {
                        blockBroken = false;
                    }
                    else
                    {
                        blockBroken = true;
                    }
                    damageable.PowerCost(blockCost);
                    
                    if (!blockBroken)
                    {
                        Damageable damageable1 = other.GetComponentInParent<Damageable>();
                        damageable1.DoneBlock();
                        if (blockable)
                        {
                            _weaponLayerSetter.WeaponParentDamageable.AttackBlocked();
                            //_parentDamageable.AttackBlocked();
                        }
                        else
                        {
                            Debug.Log("Attack animation not blockable");
                        }
                    }
                    else
                    {
                        int brokenDamage = (int)(damage * 0.5f);
                        damageable.CalculateDamage(brokenDamage, knockback, _weaponLayerSetter.WeaponParentTransform);
                        //damageable.CalculateDamage(brokenDamage, knockback, _parentTransform);
                        _weaponLayerSetter.WeaponParentDamageable.AttackSuccesful();
                        //_parentDamageable.AttackSuccesful();
                    }
                }
                else
                {
                    damageable.CalculateDamage(damage, knockback, _weaponLayerSetter.WeaponParentTransform);
                    //damageable.CalculateDamage(damage, knockback, _parentTransform);
                    _weaponLayerSetter.WeaponParentDamageable.AttackSuccesful();
                    //_parentDamageable.AttackSuccesful();
                }
            }
            else
            {
                damageable.CalculateDamage(damage, knockback, _weaponLayerSetter.WeaponParentTransform);
                //damageable.CalculateDamage(damage, knockback, _parentTransform);
                _weaponLayerSetter.WeaponParentDamageable.AttackSuccesful();
                //_parentDamageable.AttackSuccesful();
            }
        }
    }
    
    public void SetAttack(int damage, float knockback, bool blockableAttack, int blockCost)
    {
        //Debug.Log("Set attack parameters");
        this.damage = damage;
        this.knockback = knockback;
        blockable = blockableAttack;
        this.blockCost = blockCost;
    }
    
    // aproach via physics spherecast
    /*private void Update()
    {
        if (pointsForRayCast.Count > 0)
        {
            Debug.Log("Ray");
            var direction = pointsForRayCast[^1].position - pointsForRayCast[0].position;
            var distance = Vector3.Distance(pointsForRayCast[^1].position,
                pointsForRayCast[0].position);
                //if (Physics.Raycast(pointsForRayCast[0].position, direction, out RaycastHit hit, 1f,_layerMask))
                if(Physics.SphereCast(pointsForRayCast[0].position, 0.3f, direction, out RaycastHit hit, distance,_layerMask))
                {
                    Debug.Log(hit.collider);
                }
                //if (Physics.SphereCast(pointsForRayCast[0].position, 0.3f, pointsForRayCast[0].up, out RaycastHit hit2, distance,_layerMask))
                    //Debug.Log("Second " + hit2.collider);
        }
    }*/
    
    /*private void SetParentDirection()
    {
        if (this.transform.root.TryGetComponent<Damageable>(out Damageable damageable))
        {
            _parentDamageable = damageable;
            _parentTransform = this.transform.root;
        }
        else
        {
            SearchForParentDamageable(transform.gameObject);
        }
    }*/

    /*private void SearchForParentDamageable(GameObject checkObject)
    {
        if (checkObject.transform.parent.TryGetComponent<Damageable>(out Damageable damageable))
        {
            _parentDamageable = damageable;
            _parentTransform = damageable.transform;
        }
        else
        {
            SearchForParentDamageable(checkObject.transform.parent.gameObject);
        }
    }*/

            // if (other.TryGetComponent<WeaponBlocker>(out WeaponBlocker weaponBlocker))
        // {
        //     Debug.Log("Shield");
        //     //Vector3 direction = weaponBlocker.transform.position - transform.parent.root.transform.position;
        //     Vector3 direction2 = weaponBlocker.transform.parent.root.transform.position - transform.parent.root.transform.position;
        //     direction2.Normalize();
        //     //float dotProd2 = Vector3.Dot(weaponBlocker.transform.parent.root.transform.forward, transform.parent.root.transform.forward);
        //     //float dotProd = Vector3.Dot(direction, transform.parent.root.transform.forward);
        //     float dotProd3 = Vector3.Dot(direction2, weaponBlocker.transform.parent.root.transform.forward);
        //     //Debug.Log("DotProd2 " + dotProd2);
        //     Debug.Log("DotProd3 " + dotProd3 + " " + transform.parent.root);
        //     //if ( dotProd > 0.6f)
        //     if ( dotProd3 < -0.63f)
        //     {
        //         bool blockBroken = false;
        //         if (!blockBroken)
        //         {
        //             Debug.Log("Blocked attack");
        //             Damageable damageable1 = other.GetComponentInParent<Damageable>();
        //             damageable1.DoneBlock();
        //             //damageable1.CalculateDamage(0, knockback , parentTransform);
        //             parentDamageable.AttackBlocked();
        //             BlockedAttack?.Invoke();
        //             return;
        //         }
        //         else
        //         {
        //             //Debug.Log("Block broken");
        //             Damageable damageable1 = other.GetComponentInParent<Damageable>();
        //             //if(other.TryGetComponent<Damageable>(out Damageable damageable1))
        //             if(damageable1 != null)
        //             {
        //                 int brokenDamage = (int)(damage * 0.5f);
        //                 damageable1.CalculateDamage(brokenDamage, knockback * 0.5f, parentTransform);
        //                 HurtCollider.Add(damageable1);
        //             }
        //         }

        //     }
        //     else
        //     {
        //         Debug.Log("Above block");
        //     }
        // }
}
}

