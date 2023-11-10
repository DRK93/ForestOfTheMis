using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using _MyScripts.Combat;

namespace _MyScripts.StateMachines.EnemyStates
{
    public class EnemyChasingState : EnemyBaseState
{
    private readonly int _forwardSpeedHash = Animator.StringToHash("ForwardFree");
    //private readonly int _enemyBlendTreeHash = Animator.StringToHash("Locomotion");
    private int _locomotionHash;
    private const float CrossFadeDuration = 0.1f;
    private const float DampTime = 0.1f;
    private bool _traffic;
    private float _trafficTimer = 0f;
    private Vector3 _previousPosition;
    private float _movementFactor;
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        //_locomotionHash = Animator.StringToHash(StateMachine.EnemyStats.locomotionName);
    }

    public override void Enter()
    {
        SetLocomotionAnimation();
        _locomotionHash = Animator.StringToHash(StateMachine.locomotionName);
        //_locomotionHash = Animator.StringToHash(StateMachine.EnemyStats.locomotionName);
        StateMachine.isRunning = true;
        StateMachine.Animator.CrossFadeInFixedTime(_locomotionHash, CrossFadeDuration);
        _previousPosition = StateMachine.transform.position;
    }
    public override void Tick(float deltaTime)
    {
        if(StateMachine.Target != null)
            _trafficTimer += deltaTime;

        if (!InDetectionRange())
        {
            StateMachine.SwitchState(new EnemyIdleState(StateMachine));
            return;
        }
        if (IsInAttackRange())
        {
            //Debug.Log("Attack");
            StateMachine.SwitchState(new EnemyAttackingState2(StateMachine, StateMachine.EnemyStats.AttackList[StateMachine.AttackNumber]));
            return;
        }
        
        if( _trafficTimer > 0.5f)
        {
            _trafficTimer = 0f;
            //CheckTraffic();
        }
        if(_traffic)
        {
            //Flanking();
        }
        else
        {
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(StateMachine.transform.position, StateMachine.Target.transform.position, StateMachine.Agent.areaMask, path);
            if(StateMachine.Agent.enabled)
                StateMachine.Agent.SetPath(path);
            else
            {
                StateMachine.Agent.enabled = true;
                StateMachine.Agent.SetPath(path);
            }
            Move(StateMachine.Agent.desiredVelocity.normalized * StateMachine.MovementSpeed, deltaTime);
            StateMachine.Agent.velocity = StateMachine.Controller.velocity;
            // MoveToPlayer(deltaTime);
        }
        FaceTarget();
        StateMachine.Animator.SetFloat(_forwardSpeedHash, StateMachine.MovementSpeed, DampTime, deltaTime);
    }
    public override void Exit()
    {
        if(StateMachine.Agent.enabled)
        {
            StateMachine.Agent.ResetPath();
            StateMachine.Agent.velocity = Vector3.zero;
        }
    }

    private void MoveToPlayer(float deltaTime)
    {
        if(StateMachine.Agent.isOnNavMesh)
        {
            StateMachine.Agent.destination = StateMachine.Target.transform.position;
            
            Move(StateMachine.Agent.desiredVelocity.normalized * StateMachine.MovementSpeed, deltaTime);
            StateMachine.Agent.velocity = StateMachine.Controller.velocity;
        }
    }
    private void CheckTraffic()
    {
        
        // condition if there are more than 2 enemmyStateMachines around this one, there could be traffic issues
        // if (false)
        // {
        //     return;
        // }
        // else
        // {
            float distanceSquare = (_previousPosition - StateMachine.transform.position).sqrMagnitude;
            _previousPosition = StateMachine.transform.position;
            if (distanceSquare < 0.8f)
            {
//                Debug.Log("Traffic");
                _traffic = true;
            }
            else
            {
                _traffic = false;
            }
            // }
    }
    private void Flanking()
    {
        _traffic = false;
        StateMachine.SwitchState(new EnemyFlankingState(StateMachine));
    }
    private bool IsInAttackRange()
    {
        // temporal
        if(StateMachine.Target.GetComponent<Damageable>().IsDead) {return false;}
        float distanceSquare = (StateMachine.Target.transform.position - StateMachine.transform.position).sqrMagnitude;
        return distanceSquare <= StateMachine.AttackRange * StateMachine.AttackRange;
    }
    
    private void CalcMovementFactor()
    {
        switch (StateMachine.locomotionNumber)
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
}
}

