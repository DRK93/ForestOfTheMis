using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.Combat.WeaponMechanics
{
    public class WeaponHandlerer : MonoBehaviour
    {   
        [SerializeField] private GameObject mainWeaponLogic;
        private WeaponDamage _mainWeaponDamage;
        private WeaponLayerSetter _mainLayerSetter;
        private WeaponDamage _secondWeaponDamage;
        public GameObject MainWeaponLogic
        {
            get { return mainWeaponLogic;}
            set
            {
                if (mainWeaponLogic != null)
                {
                    mainWeaponLogic.SetActive(false);
                }
                mainWeaponLogic = value;
                
                if (mainWeaponLogic != null)
                {
                    mainWeaponLogic.SetActive(true);
                    _mainWeaponDamage = mainWeaponLogic.GetComponent<WeaponDamage>();
                    _mainLayerSetter = mainWeaponLogic.GetComponent<WeaponLayerSetter>();
                    switch (_mainLayerSetter.WeaponData.ThisWeaponType)
                    {
                        case WeaponStatsScriptableObject.WeaponType.Spear:
                            // change in locomotion animation
                            SecondWeaponOff();
                            break;
                        case WeaponStatsScriptableObject.WeaponType.Halberd:
                            SecondWeaponOff();
                            break;
                        case WeaponStatsScriptableObject.WeaponType.TwoHandedSword:
                            SecondWeaponOff();
                            break;
                        case WeaponStatsScriptableObject.WeaponType.TwoHandedMace:
                            SecondWeaponOff();
                            break;
                        case WeaponStatsScriptableObject.WeaponType.TwoHandedAxe:
                            SecondWeaponOff();
                            break;
                        case WeaponStatsScriptableObject.WeaponType.Bow:
                            SecondWeaponOff();
                            break;
                        case WeaponStatsScriptableObject.WeaponType.OneHandedAxe:
                            break;
                        case WeaponStatsScriptableObject.WeaponType.OneHandedSword:
                            break;
                        case WeaponStatsScriptableObject.WeaponType.OneHandedMace:
                            break;
                        case WeaponStatsScriptableObject.WeaponType.Shield:
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        
        // left weapon
        [SerializeField] private GameObject secondWeaponLogic;
        public GameObject SecondWeaponLogic
        {
            get { return secondWeaponLogic;}
            set
            {
                SecondWeaponOff();
                secondWeaponLogic = value; 
                if (secondWeaponLogic != null)
                {
                    secondWeaponLogic.SetActive(true);
                    _secondWeaponDamage = secondWeaponLogic.GetComponent<WeaponDamage>();
                }
            }
        }

        private void SecondWeaponOff()
        {
            if (secondWeaponLogic != null)
            {
                secondWeaponLogic.SetActive(false);
                secondWeaponLogic = null;
            }
        }
        public void EnableMainWeapon(int damage, float knockback, bool blockableAttack, int blockCost)
        {
            _mainWeaponDamage.enabled = true;
            _mainWeaponDamage.SetAttack(damage, knockback, blockableAttack, blockCost);
        }

        public void DisableMainWeapon()
        {
            if(_mainWeaponDamage != null)
                _mainWeaponDamage.enabled = false;
        }

         public void SecondWeaponEnable(int damage, float knockback, bool blockableAttack, int blockCost)
         {
             _secondWeaponDamage.enabled = true;
             _secondWeaponDamage.SetAttack(damage, knockback, blockableAttack, blockCost);
         }
        public void SecondWeaponDisable()
        {
            if (secondWeaponLogic != null)
                _secondWeaponDamage.enabled = false;
        }

    }
}

