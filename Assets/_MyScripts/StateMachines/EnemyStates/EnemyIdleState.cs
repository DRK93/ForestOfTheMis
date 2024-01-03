using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.StateMachines.EnemyStates
{
    public class EnemyIdleState : EnemyBaseState
    {
        private int _locomotionHash;
        private readonly int _forwardSpeedHash = Animator.StringToHash("ForwardFree");
        private const float CrossFadeDuration = 0.1f;
        private const float DampTime = 0.1f;
        public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine){}

        public override void Enter()
        {
            SetLocomotionAnimation();
            _locomotionHash = Animator.StringToHash(StateMachine.locomotionName);
            StateMachine.Animator.CrossFadeInFixedTime(_locomotionHash, CrossFadeDuration);
        
        }
        public override void Tick(float deltaTime)
        {
            Move(deltaTime);
            if (InDetectionRange(StateMachine.DetectionRange))
            {
                //Debug.Log("In Range");
                StateMachine.SwitchState(new EnemyChasingState(StateMachine));
                return;
            }
            FaceTarget();
            StateMachine.Animator.SetFloat(_forwardSpeedHash, 0f, DampTime, deltaTime);
        }
        public override void Exit()
        {
        
        }

    }
}

