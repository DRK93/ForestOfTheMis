using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.StateMachines.PlayerStates
{
    public class PlayerDodgingState : PlayerBaseState
{
    private readonly int _dodgeBlendTreeHash = Animator.StringToHash("DodgeBlendTree");
    private readonly int _dodgeForwardHash = Animator.StringToHash("DodgeForward");
    private readonly int _dodgeRightHash = Animator.StringToHash("DodgeRight");
    private string _animationTagName = "Dodge";
    private float _remainingDodgeTime;
    private bool _retreat;
    private Vector3 _dodgingDirectionInput;
    private const float CrossFadeDuration = 0.1f;
    public PlayerDodgingState(PlayerStateMachine stateMachine, Vector3 dodgingDirectionInput) : base(stateMachine)
    {
        this._dodgingDirectionInput = dodgingDirectionInput;
    }

    public override void Enter()
    {
        StateMachine.WeaponChangeAllowed = false;
        if(_dodgingDirectionInput.y > 0.3f && (_dodgingDirectionInput.x > 0.3f || _dodgingDirectionInput.x < 0.3f))
        {
            StateMachine.Animator.SetFloat(_dodgeRightHash, 0f);
            StateMachine.Animator.SetFloat(_dodgeForwardHash, 1f);
        }
        else if( Mathf.Abs(_dodgingDirectionInput.y) < 0.3f && Mathf.Abs(_dodgingDirectionInput.x) < 0.3f)
        {
            StateMachine.Animator.SetFloat(_dodgeForwardHash, -1f);
            _retreat = true;
        }
        else
        {
            StateMachine.Animator.SetFloat(_dodgeRightHash, _dodgingDirectionInput.x);
            StateMachine.Animator.SetFloat(_dodgeForwardHash, _dodgingDirectionInput.y);
        }
        StateMachine.Animator.CrossFadeInFixedTime(_dodgeBlendTreeHash, CrossFadeDuration);
        StateMachine.PlayerDamageable.SetInvulnerable(true);
    }
    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3();
        float normalizedTime = GetNormalizedTime(StateMachine.Animator, _animationTagName);

        //if(normalizedTime < 0.4f)
            //FaceTarget();
        if(normalizedTime < 0.78f)      
        {
            if(_retreat)
            {
                movement += StateMachine.transform.forward  * StateMachine.DodgeDistance / StateMachine.DodgeDuration * -0.5f;
            }
            else
            {
                movement += StateMachine.transform.right * _dodgingDirectionInput.x * StateMachine.DodgeDistance / StateMachine.DodgeDuration;
                movement += StateMachine.transform.forward * _dodgingDirectionInput.y * StateMachine.DodgeDistance / StateMachine.DodgeDuration;
            }
            Move(movement, deltaTime);
        }

        if(normalizedTime >= 0.86f)
        {
            StateMachine.PlayerDamageable.RemoveInvulnerable();
            if(StateMachine.Targeter.CurrentTarget != null)
                StateMachine.SwitchState(new PlayerFreeLookState(StateMachine));
                //stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            else
            {
                StateMachine.SwitchState(new PlayerFreeLookState(StateMachine));
            }
        }
    }
    public override void Exit()
    {
        StateMachine.PlayerDamageable.RemoveInvulnerable();
    }

    private void DirectionCalc()
    {

    }
}
}

