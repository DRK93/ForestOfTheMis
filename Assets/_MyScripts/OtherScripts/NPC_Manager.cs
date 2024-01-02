using System;
using System.Collections;
using System.Collections.Generic;
using _MyScripts.GameEventSystem;
using UnityEngine;

namespace _MyScripts.OtherScripts
{
    public class NPC_Manager : MonoBehaviour
    {
        public List<EnemySpawnType.EnemyType> typesInScene;
        public List<int> enemiesCapAmount;
        public List<GameObject> weakBandits;
        public List<GameObject> strongBandits;
        public List<GameObject> mercenaries;
        public List<GameObject> banditBosses;
        public List<GameObject> deers;
        public List<GameObject> bears;
        public List<GameObject> boars;
        public List<GameObject> wolves;
        public List<GameObject> animalBosses;
        public List<GameObject> titans;
        public List<GameObject> aliens;
        public List<GameObject> spiders;
        public List<GameObject> ripperDogs;
        public List<GameObject> pangolians;
        public List<GameObject> monsterBosses;
        
        public List<List<GameObject>> enemiesInGame;

        private void Awake()
        {
            
        }

        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void AddEnemyToList(EnemySpawnType.EnemyType enemy, GameObject enemyObject)
        {
            bool typeCheck = false;
            foreach (var inSceneType in typesInScene)
            {
                if (inSceneType == enemy)
                {
                    typeCheck = true;
                    
                }
            }

            if (typeCheck)
            {
                
            }
            else
            {

            }
        }

    }
}


