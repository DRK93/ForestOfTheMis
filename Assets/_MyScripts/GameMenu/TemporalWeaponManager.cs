using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using _MyScripts.Combat.WeaponMechanics;

namespace _MyScripts.GameMenu
{
    public class TemporalWeaponManager : MonoBehaviour
    {
        [field: SerializeField] public WeaponManager WeaponManager { get; private set; }
        // Start is called before the first frame update
        public AudioSource buttonClick;
        public Action buttonClicked;
        public void ChangeWeapon(int weaponGenreNumber)
        {
            SetProperWeapon(weaponGenreNumber);
            buttonClicked?.Invoke();
        }
        private void SetProperWeapon(int weaponType)
        {
            WeaponManager.TemporalSetWeapon(weaponType);
            if (weaponType == 1)
            {
                WeaponManager.TemporalSetWeapon(4);
            }
        }
        private void PlayClickAudio()
        {
            buttonClick.Play();
        }
    }
}

