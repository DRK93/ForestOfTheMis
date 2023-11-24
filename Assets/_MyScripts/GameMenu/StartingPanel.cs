using System.Collections;
using System.Collections.Generic;
using NGS.ExtendableSaveSystem;
using UnityEngine;

namespace _MyScripts.GameMenu
{
    public class StartingPanel : MonoBehaviour, ISavableComponent
    {
        public bool startingPanelDone;
        public GameObject startingPanel;
        public GameObject introductionPanel;
        public GameObject baseSetup;
        [SerializeField] private TemporalWeaponManager temporalWeaponManager;
        private int _uniqueID;
        private int _executionOrder;

        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 0f;
            startingPanel.SetActive(true);
        }
        

        public void BeginSetup()
        {
            introductionPanel.SetActive(false);
            baseSetup.SetActive(true);
        }

        public void FirstWeapon(int number)
        {
            temporalWeaponManager.ChangeWeapon(number);
            EndIntroduction();
        }
        private void EndIntroduction()
        {
            startingPanelDone = true;
            Time.timeScale = 1f;
            startingPanel.SetActive(false);
        }

        public int uniqueID => _uniqueID;

        public int executionOrder => _executionOrder;

        public ComponentData Serialize()
        {
            ExtendedComponentData data = new ExtendedComponentData();
            data.SetBool("startDone", startingPanelDone);
            return data;
        }

        public void Deserialize(ComponentData data)
        {
            AfterLoadGame();
        }

        private void AfterLoadGame()
        {
            EndIntroduction();
        }
    }
}

