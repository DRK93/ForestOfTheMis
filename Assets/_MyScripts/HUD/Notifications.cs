using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.HUD
{
    public class Notifications : MonoBehaviour
    {
        public GameObject notificationWrongWeapon;
        public GameObject notifiactionWrongSlot;
        public GameObject notEnoughtMana;
        public GameObject notEnoughtStamina;
        public GameObject skillOnCooldown;
        public GameObject noFirstAttack;
        public GameObject levelUp;
        public GameObject playerDead;
        
        public Action hideThis;
        
        private GameObject _currentNotification;
        private void ShowNotification(GameObject notificationObject)
        {
            if (hideThis == null)
            {
                hideThis += HideThisObject;
                _currentNotification = notificationObject;
                notificationObject.SetActive(true);
                LeanTween.alpha(notificationObject, 255f, 0.2f);
                LeanTween.alpha(notificationObject, 0f, 5f).setOnComplete(hideThis);
            }
            else
            {
                //Debug.Log("Notification ongoing");
            }
            
        }
        private void HideThisObject()
        {
            _currentNotification.SetActive(false);
            hideThis -= HideThisObject;
        }
        
        public void ObjectNotification (int objectNumber)
        {
            switch (objectNumber)
            {
                case 1:
                    ShowNotification(notificationWrongWeapon);
                    break;
                case 2:
                    ShowNotification((notifiactionWrongSlot));
                    break;
                case 3:
                    ShowNotification((notEnoughtMana));
                    break;
                case 4:
                    ShowNotification(notEnoughtStamina);
                    break;
                case 5:
                    ShowNotification(skillOnCooldown);
                    break;
                case 6:
                    ShowNotification(noFirstAttack);
                    break;
                case 7:
                    ShowNotification(levelUp);
                    break;
                case 8:
                    ShowNotification(playerDead);
                    break;
                default:
                    break;
            }
        }
    }
}

