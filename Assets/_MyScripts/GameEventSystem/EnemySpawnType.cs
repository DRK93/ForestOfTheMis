using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.GameEventSystem
{
    public class EnemySpawnType : MonoBehaviour
    {
        public enum EnemyType
        {
            WeakBandit,
            StrongBandit,
            Animal,
            Monster,
            StrongMonster,
            HumanBoss,
            AnimalBoss,
            MonsterBoss
        }
    }
}

