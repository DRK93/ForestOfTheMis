using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.Combat.WeaponMechanics
{
    public class WeaponToPlayerManager : MonoBehaviour
    {
        // Start is called before the first frame update
        private bool _secondWeapon;
        private GameObject _player;
        void Start()
        {
            _player = GameObject.FindWithTag("Player");
            SendWeaponsToManager();
        }

        private void SendWeaponsToManager()
        {
            if (transform.TryGetComponent<SecondWeapon>(out SecondWeapon secondWeapon))
                _secondWeapon = true;
        
            for (int i = 0; i < transform.childCount; i++)
            {
                if (_secondWeapon)
                {
                    _player.GetComponent<WeaponManager>().SetWeaponObject(transform.GetChild(i).GetComponent<WeaponLayerSetter>().WeaponData, true, transform.GetChild(i).gameObject);
                }
                else
                {
                    _player.GetComponent<WeaponManager>().SetWeaponObject(transform.GetChild(i).GetComponent<WeaponLayerSetter>().WeaponData, false, transform.GetChild(i).gameObject);
                }
            }
        }
    }
}

