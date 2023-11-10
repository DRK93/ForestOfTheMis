using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.StateMachines.PlayerStates
{
    public class PlayerFallingState : PlayerBaseState
    {
        private Vector3 _momentum;
        private readonly int _fallHash = Animator.StringToHash("Fall");
        private const float CrossFadeDuration = 0.1f;
        public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            StateMachine.WeaponChangeAllowed = false;
            _momentum = StateMachine.CharController.velocity;
            _momentum.y = 0f;
            StateMachine.Animator.CrossFadeInFixedTime(_fallHash, CrossFadeDuration);
        }
        public override void Tick(float deltaTime)
        {
            Move(_momentum, deltaTime * Time.timeScale);
            if(StateMachine.CharController.isGrounded)
            {
                ReturnToLocomotion();
            }
            // in case of targeting state jumping
            FaceTarget();
        }
        public override void Exit()
        {

        }
    }
}

