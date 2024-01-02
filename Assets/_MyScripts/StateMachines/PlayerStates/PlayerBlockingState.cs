using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.StateMachines.PlayerStates
{
        public class PlayerBlockingState : PlayerBaseState
    {
        //private readonly int BlockHash = Animator.StringToHash("1H&Sh Block");
        private string _animationTagName = "Block";
        private float _crossFadeDuration = 0.1f;
        // crossfadeduration for fast counter should be 0.2f otherwise it looks off place (to fast block position)
        private int _blockingWeaponNumber;
        private bool _blocking;
        private bool _firstCheck;
        private bool _blockedHitAnim;
        private float _counterTimer;
        private BlockingAnimScriptableObject _blockingSo;
        public PlayerBlockingState(PlayerStateMachine stateMachine, int number, BlockingAnimScriptableObject blockingAnim) : base(stateMachine)
        {
            _blockingWeaponNumber = blockingAnim.BlockingNumber;
            // weapon which will blocking attacks:
            // 1 - means shield
            // 3 - means two handed
            // 5 - means spear

            _blockingSo = blockingAnim;
        }
            public PlayerBlockingState(PlayerStateMachine stateMachine, int number, float crossTime, BlockingAnimScriptableObject blockingAnim) : base(stateMachine)
        {
            _blockingWeaponNumber = blockingAnim.BlockingNumber;
            // weapon which will blocking attacks:
            // 1 - means shield
            // 3 - means two handed
            // 5 - means spear

            _blockingSo = blockingAnim;
            // safety check, if there will not be assigned any value to crossFadeTime
            if(crossTime != 0f)
                _crossFadeDuration = crossTime;
        }

        public override void Enter()
        {
            StateMachine.WeaponGripManager.ChangeGrip(_blockingSo.GripNumber);
            StateMachine.WeaponChangeAllowed = false;
            StateMachine.Animator.CrossFadeInFixedTime(_blockingSo.AnimationName, _crossFadeDuration);
            StateMachine.PlayerDamageable.SuccesfulBlock += BlockedAnim;
            StateMachine.InputReader.TargetEvent += OnTarget;
        }
        public override void Tick(float deltaTime)
        {
            Move(deltaTime);
            _counterTimer += deltaTime;
            float normalizedTime = GetNormalizedTime(StateMachine.Animator, _animationTagName);
            if (_blockedHitAnim)
            {   
                if(_firstCheck)
                {
                    _firstCheck = false;
                    normalizedTime = 0f;
                }
                if (normalizedTime >= 0.80f)
                {
                    BackToBlockPosition();
                }
            }
            else
            {
                if( normalizedTime >= _blockingSo.StartBlockingTime && _blocking == false )
                {
                    StartBlocking();
                }
                if (normalizedTime < _blockingSo.RotateTowardsEnemyTimer)
                {
                    FaceTarget();
                }
                
                if(!StateMachine.InputReader.IsBlocking)
                {
                    if(StateMachine.Targeter.CurrentTarget == null)
                    {
                        StateMachine.SwitchState(new PlayerFreeLookState(StateMachine));
                        return;
                    } 
                    else
                    {
                        StateMachine.SwitchState(new PlayerFreeLookState(StateMachine));
                        //stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
                        return;
                    }
                }
                if (_counterTimer > _blockingSo.StartCounterWindow && StateMachine.InputReader.IsAttacking)
                {
                    StateMachine.SwitchState(new PlayerAttackingState2(StateMachine, StateMachine.CurrentCounterAttack));
                    return;
                }
            }

        }
        public override void Exit()
        {
            StateMachine.WeaponGripManager.ReturnToBaseGrip(_blockingSo.GripNumber / 10);
            StateMachine.InputReader.TargetEvent -= OnTarget;
            StateMachine.PlayerDamageable.SuccesfulBlock -= BlockedAnim;
            StateMachine.Animator.speed = 1f;
            EndBlocking();
        }
        private void BlockedAnim()
        {
            //stateMachine.SwitchState(new PlayerBlockingState(stateMachine, 1, stateMachine.BlockingAnimList[1]));
            StateMachine.PlayerDamageable.SuccesfulBlock -= BlockedAnim;
            _blockedHitAnim = true;
            _firstCheck = true;
            
            StateMachine.Animator.CrossFadeInFixedTime(StateMachine.currentBlockedHit.AnimationName, _crossFadeDuration);
        }
        private void BackToBlockPosition()
        {
            _blockedHitAnim = false;
            StateMachine.PlayerDamageable.SuccesfulBlock += BlockedAnim;
            StateMachine.Animator.CrossFadeInFixedTime(_blockingSo.AnimationName, _crossFadeDuration);
        }
        private void StartBlocking()
        {
            _blocking = true;
            StateMachine.PlayerDamageable.SetBlocking();
        }
        public void EndBlocking()
        {
            _blocking = false;
            StateMachine.PlayerDamageable.SuccesfulBlock -= BlockedAnim;
            StateMachine.PlayerDamageable.RemoveBlocking();

        }
    }
}

