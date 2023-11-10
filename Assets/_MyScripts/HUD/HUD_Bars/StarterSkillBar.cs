using System;
using System.Collections;
using System.Collections.Generic;
using NGS.ExtendableSaveSystem;
using UnityEngine;

namespace _MyScripts.HUD.HUD_Bars
{
    public class StarterSkillBar : MonoBehaviour, ISavableComponent
    {
        public bool ActiveOnStart;
        public bool activeBars;
        [SerializeField] private int _uniqueID;
        [SerializeField] private int _executionOrder;
        public int uniqueID => _uniqueID;

        public int executionOrder => _executionOrder;
        private void OnEnable()
        {
            activeBars = true;
        }
        private void Start()
        {
            if(!ActiveOnStart)
                gameObject.SetActive(false);
        }
        private void OnDisable()
        {
            activeBars = false;
        }
    
        public ComponentData Serialize()
        {
            ExtendedComponentData data = new ExtendedComponentData();
            data.SetBool("ActiveSkillBars", activeBars);
            return data;
        }

        public void Deserialize(ComponentData data)
        {
        
            ExtendedComponentData unPacked = (ExtendedComponentData)data;
            activeBars = unPacked.GetBool("ActiveSkillBars");
            StartCoroutine(DelayedHide());
        }

        private IEnumerator DelayedHide()
        {
            yield return new WaitForSeconds(0.6f);
            if(!activeBars)
                gameObject.SetActive(false);
        }
    }
}

