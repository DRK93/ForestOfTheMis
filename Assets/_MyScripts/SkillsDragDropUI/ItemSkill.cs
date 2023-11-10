using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameDevTV.Core.UI.Dragging;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using _MyScripts.GameMenu;

namespace _MyScripts.SkillsDragDropUI
{
        public class ItemSkill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public enum SlotItemRole
        {
            BaseAttack,
            AdvanceAttack,
            SpecialAttack,
            Action,
            Potion,
            NotToDrop
        }

        // this enum will set where could something be placed on skillBar
        public SlotItemRole itemRole;

        public List<WeaponStatsScriptableObject.WeaponType> weaponType = new List<WeaponStatsScriptableObject.WeaponType>();
        // data conntainer for SO with anim data
        public AttackScriptableObject attackData;
        // icon of itemskill
        [FormerlySerializedAs("ItemSkillIcon")] public Sprite itemSkillIcon;
        // only for Potion ? 
        public int attackID;
        public int amount;
        private SkillDescriptionManager _skilDescription;
        private void Start()
        {
            _skilDescription = GameObject.FindWithTag("SkillWindowInfo").GetComponent<SkillDescriptionManager>();
            if (attackID != 0)
            {
                StartCoroutine(DelayToSendSkills());
            }
        }

        public void SetItem(ItemSkill item)
        {
            var iconImage = GetComponent<Image>();
            if (item == null)
            {
                iconImage.enabled = false;

            }
            else
            {
                itemRole = item.itemRole;
                attackData = item.attackData;
                itemSkillIcon = item.itemSkillIcon;
                amount = item.amount;
                iconImage.enabled = true;
                iconImage.sprite = itemSkillIcon;
                attackID = item.attackID;
            }
        }

        public ItemSkill GetItem()
        {
            var iconImage = GetComponent<Image>();
            if (!iconImage.enabled)
            {
                return null;
            }

            var newItem = GetComponent<ItemSkill>();
            newItem.itemSkillIcon = itemSkillIcon;
            newItem.amount = amount;
            newItem.attackData = attackData;
            newItem.itemRole = itemRole;
            newItem.attackID = attackID;
            return newItem;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _skilDescription.ShowWindow();
            if(attackData != null)
                _skilDescription.SetAttackData(attackData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _skilDescription.HideWindow();
        }
        
        private IEnumerator DelayToSendSkills()
        {
            yield return new WaitForSeconds(0.1f);
            SendSkillToManager();
        }

        private void SendSkillToManager()
        {
            GameObject.FindWithTag("SkillManager").GetComponent<ItemSkillManager>().listOfAllSkils.Add(this);
        }
    }
}

