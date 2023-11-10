using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _MyScripts.Combat;

namespace _MyScripts.StateMachines.EnemyStates
{
    public abstract class EnemyBaseState : State
    {

        protected EnemyStateMachine StateMachine;

        public EnemyBaseState(EnemyStateMachine stateMachine)
        {
            this.StateMachine = stateMachine;
        }

        protected void Move(float deltaTime)
        {
            Move(Vector3.zero, deltaTime);
        }
        protected void Move (Vector3 motion, float deltaTime)
        {
            StateMachine.Controller.Move((motion + StateMachine.ForceReceiver.Movement) * deltaTime);
        }
        protected void Move (Vector3 motion, float xModifier, float zModifier, float deltaTime)
        {
            motion.x = motion.x * xModifier;
            motion.z = motion.z * zModifier;
            StateMachine.Controller.Move((motion + StateMachine.ForceReceiver.Movement) * deltaTime);
        }
        protected void FaceTarget()
        {
            if(StateMachine.Target != null) 
            {
                Vector3 targetDirection = StateMachine.Target.transform.position - StateMachine.transform.position ;
                targetDirection.y = 0;

                StateMachine.transform.rotation = Quaternion.LookRotation(targetDirection);
            }
        }
        protected bool InDetectionRange()
        {
            // temporal
            if(StateMachine.Target.GetComponent<Damageable>().IsDead) {return false;}
            float toTargetSqr = (StateMachine.Target.transform.position - StateMachine.transform.position).sqrMagnitude;
            return toTargetSqr <= StateMachine.DetectionRange * StateMachine.DetectionRange;
        }

        protected void SetLocomotionAnimation()
        {
            switch (StateMachine.EnemyStats.enemyLocomotion)
            {
                case EnemyStatsScriptableObject.EnemyLocomotion.Shield:
                    StateMachine.locomotionNumber = 1;
                    StateMachine.locomotionName = "1H&Sh Locomotion";
                    break;
                case EnemyStatsScriptableObject.EnemyLocomotion.TwoHanded:
                    StateMachine.locomotionNumber = 2;
                    StateMachine.locomotionName = "2H Locomotion";
                    break;
                case EnemyStatsScriptableObject.EnemyLocomotion.Spear:
                    StateMachine.locomotionNumber = 3;
                    StateMachine.locomotionName = "Spear Locomotion";
                    break;
                default:
                    StateMachine.locomotionNumber = 10;
                    StateMachine.locomotionName = "1H&Sh Locomotion";
                    break;
            }
        }
        
    }
}

