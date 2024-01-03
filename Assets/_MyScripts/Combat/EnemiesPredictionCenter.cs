using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace _MyScripts.Combat
{
    public class EnemiesPredictionCenter : MonoBehaviour
    {
        
        public Action EnemyReaction;

        public void PlayerAttack()
        {
            EnemyReaction?.Invoke();
        }
    }
}

