using System;
using System.Collections;
using System.Collections.Generic;
using _MyScripts.Combat;
using _MyScripts.Targeting;
using UnityEngine;

namespace _MyScripts.Enemy.Patrol
{
    public class PatrolJob : MonoBehaviour
    {
        private PatrolGroup _patrolGroup;

        private Damageable _damageable;
        public Target CurrentTarget {get; private set;}
        [SerializeField] private List<Target> targets = new List<Target>();
        [SerializeField] private List<Target> groupTargets = new List<Target>();
        [SerializeField] private HashSet<Target> groupTargets2;
        private bool _inPatrolGroup;

        private bool _inCombat;
        // Start is called before the first frame update
        void Start()
        {
        }
        
        private void OnDisable()
        {
            LeavePatrolGroup();
        }

        private void OnDestroy()
        {
            LeavePatrolGroup();
        }

        private void InformPatrolAboutCombat()
        {
            if (_inPatrolGroup)
            {
                _patrolGroup.MemberInCombat();
            }
        }

        private void InformPatrolAbutOutCombat()
        {
            if (_inPatrolGroup)
            {
                _patrolGroup.MemberOutCombat();
            }
        }

        public void JoinPatrolGroup()
        {
            _patrolGroup.JoinPatrolGroup(this);
            _patrolGroup.patrolGroupInCombat += PatrolInCombat;
            _patrolGroup.patrolGroupOutCombat += PatrolOutCombat;
        }

        public void LeavePatrolGroup()
        {
            _patrolGroup.LeavePatrolGroup(this);
            _patrolGroup.patrolGroupInCombat -= PatrolInCombat;
            _patrolGroup.patrolGroupOutCombat -= PatrolOutCombat;
        }
        public void PatrolInCombat()
        {
            _inCombat = true;
        }

        public void PatrolOutCombat()
        {
            _inCombat = false;
        }

   

        public void SetTarget(Target newTarget)
        {
            targets.Add(newTarget);
            groupTargets2.Add(newTarget);
            if (targets.Count == 1)
            {
                _damageable.InBattle?.Invoke();
                _inCombat = true;
                InformPatrolAboutCombat();
            }
        }

        public void CheckTargets()
        {
            
        }

        public void RemoveTarget(Target target)
        {
            targets.Remove(target);
            if (targets.Count == 0)
            {
                if (!_inPatrolGroup)
                {
                    _damageable.OutOfBattle?.Invoke();
                    _inCombat = false;
                }
                
                InformPatrolAbutOutCombat();
            }
        }
        public void GivePatrolTarget( Target target)
        {
            
        }
        public void InComabt()
        {
            _damageable.InBattle?.Invoke();
            InformPatrolAboutCombat();
        }

        public void OutComabt()
        {
            _damageable.OutOfBattle?.Invoke();
            InformPatrolAbutOutCombat();
        }
        
        public void SelectTargetByDistance()
        {
            if (targets.Count == 0) {return;}
    
            Target temporaryTarget = null;
            float closestTargetDistance = Mathf.Infinity;
            foreach(Target target in targets)
            {
                Vector3 targetPosition = target.transform.position;
                float distance = Vector3.Distance(targetPosition, transform.position);
    
                if (CurrentTarget == target)
                {
    
                }
                else
                {
                    if (distance < closestTargetDistance)
                    {
                        temporaryTarget = target;
                    }
                }
            }
            CurrentTarget = temporaryTarget;
        }
    }
}

