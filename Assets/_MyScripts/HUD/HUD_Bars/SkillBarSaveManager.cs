using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NGS.ExtendableSaveSystem;

namespace _MyScripts.HUD.HUD_Bars
{
    public class SkillBarSaveManager : MonoBehaviour, ISavableComponent
    {
        // check to acivate bars
        [SerializeField] private List<GameObject> barsToActivate;
        [SerializeField] private List<bool> barsActive;
        private int _uniqueID = 200;
        private int _executionOrder;
        private bool _simpleCheck;
        public int uniqueID => _uniqueID;
    
        public int executionOrder => _executionOrder;
    
        public ComponentData Serialize()
        {
            int indexer1 = 0;
            int indexer2 = 0;
            foreach (var bar in barsToActivate)
            {
                barsActive.Add(false);
                if (bar.activeInHierarchy)
                {
                    barsActive[indexer1] = true;
                }
                else
                {
                    barsActive[indexer1] = false;
                }
    
                indexer1++;
            }
    
            ExtendedComponentData data = new ExtendedComponentData();
    
            foreach (var activeBar in barsActive)
            {
                string barName = "bar" + indexer2;
                data.SetBool(barName, activeBar);
                indexer2++;
            }
    
            return data;
    
        }
    
        public void Deserialize(ComponentData data)
        {
            int indexer1 = 0;
            int indexer2 = 0;
            ExtendedComponentData unPacked = (ExtendedComponentData)data;
            foreach (var bar in barsToActivate)
            {
                string barName = "bar" + indexer1;
                barsActive[indexer1] = unPacked.GetBool(barName);
                indexer1++;
            }
    
            foreach (var bar in barsToActivate)
            {
                if(barsActive[indexer2])
                    bar.SetActive(true);
                else if(bar.activeInHierarchy)
                {
                    bar.SetActive(false);
                }
                indexer2++;
            }
        }
    }
}



