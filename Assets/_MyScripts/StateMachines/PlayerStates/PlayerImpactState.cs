using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.StateMachines.PlayerStates
{
    public class PlayerImpactState : PlayerBaseState
    {
        private string _animationTagName = "Hurt";
        private readonly int _impactHash = Animator.StringToHash("Hurt");
        private const float CrossFadeDuration = 0.1f;
        private float _duration = 1f;
        private bool _dodgeOn;

        public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            StateMachine.WeaponChangeAllowed = false;
            StateMachine.Animator.CrossFadeInFixedTime(_impactHash, CrossFadeDuration);
            StateMachine.InputReader.DodgeEvent += DoDodge;
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);
            float normalizedTime = GetNormalizedTime(StateMachine.Animator, _animationTagName);
            _duration -= deltaTime;
            if (_duration <= 0f)
            {
                ReturnToLocomotion();
            }

            if (normalizedTime > 0.60f && _dodgeOn)
            {
                StateMachine.SwitchState(new PlayerDodgingState(StateMachine, StateMachine.InputReader.MovementValue));
            }
        }

        public override void Exit()
        {
            StateMachine.InputReader.DodgeEvent -= DoDodge;
        }

        private void DoDodge()
        {
            if (StateMachine.InputReader.MovementValue == Vector2.zero)
            {
                return;
            }

            _dodgeOn = true;
        }
    }
}

