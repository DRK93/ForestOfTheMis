using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.StateMachines.PlayerStates
{
    public class PlayerDodgeState2 : PlayerBaseState
{
    private readonly int _dodgeBlendTreeHash = Animator.StringToHash("DodgeBlendTree");
    private readonly int _dodgeForwardHash = Animator.StringToHash("DodgeForward");
    private readonly int _dodgeRightHash = Animator.StringToHash("DodgeRight");
    private string _animationTagName = "Dodge";
    private float _remainingDodgeTime;
    private bool _retreat;
    private Vector3 _dodgingDirectionInput;
    private const float CrossFadeDuration = 0.1f;
    public PlayerDodgeState2(PlayerStateMachine stateMachine,  Vector3 dodgingDirectionInput) : base(stateMachine)
    {
        this._dodgingDirectionInput = dodgingDirectionInput;
    }

    public override void Enter()
    {
        StateMachine.WeaponChangeAllowed = false;
        //if(dodgingDirectionInput.y > 0.3f && (dodgingDirectionInput.x > 0.3f || dodgingDirectionInput.x < -0.3f))
        if(_dodgingDirectionInput.y > 0.3f)
        {
            StateMachine.Animator.SetFloat(_dodgeRightHash, 0f);
            StateMachine.Animator.SetFloat(_dodgeForwardHash, 1f);
        }
        else if( Mathf.Abs(_dodgingDirectionInput.y) < 0.05f && Mathf.Abs(_dodgingDirectionInput.x) < 0.05f)
        {
            StateMachine.Animator.SetFloat(_dodgeForwardHash, -1f);
            _retreat = true;
        }
        else if ((_dodgingDirectionInput.x > 0.05f || _dodgingDirectionInput.x < -0.05f) && _dodgingDirectionInput.y > -0.1)
        // else if ((dodgingDirectionInput.x > 0.3f || dodgingDirectionInput.x < -0.3f))
        {
            // stateMachine.Animator.SetFloat(DodgeRightHash, dodgingDirectionInput.x);
            // stateMachine.Animator.SetFloat(DodgeForwardHash, dodgingDirectionInput.y);
            StateMachine.Animator.SetFloat(_dodgeRightHash, 0f);
            StateMachine.Animator.SetFloat(_dodgeForwardHash, 1f);
        }
        else
        {
            StateMachine.Animator.SetFloat(_dodgeRightHash, 0f);
            StateMachine.Animator.SetFloat(_dodgeForwardHash, 1f);
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

            // Debug.Log("Right " + dodgingDirectionInput.x);
            // Debug.Log("Forward " + dodgingDirectionInput.y);
            if(_dodgingDirectionInput.y < 0 )
            {
                if(_dodgingDirectionInput.y == 0f)
                    movement += StateMachine.transform.forward * 1.1f *  Mathf.Abs(_dodgingDirectionInput.x) * StateMachine.DodgeDistance / StateMachine.DodgeDuration;
                else
                    movement += StateMachine.transform.forward * 0.8f * Mathf.Abs(_dodgingDirectionInput.x) * StateMachine.DodgeDistance / StateMachine.DodgeDuration;
                
                if (_dodgingDirectionInput.x == 0f)
                    movement+= StateMachine.transform.forward * -1.1f * _dodgingDirectionInput.y * StateMachine.DodgeDistance / StateMachine.DodgeDuration;
                else
                    movement+= StateMachine.transform.forward * -0.8f * _dodgingDirectionInput.y * StateMachine.DodgeDistance / StateMachine.DodgeDuration;
            }
            else if(_retreat)
            {
                movement += StateMachine.transform.forward * 1.2f * -0.9f * StateMachine.DodgeDistance / StateMachine.DodgeDuration;
            }
            else
            {
                if(_dodgingDirectionInput.y == 0f)
                    movement += StateMachine.transform.forward * 1.1f * Mathf.Abs(_dodgingDirectionInput.x) * StateMachine.DodgeDistance / StateMachine.DodgeDuration;
                else
                    movement += StateMachine.transform.forward * 0.8f * Mathf.Abs(_dodgingDirectionInput.x) * StateMachine.DodgeDistance / StateMachine.DodgeDuration;
                
                if (_dodgingDirectionInput.x == 0f)
                    movement += StateMachine.transform.forward * 1.1f * _dodgingDirectionInput.y * StateMachine.DodgeDistance / StateMachine.DodgeDuration;
                else
                    movement+= StateMachine.transform.forward * 0.8f * _dodgingDirectionInput.y * StateMachine.DodgeDistance / StateMachine.DodgeDuration;
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
        StateMachine.Animator.SetFloat(_dodgeRightHash, 0f);
        StateMachine.Animator.SetFloat(_dodgeForwardHash, 0f);
    }


}
}

