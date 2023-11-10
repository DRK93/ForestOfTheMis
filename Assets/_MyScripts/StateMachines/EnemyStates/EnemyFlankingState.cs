using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace _MyScripts.StateMachines.EnemyStates
{
    public class EnemyFlankingState : EnemyBaseState
{
    // state in which enemy character move to side to flank player
    private readonly int _forwardSpeedHash = Animator.StringToHash("ForwardFree");
    private readonly int _rightSpeedHash = Animator.StringToHash("RightFree");
    private readonly int _enemyBlendTreeHash = Animator.StringToHash("1H&Sh Locomotion");
    private const float CrossFadeDuration = 0.1f;
    private const float DampTime = 0.1f;
    private LayerMask _enemyAlliesLayer = LayerMask.GetMask("Enemy");
    private float _flankingTimer;
    private float _radiusToTarget;
    private float _flankingForwardModifer;
    private float _flankingRightModifer;
    private Vector3 _possibleFlankingPoint;
    private Vector3 _flankingPoint;
    private bool _traffic;
    public EnemyFlankingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Flanking");
        StateMachine.Animator.CrossFadeInFixedTime(_enemyBlendTreeHash, CrossFadeDuration);
        _flankingTimer = 0f;
        CalculateFlankkingPath();
    }
    public override void Tick(float deltaTime)
    {
        if(_flankingTimer > 1.0f)
        {
            CheckFlanking();
        }
        CheckDistanceToFlankingPoint();

        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(StateMachine.transform.position, _flankingPoint, StateMachine.Agent.areaMask, path);
        StateMachine.Agent.SetPath(path);
        StateMachine.Agent.velocity = StateMachine.Controller.velocity;

        Move(StateMachine.Agent.desiredVelocity.normalized * StateMachine.MovementSpeed, 0.6f, 0.7f, deltaTime);
        FaceTarget();

        StateMachine.Animator.SetFloat(_forwardSpeedHash, 0f, DampTime, deltaTime);
        if (StateMachine.Agent.desiredVelocity.x >0f)
            StateMachine.Animator.SetFloat(_rightSpeedHash, 2.5f, DampTime, deltaTime);
        else
            StateMachine.Animator.SetFloat(_rightSpeedHash, -2.5f, DampTime, deltaTime);

        _flankingTimer += deltaTime;
    }
    public override void Exit()
    {
        StateMachine.Animator.SetFloat(_forwardSpeedHash, 0f);
        StateMachine.Animator.SetFloat(_rightSpeedHash, 0);
        Debug.Log("Flanking Exit");
    }
    private void CalculateFlankkingPath()
    {
        _radiusToTarget = Mathf.Sqrt(Mathf.Pow(StateMachine.transform.position.x - StateMachine.Target.transform.position.x , 2) 
        + Mathf.Pow(StateMachine.transform.position.z - StateMachine.Target.transform.position.z, 2)) + 6f;
        Vector3 testPoint = StateMachine.Target.transform.position + new Vector3(0f,0f, _radiusToTarget);
        float angleRadius = Vector3.SignedAngle(testPoint, StateMachine.transform.position, StateMachine.Target.transform.position);
        if (angleRadius >= 0)
        {
            float dirX = StateMachine.Target.transform.position.x + _radiusToTarget * Mathf.Sin(Mathf.Deg2Rad * (30 + angleRadius));
            float dirZ = StateMachine.Target.transform.position.z + _radiusToTarget * Mathf.Cos(Mathf.Deg2Rad * (30 + angleRadius));
            _possibleFlankingPoint = new Vector3(dirX, StateMachine.Target.transform.position.y, dirZ);
        }
        else
        {
            float dirX = StateMachine.Target.transform.position.x + _radiusToTarget * Mathf.Sin(Mathf.Deg2Rad * (-30 + angleRadius));
            float dirZ = StateMachine.Target.transform.position.z + _radiusToTarget * Mathf.Cos(Mathf.Deg2Rad * (-30 + angleRadius));
            _possibleFlankingPoint = new Vector3(dirX, StateMachine.Target.transform.position.y, dirZ);
        }
        _flankingPoint = _possibleFlankingPoint;
    }
    private void CheckFlanking()
    {
        float currentDistance = Mathf.Sqrt(Mathf.Pow(StateMachine.transform.position.x - StateMachine.Target.transform.position.x , 2) 
        + Mathf.Pow(StateMachine.transform.position.z - StateMachine.Target.transform.position.z, 2));
        bool trafficCheck = Physics.SphereCast(StateMachine.transform.position, 0.4f, StateMachine.transform.forward, out RaycastHit hit, currentDistance + 0.5f, _enemyAlliesLayer);
        _flankingTimer = 0f;

        if(trafficCheck)
        {
            CalculateFlankkingPath();
        }
        else
        {
            StateMachine.SwitchState(new EnemyChasingState(StateMachine));
        }
    }
    private void CheckDistanceToFlankingPoint()
    {
        float distanceToPoint = Mathf.Sqrt(Mathf.Pow(StateMachine.transform.position.x - _flankingPoint.x, 2f)
        + Mathf.Pow(StateMachine.transform.position.z - _flankingPoint.z, 2f));
        if (distanceToPoint <= 0.5f)
        {
            CheckFlanking();
        }
    }

    
}
}

