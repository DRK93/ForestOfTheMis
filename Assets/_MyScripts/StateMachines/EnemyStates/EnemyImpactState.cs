using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.StateMachines.EnemyStates
{
    public class EnemyImpactState : EnemyBaseState
    {
        private readonly int _impactHash = Animator.StringToHash("Hurt");
        private const float CrossFadeDuration = 0.1f;
        private float _duration = 1f;
        public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(_impactHash, CrossFadeDuration);
        }
        public override void Tick(float deltaTime)
        {
            Move(deltaTime);
            _duration -= deltaTime;
            if (_duration <= 0f)
            {
                StateMachine.SwitchState(new EnemyIdleState(StateMachine));
                return;
            }
        }
        public override void Exit()
        {

        }
    }
}

