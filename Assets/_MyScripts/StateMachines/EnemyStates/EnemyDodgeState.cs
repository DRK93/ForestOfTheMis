using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.StateMachines.EnemyStates
{
    public class EnemyDodgeState : EnemyBaseState
    {
        private readonly int _dodgeBlendTreeHash = Animator.StringToHash("DodgeBlendTree");
        private readonly int _dodgeForwardHash = Animator.StringToHash("DodgeForward");
        private readonly int _dodgeRightHash = Animator.StringToHash("DodgeRight");
        private float _remainingDodgeTime;
        private Vector3 _dodgingDirectionInput;
        private const float CrossFadeDuration = 0.1f;
        public EnemyDodgeState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
        
        }
        public override void Tick(float deltaTime)
        {
            Vector3 movement = new Vector3();
            //movement += stateMachine.transform.right * dodgingDirectionInput.x * stateMachine.DodgeDistance / stateMachine.DodgeDuration;
            //movement += stateMachine.transform.forward * dodgingDirectionInput.y * stateMachine.DodgeDistance / stateMachine.DodgeDuration;
            Move(movement, deltaTime);

            FaceTarget();

            //remainingDodgeTime -= deltaTime * 1/Time.timeScale;
            _remainingDodgeTime -= deltaTime;
            if(_remainingDodgeTime <= 0f)
            {
                //stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
                StateMachine.SwitchState(new EnemyChasingState(StateMachine));
            }
        }
        public override void Exit()
        {
        
        }



    
    }
}

