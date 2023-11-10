using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.Enemy.Patrol
{
    
    public class PatrolPositioning : MonoBehaviour
    {
        public List<PatrolPoint> patrolPoints;
        
        //  maintence for one patrol position which include many patrol points
        //  logic varies from how many npc are in patrol group

        public bool CheckIfPatrolCouldGo(int patrolMembersNumber)
        {
            if (patrolPoints.Count < patrolMembersNumber)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void AssignPatrolPoint(PatrolPoint patrolPoint)
        {
            
        }

        public Vector2 GetPatrolPositionPointXZ(PatrolPoint patrolPoint)
        {
            float patrolPointXPos = patrolPoint.GetPositionX();
            float patrolPointZPos = patrolPoint.GetPositionZ();
            Vector2 positionOnPlane = new Vector2(patrolPointXPos, patrolPointZPos);
            return positionOnPlane;
        }
        public void CreatePatrolPoint()
        {
            // automatic creation of Patrol points around center point
            // is it worth or do some manually ?
        }
    }
}

