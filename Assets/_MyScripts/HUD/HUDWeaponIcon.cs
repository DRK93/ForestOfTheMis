using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.HUD
{
    public class HUDWeaponIcon : MonoBehaviour
    {
        public GameObject shieldIcon;
        public GameObject spearIcon;
        public GameObject swordIcon;
    
        public void ChangeActiveWeaponToShield()
        {
            shieldIcon.SetActive(true);
            swordIcon.SetActive(false);
            spearIcon.SetActive(false);

        }
        public void ChangeActiveWeaponToTwoHanded()
        {
            shieldIcon.SetActive(false);
            swordIcon.SetActive(true);
            spearIcon.SetActive(false);
        }

        public void ChangeActiveWeaponToSpear()
        {
            shieldIcon.SetActive(false);
            swordIcon.SetActive(false);
            spearIcon.SetActive(true);
        }
    }
}

