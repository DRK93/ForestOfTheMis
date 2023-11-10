using System.Collections;
using System.Collections.Generic;
using _MyScripts.HUD;
using UnityEngine;

namespace _MyScripts.StateMachines.PlayerStates
{
    public class PlayerTargetingState : PlayerBaseState
{

    private readonly int _targetingBlendTreeHash = Animator.StringToHash("1H&Sh Locomotion");
    private readonly int _forwardSpeedHash = Animator.StringToHash("Forward");
    private readonly int _turnSpeedHash = Animator.StringToHash("Right");
    private readonly int _forwardSpeedHash2 = Animator.StringToHash("ForwardFree");
    private readonly int _turnSpeedHash2 = Animator.StringToHash("RightFree");
    private float _movementDampTime = 0.1f;
    // this will change speed when going backward
    private const float CrossFadeDuration = 0.1f;
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.WeaponChangeAllowed = true;
        StateMachine.InputReader.TargetEvent += OnTargetFound;
        StateMachine.InputReader.DodgeEvent += OnDodge;
        StateMachine.Animator.CrossFadeInFixedTime(_targetingBlendTreeHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if(StateMachine.InputReader.IsAttacking)
        {
            // check for attack nr1 if zero then do nothing
            if (StateMachine.AttackBar.SkillSlot1.attackData != null)
            {
                StateMachine.SwitchState(new PlayerAttackingState2(StateMachine,
                    StateMachine.AttackBar.SkillSlot1.attackData));
                return;
            }
            else
            {
                GameObject.FindWithTag("Notification").GetComponent<Notifications>().ObjectNotification(6);
                return;
            }
        }
        if(StateMachine.InputReader.IsBlocking)
        {
            StateMachine.SwitchState(new PlayerBlockingState(StateMachine, 1, StateMachine.currentBlockingAnimation));
            return; 
        }
        if(StateMachine.Targeter.CurrentTarget == null && StateMachine.Targeter.IsTargetListEmpty())
        {
            StateMachine.SwitchState(new PlayerFreeLookState(StateMachine));
            return;
        }



        Vector3 movement = CalculateMovement(deltaTime);

        Move(movement * StateMachine.TargetMovementSpeed, deltaTime);
        UpdateAnimator(deltaTime);
        FaceTarget();

    }
    public override void Exit()
    {
        StateMachine.InputReader.TargetEvent -= OnTargetFound;
        StateMachine.InputReader.DodgeEvent -= OnDodge;
        //stateMachine.Animator.SetFloat(TurnSpeedHash, 0f);
        //stateMachine.Animator.SetFloat(ForwardSpeedHash, 0f);
        StateMachine.Animator.SetFloat(_turnSpeedHash2, 0f);
        StateMachine.Animator.SetFloat(_forwardSpeedHash2, 0f);
    }

    private void OnTargetFound()
    {
        if(!StateMachine.Targeter.IsTargetListEmpty())
        {
            StateMachine.Targeter.Cancel();
            StateMachine.SwitchState(new PlayerFreeLookState(StateMachine));
        }
        else
        {
            //stateMachine.Targeter.SelectTargetByDistance();
        }

    }
    private void OnDodge()
    {
        if(StateMachine.InputReader.MovementValue == Vector2.zero) {return;}
        StateMachine.SwitchState(new PlayerDodgingState(StateMachine,StateMachine.InputReader.MovementValue));
    }

    private Vector3 CalculateMovement(float deltaTime)
    {
        Vector3 movement = new Vector3();
        float forwardModifer = 1.3f;
        float rightModifer = 0.95f;
        if(StateMachine.InputReader.MovementValue.x < 0)
            rightModifer = 0.7f;
        movement += StateMachine.transform.right * StateMachine.InputReader.MovementValue.x * rightModifer;

        if(StateMachine.InputReader.MovementValue.y < 0f )
            forwardModifer = 0.7f;

        movement += StateMachine.transform.forward * StateMachine.InputReader.MovementValue.y * forwardModifer;

        return movement;
    }

    private void UpdateAnimator(float deltaTime)
    {
        if(StateMachine.InputReader.MovementValue.y == 0)
        {
            // stateMachine.Animator.SetFloat(ForwardSpeedHash, 0f, MovementDampTime, deltaTime);
            StateMachine.Animator.SetFloat(_forwardSpeedHash2, 0f, _movementDampTime, deltaTime);
        }
        else
        {
            // stateMachine.Animator.SetFloat(ForwardSpeedHash, stateMachine.InputReader.MovementValue.y
            // * stateMachine.TargetMovementSpeed, MovementDampTime, deltaTime);
            StateMachine.Animator.SetFloat(_forwardSpeedHash2, StateMachine.InputReader.MovementValue.y
            * StateMachine.TargetMovementSpeed, _movementDampTime, deltaTime);
        }
        if(StateMachine.InputReader.MovementValue.x == 0)
        {
            // stateMachine.Animator.SetFloat(TurnSpeedHash, 0f, MovementDampTime, deltaTime);
            StateMachine.Animator.SetFloat(_turnSpeedHash2, 0f, _movementDampTime, deltaTime);
        }
        else
        {
            // stateMachine.Animator.SetFloat(TurnSpeedHash, -1f * stateMachine.InputReader.MovementValue.x
            // * stateMachine.TargetMovementSpeed, MovementDampTime, deltaTime);
            StateMachine.Animator.SetFloat(_turnSpeedHash2, -1f * StateMachine.InputReader.MovementValue.x
            * StateMachine.TargetMovementSpeed, _movementDampTime, deltaTime);
        }

    }


}
}

