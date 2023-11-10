using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _MyScripts.Enemy.Patrol;

namespace _MyScripts.Targeting
{
    public class EnemyTargeter : MonoBehaviour
    {
        [SerializeField] private PatrolJob patrolJob;

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<Target>(out Target target))
            {
                patrolJob.SetTarget(target);
                target.OnDestroyed += RemoveTarget;
            }
        }
    
        private void OnTriggerExit(Collider other)
        {
            if(other.TryGetComponent<Target>(out Target target)) 
            {
                RemoveTarget(target);
            }
        }
        private void RemoveTarget(Target target)
        {
            patrolJob.RemoveTarget(target);
            target.OnDestroyed -= RemoveTarget;
        }
    }
}

