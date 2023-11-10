using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.ShowingObjects
{
    public class EnemyWeaponToShow : MonoBehaviour
    {
        [SerializeField] private GameObject mainWeapon;
    
        [SerializeField] private GameObject secondWeapon;
    
        [SerializeField] private float timeToWait = 0.5f;
        private bool _weaponActivated;
    
        private void OnEnable()
        {
            //if(!_weaponActivated)
                StartCoroutine(DelayToShowWeapons());
        }
    
        private void OnDisable()
        {
            if (!_weaponActivated)
            {
                if (mainWeapon != null)
                {
                    mainWeapon.SetActive(false);
                    _weaponActivated = true;
                }
    
                if (secondWeapon != null)
                {
                    secondWeapon.SetActive(false);
                    _weaponActivated = true;
                }
            }
            StopAllCoroutines();
        }
    
        private void ActiveWeapons()
        {
            if (mainWeapon != null)
            {
                mainWeapon.SetActive(true);
                _weaponActivated = true;
            }
    
            if (secondWeapon != null)
            {
                secondWeapon.SetActive(true);
                _weaponActivated = true;
            }
        }
    
        private IEnumerator DelayToShowWeapons()
        {
            yield return new WaitForSeconds(timeToWait);
            ActiveWeapons();
        }
    }
}

