using System;
using System.Collections;
using System.Collections.Generic;
using Highlighters;
using UnityEngine;

namespace _MyScripts.Targeting
{
    public class EnemyHighlight : MonoBehaviour
    {
        [field: SerializeField] private Highlighter mainHighlither;
        [field: SerializeField] private Highlighter secondHighlither;
        public Targeter playerTargeter;
    
        public void AddedToTargetList(Targeter targeter)
        {
            playerTargeter = targeter;
            playerTargeter.MainTarget += MainEnemy;
            playerTargeter.NoTarget += NoEnemy;
        }
        private void MainEnemy()
        {
            if (playerTargeter.CurrentTarget.transform.gameObject == this.transform.gameObject)
            {
                secondHighlither.enabled = false;
                mainHighlither.enabled = true;
            }
            else
            {
                JustEnemy();
            }

        }

        private void JustEnemy()
        {
            secondHighlither.enabled = true;
            mainHighlither.enabled = false;
        }

        private void NoEnemy()
        {
            mainHighlither.enabled = false;
            secondHighlither.enabled = false;
        }
    
        public void RemoveFromTargetList()
        {
            playerTargeter.MainTarget -= MainEnemy;
            playerTargeter.NoTarget -= NoEnemy;
            NoEnemy();
            playerTargeter = null;
        }
    }
}

