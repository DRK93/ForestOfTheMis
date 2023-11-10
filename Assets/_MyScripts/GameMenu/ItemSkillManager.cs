using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _MyScripts.SkillsDragDropUI;

namespace _MyScripts.GameMenu
{
    public class ItemSkillManager : MonoBehaviour
    {
        public List<ItemSkill> listOfAllSkils;
        public GameObject AttackPanel;

        void Start()
        {
            listOfAllSkils = new List<ItemSkill>();
            StartCoroutine(HidePanel());
        }

        private IEnumerator HidePanel()
        {
            yield return new WaitForSeconds(0.2f);
            AttackPanel.SetActive(false);
        }

        public ItemSkill FindThatSkill(int number)
        {
            int index = 0;


            foreach (var skill in listOfAllSkils)
            {
                if (number == skill.attackID)
                {
                    Debug.Log(index);
                    break;
                }
                index++;
            }
            Debug.Log("Skill number " + number);


            //Debug.Log(index);
            return listOfAllSkils[index];
        }
    }
}

