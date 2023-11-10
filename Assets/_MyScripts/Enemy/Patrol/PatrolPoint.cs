using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.Enemy.Patrol
{
    public class PatrolPoint : MonoBehaviour
    {
        public Transform patrolPointTransform;
        //[SerializeField] public LayerMask layerMask;
        public bool isBusy;

        public bool patrolPositionCenter;

        private void Start()
        {
            patrolPointTransform = this.transform;
        }

        public Transform GetPointTransform()
        {
            return patrolPointTransform;
        }
        public float GetPositionX()
        {
            return patrolPointTransform.position.x;
        }

        public float GetPositionZ()
        {
            return patrolPointTransform.position.z;
        }

        public bool CheckCollisions()
        {
            return false;
        }
        // basic patrol position where NPC can go while patroling
    }
}

