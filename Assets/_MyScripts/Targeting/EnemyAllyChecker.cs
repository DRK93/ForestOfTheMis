using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace _MyScripts.Targeting
{
    public class EnemyAllyChecker : MonoBehaviour
    {
        public event Action EnemyInRange;
        public event Action EnemyOutOfRange;
        private HashSet<GameObject> _enemyAlliesInRange = new HashSet<GameObject>();

        private void OnTriggerEnter(Collider other)
        {
            _enemyAlliesInRange.Add(other.transform.parent.root.gameObject);
        }
        private void OnTriggerExit(Collider other)
        {
            _enemyAlliesInRange.Remove(other.transform.parent.root.gameObject);
        }

        public void FollowEnemy()
        {
            EnemyInRange?.Invoke();
        }

        public void StopFollowingEnemy()
        {
            EnemyOutOfRange?.Invoke();
        }

    }
}

