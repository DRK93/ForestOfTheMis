using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using _MyScripts.Targeting;

namespace _MyScripts.Enemy.Patrol
{
    public class PatrolGroup : MonoBehaviour
    {
        public List<PatrolJob> patrolMembers;
        public PatrolRoutine patrolRoutine;
        public List<Target> patrolTargetsList;
        public Target currentTarget;
        public bool inCombat;
        public int memberinCombatCount = 0;

        public Action patrolGroupUpdate;
        public Action patrolGroupInCombat;
        public Action patrolGroupOutCombat;
        private void Start()
        {
            patrolGroupUpdate += CheckPatrolGroupStatus;
        }

        private void OnDisable()
        {
            patrolGroupUpdate -= CheckPatrolGroupStatus;
        }

        private void OnDestroy()
        {
            patrolGroupUpdate -= CheckPatrolGroupStatus;
        }

        // logic behind maintence of group of NPC that do patrol stuff
        public void JoinPatrolGroup(PatrolJob patrolMember)
        {
            patrolMembers.Add(patrolMember);
            patrolGroupUpdate?.Invoke();
        }

        public void LeavePatrolGroup(PatrolJob patrolMember)
        {
            patrolMembers.Remove(patrolMember);
            patrolGroupUpdate?.Invoke();
        }
        public void CheckPatrolGroupStatus()
        {
            if (patrolMembers.Count == 0)
            {
                DismantlePatrolGroup();
            }
        }

        public void DismantlePatrolGroup()
        {
            //dismantle this unit;
        }

        public void GetInCombat()
        {
            inCombat = true;
            patrolGroupInCombat?.Invoke();
        }

        public void GetOutCombat()
        {
            inCombat = false;
            patrolGroupOutCombat?.Invoke();
        }

        public void MemberInCombat()
        {
            memberinCombatCount++;
            if (memberinCombatCount == 1)
            {
                GetInCombat();
            }
        }

        public void MemberOutCombat()
        {
            memberinCombatCount--;
            if (memberinCombatCount == 0)
            {
                GetOutCombat();
            }
        }

        public Target GetTargetToPatrol(Target foundedTarget)
        {
            return foundedTarget;
        }

        public Target RemoveTargetFromPatrol(Target removingTarget)
        {
            return removingTarget;
        }
    }
}

