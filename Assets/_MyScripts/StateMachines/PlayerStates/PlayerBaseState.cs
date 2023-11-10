using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.StateMachines.PlayerStates
{
    public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine StateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.StateMachine = stateMachine;
    }
    protected void Move(float deltaTime)
    {
        Move(Vector3.zero + StateMachine.ForceReceiver.Movement, deltaTime);
    }
    protected void Move (Vector3 motion, float deltaTime)
    {
        StateMachine.CharController.Move((motion + StateMachine.ForceReceiver.Movement) * deltaTime / Time.timeScale);
    }

    protected void FaceTarget()
    {
        //Debug.Log("Rotate2");
        //Debug.Log(StateMachine.Targeter.CurrentTarget);
        if(StateMachine.Targeter.CurrentTarget != null)
        {
            //Debug.Log("Rotate");
            Vector3 targetDirection = StateMachine.Targeter.CurrentTarget.transform.position - StateMachine.transform.position ;
            targetDirection.y = 0;

            StateMachine.transform.rotation = Quaternion.LookRotation(targetDirection);
        }
    }

    protected void ReturnToLocomotion()
    {
        if (StateMachine.Targeter.CurrentTarget != null)
        {
            StateMachine.SwitchState( new PlayerFreeLookState(StateMachine));
            //stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
        else
        {
            StateMachine.SwitchState( new PlayerFreeLookState(StateMachine));
        }
    }
    
    protected void TryNextAttack()
    {
        StateMachine.AttackNumberIncrement();
        if (StateMachine.AttackNumber < 10)
        {
            switch ( StateMachine.AttackNumber)
            {
                case 0:
                    StateMachine.SwitchState(new PlayerAttackingState2(StateMachine, StateMachine.AttackBar.SkillSlot1.attackData));
                        break;
                case 1:
                    StateMachine.SwitchState(new PlayerAttackingState2(StateMachine, StateMachine.AttackBar.SkillSlot2.attackData));
                        break;    
                case 2:
                    StateMachine.SwitchState(new PlayerAttackingState2(StateMachine, StateMachine.AttackBar.SkillSlot3.attackData));
                        break; 
                case 3:
                    StateMachine.SwitchState(new PlayerAttackingState2(StateMachine, StateMachine.AttackBar.SkillSlot4.attackData));
                    break;    
                case 4:
                    StateMachine.SwitchState(new PlayerAttackingState2(StateMachine, StateMachine.AttackBar.SkillSlot5.attackData));
                    break;   
                default:
                    break;
            }
        } 
        else
        {
            return;
        }
    }
    protected void OnTarget()
    {
        //Debug.Log("On target");
        if (StateMachine.Targeter.IsOnlyOneTarget())
        {
            return;
        }
        else
        {
            StateMachine.Targeter.NextTarget();
        }
        //stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }
}
}

