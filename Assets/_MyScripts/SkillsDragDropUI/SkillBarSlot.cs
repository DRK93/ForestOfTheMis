using System;
using System.Collections;
using System.Collections.Generic;
using _MyScripts.HUD;
using UnityEngine;
using GameDevTV.Core.UI.Dragging;
using UnityEngine.Serialization;

namespace _MyScripts.SkillsDragDropUI
{
    public class SkillBarSlot : MonoBehaviour, IDragContainer<ItemSkill>
    {
        [SerializeField] ItemSkill itemSkill = null;
        [SerializeField] private bool hidingIcon;
        public List<WeaponStatsScriptableObject.WeaponType> weaponType = new List<WeaponStatsScriptableObject.WeaponType>();
        public List<ItemSkill.SlotItemRole> itemRole = new List<ItemSkill.SlotItemRole>();
        
        public int MaxAcceptable(ItemSkill item)
        {
            if (GetItem() == null)
            {
                return int.MaxValue;
            }
            return 0;
        }
    
        public void AddItems(ItemSkill item, int number)
        {
            Debug.Log(weaponType[0]);
            if (item.weaponType[0] == weaponType[0])
            {
                if(item.itemRole == ItemSkill.SlotItemRole.BaseAttack)
                    itemSkill.SetItem(item);
                else if(item.itemRole == ItemSkill.SlotItemRole.AdvanceAttack && itemRole[0] != ItemSkill.SlotItemRole.BaseAttack )
                {
                    itemSkill.SetItem(item);
                }
                else if(item.itemRole == ItemSkill.SlotItemRole.SpecialAttack && itemRole[0] == ItemSkill.SlotItemRole.SpecialAttack)
                {
                    itemSkill.SetItem(item);
                }
                else
                {
                    GameObject.FindWithTag("Notification").GetComponent<Notifications>().ObjectNotification(2);
                }
            }
            else
            {
                if (weaponType[0] == WeaponStatsScriptableObject.WeaponType.Empty)
                {
                    
                }
                else
                {
                    GameObject.FindWithTag("Notification").GetComponent<Notifications>().ObjectNotification(1);
                }
                
            }
        }
    
        public ItemSkill GetItem()
        {
            return itemSkill.GetItem();
        }
    
        public int GetNumber()
        {
            return 1;
        }
    
        public void RemoveItems(int number)
        {
            if(hidingIcon)
                itemSkill.SetItem((null));
        }
    
    }
}

