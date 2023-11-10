using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.StateMachines.PlayerStates
{
    public class PlayerJumpingState : PlayerBaseState
    {
        private readonly int _jumpHash = Animator.StringToHash("Jump");
        private Vector3 _momentum;
        private const float CrossFadeDuration = 0.1f;
        public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            StateMachine.WeaponChangeAllowed = false;
            StateMachine.ForceReceiver.Jump(StateMachine.JumpForce);
            _momentum = StateMachine.CharController.velocity;
            //momentum.y = 0f;
            StateMachine.Animator.CrossFadeInFixedTime(_jumpHash, CrossFadeDuration);
        }
        public override void Tick(float deltaTime)
        {
            Move(_momentum, deltaTime * Time.timeScale);
            if(StateMachine.CharController.velocity.y <= 0f)
            {
                StateMachine.SwitchState(new PlayerFallingState(StateMachine));
                return;
            }

            // if jump in targeting section
            //FaceTarget();
        }
        public override void Exit()
        {

        }


    }
}

