using System.Collections;
using System.Collections.Generic;
using _MyScripts.HUD;
using UnityEngine;

namespace _MyScripts.StateMachines.PlayerStates
{
    public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int _forwardSpeedHash = Animator.StringToHash("ForwardFree");
    private int _locomotionNumber;
    private int _currentLocomotion;
    private float _movementFactor;
    private float _movementDampTime = 0.1f;

    private float _currentMoveSpeed;
    // I need to play with this crossFadeDuration to get the proper animator smooth transisions from code levels
    // in course was 0.1f
    private const float CrossFadeDuration = 0.25f;
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }
    public override void Enter()
    {
        StateMachine.WeaponChangeAllowed = true;
        StateMachine.InputReader.TargetEvent += OnTarget;
        StateMachine.InputReader.JumpEvent += OnJump;
        StateMachine.InputReader.DodgeEvent += OnDodge;
        StateMachine.InputReader.Special += UseSpecial;
        StateMachine.InputReader.ToggleWalkRun += OnWalkRunChange;
        ChooseCurrentLocomotion();
        StateMachine.Animator.CrossFadeInFixedTime(StateMachine.currentLocomotion, CrossFadeDuration);
    }
    
    private void ChooseCurrentLocomotion()
    {
        SetAnimatorMoveSpeed();
        if (StateMachine.WeaponChangeAllowed)
        {
            switch (StateMachine.LocomotionNumber)
            {
                case 1:
                    _movementFactor = 0.5f;
                    break;
                case 2:
                    _movementFactor = 0.5f;
                    break;
                case 3:
                    _movementFactor = 0.5f;
                    break;
                default:
                    _movementFactor = 1;
                    break;
            }
        }
        else
        {
            //Show notification that changing weapon is not allowed
            Debug.Log("Change not allowed");
        }

    }
    public override void Tick(float deltaTime)
    {
        
        if(StateMachine.InputReader.IsAttacking)
        {
            if (StateMachine.AttackBar.SkillSlot1.attackData != null)
            {
                StateMachine.SwitchState(new PlayerAttackingState2(StateMachine, StateMachine.AttackBar.SkillSlot1.attackData));
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

        Vector3 movement = CalculateMovement() * (_movementFactor * _currentMoveSpeed / StateMachine.animatorRunSpeed);
        Move(movement * StateMachine.FreeMovementSpeed, deltaTime);

        if(StateMachine.InputReader.MovementValue == Vector2.zero)
        {
            if (Time.timeScale < 0.02f)
            {
                StateMachine.Animator.SetFloat(_forwardSpeedHash, 0f, _movementDampTime, deltaTime);
            }
            else
            {
                //StateMachine.Animator.SetFloat(_forwardSpeedHash, 0f, _movementDampTime, deltaTime * 1/Time.timeScale);
                StateMachine.Animator.SetFloat(_forwardSpeedHash, 0f, _movementDampTime, deltaTime);
            }

            return;
        }

        if (Time.timeScale < 0.02f)
        {
            StateMachine.Animator.SetFloat(_forwardSpeedHash, 0f, _movementDampTime, deltaTime);
        }
        else
        {
            StateMachine.Animator.SetFloat(_forwardSpeedHash, _currentMoveSpeed, _movementDampTime, deltaTime * 1/Time.timeScale);
        }
        
        FaceMovementDirection(movement, deltaTime);
    }
    public override void Exit()
    {
        StateMachine.Animator.SetFloat(_forwardSpeedHash, 0f);
        
        StateMachine.InputReader.JumpEvent -= OnJump;
        StateMachine.InputReader.DodgeEvent -= OnDodge;
        StateMachine.InputReader.Special -= UseSpecial;
        StateMachine.InputReader.TargetEvent -= OnTarget;
        StateMachine.InputReader.ToggleWalkRun -= OnWalkRunChange;
    }

    /*private void OnTarget()
    {
        if (StateMachine.Targeter.IsOnlyOneTarget())
        {
            return;
        }
        else
        {
            StateMachine.Targeter.NextTarget();
        }

        //stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }*/
    
    private Vector3 CalculateMovement()
    {
        Vector3 forward = StateMachine.MainCameraTransform.forward;
        Vector3 right = StateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        return forward * StateMachine.InputReader.MovementValue.y +
        right * StateMachine.InputReader.MovementValue.x;
    }
    private void FaceMovementDirection ( Vector3 movement, float deltaTime)
    {
        StateMachine.transform.rotation = Quaternion.Lerp(StateMachine.transform.rotation,
        Quaternion.LookRotation(movement),
        deltaTime * 1/Time.timeScale * StateMachine.RotationDamp);
    }
    private void OnJump()
    {
        StateMachine.SwitchState(new PlayerJumpingState(StateMachine));
    }
    private void OnDodge()
    {
        //if(stateMachine.InputReader.MovementValue == Vector2.zero) {return;}
        //stateMachine.SwitchState(new PlayerDodgingState(stateMachine,stateMachine.InputReader.MovementValue));
        StateMachine.SwitchState(new PlayerDodgeState2(StateMachine,StateMachine.InputReader.MovementValue));
    }

    private void SetAnimatorMoveSpeed()
    {
        if (StateMachine.isRunning)
            _currentMoveSpeed = StateMachine.animatorRunSpeed;
        else
            _currentMoveSpeed = StateMachine.animatorWalkSpeed;
    }
    private void OnWalkRunChange()
    {
        if (StateMachine.isRunning)
        {
            StateMachine.isRunning = false;
            _currentMoveSpeed = StateMachine.animatorWalkSpeed;
        }
        else
        {
            StateMachine.isRunning = true;
            _currentMoveSpeed = StateMachine.animatorRunSpeed;
        }
    }

    private void UseSpecial()
    {
        switch (StateMachine.InputReader.SpecialNumber)
        {
            case 1:
                // use attack Animation from skill slo from skill bar number1
                if (StateMachine.SkillBar.SkillSlot1.attackData != null)
                {
                    // there will be one new condition with special on cooldwon
                    if (StateMachine.PlayerDamageable.Mana() > StateMachine.SkillBar.SkillSlot1.attackData.manaCost &&
                        StateMachine.PlayerDamageable.Power() > StateMachine.SkillBar.SkillSlot1.attackData.powerCost)
                    {
                        StateMachine.PlayerDamageable.ManaCost(StateMachine.SkillBar.SkillSlot1.attackData.manaCost);
                        StateMachine.PlayerDamageable.PowerCost(StateMachine.SkillBar.SkillSlot1.attackData.powerCost);
                        StateMachine.SwitchState(new PlayerAttackingState2(StateMachine,
                            StateMachine.SkillBar.SkillSlot1.attackData));
                    }
                    else if (StateMachine.PlayerDamageable.Mana() >
                             StateMachine.SkillBar.SkillSlot1.attackData.manaCost)
                    {
                        GameObject.FindWithTag("Notification").GetComponent<Notifications>().ObjectNotification(4);
                    }
                    else
                    {
                        GameObject.FindWithTag("Notification").GetComponent<Notifications>().ObjectNotification(3);
                    }

                    {
                        //   GameObject.FindWithTag("Notification").GetComponent<Notifications>().ObjectNotification(5);
                    }
                }
                else
                {
                    GameObject.FindWithTag("Notification").GetComponent<Notifications>().ObjectNotification(6);
                }
                break;
            case 2:
                if (StateMachine.SkillBar.SkillSlot2.attackData != null)
                {
                    if (StateMachine.PlayerDamageable.Mana() > StateMachine.SkillBar.SkillSlot2.attackData.manaCost &&
                        StateMachine.PlayerDamageable.Power() > StateMachine.SkillBar.SkillSlot2.attackData.powerCost)
                    {
                        StateMachine.PlayerDamageable.ManaCost(StateMachine.SkillBar.SkillSlot2.attackData.manaCost);
                        StateMachine.PlayerDamageable.PowerCost(StateMachine.SkillBar.SkillSlot2.attackData.powerCost);
                        StateMachine.SwitchState(new PlayerAttackingState2(StateMachine, StateMachine.SkillBar.SkillSlot2.attackData));
                    }
                    else if (StateMachine.PlayerDamageable.Mana() > StateMachine.SkillBar.SkillSlot2.attackData.manaCost)
                    {
                        GameObject.FindWithTag("Notification").GetComponent<Notifications>().ObjectNotification(4);
                    }
                    else
                    {
                        GameObject.FindWithTag("Notification").GetComponent<Notifications>().ObjectNotification(3);
                    }
                    {
                        //   GameObject.FindWithTag("Notification").GetComponent<Notifications>().ObjectNotification(5);
                    }
                }
                else
                {
                    GameObject.FindWithTag("Notification").GetComponent<Notifications>().ObjectNotification(6);
                }
                break;
            case 3:
                if (StateMachine.SkillBar.SkillSlot3.attackData != null)
                {

                    if (StateMachine.PlayerDamageable.Mana() > StateMachine.SkillBar.SkillSlot3.attackData.manaCost &&
                        StateMachine.PlayerDamageable.Power() > StateMachine.SkillBar.SkillSlot3.attackData.powerCost)
                    {
                        StateMachine.PlayerDamageable.ManaCost(StateMachine.SkillBar.SkillSlot3.attackData.manaCost);
                        StateMachine.PlayerDamageable.PowerCost(StateMachine.SkillBar.SkillSlot3.attackData.powerCost);
                    
                            StateMachine.SwitchState(new PlayerAttackingState2(StateMachine, StateMachine.SkillBar.SkillSlot3.attackData));
                    }
                    else if (StateMachine.PlayerDamageable.Mana() > StateMachine.SkillBar.SkillSlot3.attackData.manaCost)
                    {
                        GameObject.FindWithTag("Notification").GetComponent<Notifications>().ObjectNotification(4);
                    }
                    else
                    {
                        GameObject.FindWithTag("Notification").GetComponent<Notifications>().ObjectNotification(3);
                    }
                    {
                     //   GameObject.FindWithTag("Notification").GetComponent<Notifications>().ObjectNotification(5);
                    }
                }
                else
                {
                    GameObject.FindWithTag("Notification").GetComponent<Notifications>().ObjectNotification(6);
                }
                break;
            case 4:
                if (StateMachine.SkillBar.SkillSlot4.attackData != null)
                {
                    if (StateMachine.PlayerDamageable.Mana() > StateMachine.SkillBar.SkillSlot4.attackData.manaCost &&
                        StateMachine.PlayerDamageable.Power() > StateMachine.SkillBar.SkillSlot4.attackData.powerCost)
                    {
                        StateMachine.PlayerDamageable.ManaCost(StateMachine.SkillBar.SkillSlot4.attackData.manaCost);
                        StateMachine.PlayerDamageable.PowerCost(StateMachine.SkillBar.SkillSlot4.attackData.powerCost);
                        StateMachine.SwitchState(new PlayerAttackingState2(StateMachine, StateMachine.SkillBar.SkillSlot4.attackData));
                    }
                    else if (StateMachine.PlayerDamageable.Mana() > StateMachine.SkillBar.SkillSlot4.attackData.manaCost)
                    {
                        GameObject.FindWithTag("Notification").GetComponent<Notifications>().ObjectNotification(4);
                    }
                    else
                    {
                        GameObject.FindWithTag("Notification").GetComponent<Notifications>().ObjectNotification(3);
                    }

                    {
                        //   GameObject.FindWithTag("Notification").GetComponent<Notifications>().ObjectNotification(5);
                    }
                }
                else
                {
                    GameObject.FindWithTag("Notification").GetComponent<Notifications>().ObjectNotification(6);
                }
                break;
            case 5:
                if (StateMachine.SkillBar.SkillSlot5.attackData != null)
                {
                    if (StateMachine.PlayerDamageable.Mana() > StateMachine.SkillBar.SkillSlot5.attackData.manaCost &&
                        StateMachine.PlayerDamageable.Power() > StateMachine.SkillBar.SkillSlot5.attackData.powerCost)
                    {
                        StateMachine.PlayerDamageable.ManaCost(StateMachine.SkillBar.SkillSlot5.attackData.manaCost);
                        StateMachine.PlayerDamageable.PowerCost(StateMachine.SkillBar.SkillSlot5.attackData.powerCost);
                        StateMachine.SwitchState(new PlayerAttackingState2(StateMachine, StateMachine.SkillBar.SkillSlot5.attackData));
                    }
                    else if (StateMachine.PlayerDamageable.Mana() > StateMachine.SkillBar.SkillSlot5.attackData.manaCost)
                    {
                        GameObject.FindWithTag("Notification").GetComponent<Notifications>().ObjectNotification(4);
                    }
                    else
                    {
                        GameObject.FindWithTag("Notification").GetComponent<Notifications>().ObjectNotification(3);
                    }

                    {
                        //   GameObject.FindWithTag("Notification").GetComponent<Notifications>().ObjectNotification(5);
                    }
                }
                else
                {
                    GameObject.FindWithTag("Notification").GetComponent<Notifications>().ObjectNotification(6);
                }
                break;
            default:
                break;
        }
    }

}
}

