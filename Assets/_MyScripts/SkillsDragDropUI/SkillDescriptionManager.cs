using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace _MyScripts.SkillsDragDropUI
{
   public class SkillDescriptionManager : MonoBehaviour
   {
      [SerializeField] private GameObject windowInAttackView;
      [SerializeField] private TextMeshProUGUI attackName;
      [SerializeField] private TextMeshProUGUI attackDescription;
      [SerializeField] private TextMeshProUGUI attackDamage;
      [SerializeField] private TextMeshProUGUI attackRange;
      [SerializeField] private TextMeshProUGUI attackSpeed;
      [SerializeField] private TextMeshProUGUI powerCost;
      [SerializeField] private TextMeshProUGUI manaCost;
      [SerializeField] private TextMeshProUGUI multiplyAttack;
      
      private bool _showedWindow;
      private bool _notAllowed;
   
      public void ShowWindow()
      {
         if (!_notAllowed)
         {
            windowInAttackView.SetActive(true);
            _showedWindow = true;
         }
         
      }
   
      public void HideWindow()
      {
         if (_showedWindow)
         {
            windowInAttackView.SetActive(false);
            _showedWindow = false;
         }
         
      }
   
      public void NotShowingWindow()
      {
         windowInAttackView.SetActive(false);
         _notAllowed = true;
      }
   
      public void AllowShowingWindow()
      {
         _notAllowed = false;
      }
   
      public void SetAttackData(AttackScriptableObject attackData)
      {
         attackName.text = attackData.name;
         attackDescription.text = attackData.Description;
         int damage = 0;
         for (int i = 0; i < attackData.Damages.Count ; i++)
         {
            damage += attackData.Damages[i];
         }
         attackDamage.text = damage.ToString();
         attackRange.text = attackData.AttackRange[0].ToString();
         attackSpeed.text = attackData.Speed;
         powerCost.text = attackData.powerCost.ToString();
         manaCost.text = attackData.manaCost.ToString();
         if (attackData.MultipleAttack)
         {
            multiplyAttack.text = "yes";
         }
         else
         {
            multiplyAttack.text = "no";
         }
         
      }
   }
}

