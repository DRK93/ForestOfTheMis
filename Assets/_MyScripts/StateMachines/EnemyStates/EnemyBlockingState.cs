using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.StateMachines.EnemyStates
{
    public class EnemyBlockingState : EnemyBaseState
{
    private string _animationTagName = "Block";
    private float _crossFadeDuration = 0.1f;
    private int _blockingWeaponNumber;
    private bool _blocking;
    private bool _firstCheck;
    private bool _blockedHitAnim;
    private bool _counterCome;
    private int _counterCheck;
    private float _counterTimer;
    // there should be some changes here, so it will have better algorithm for how long npc stay in block state
    private float _howLongBlocking = 2f;

    private BlockingAnimScriptableObject _blockingSo;
    public EnemyBlockingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }
    public EnemyBlockingState(EnemyStateMachine stateMachine, int blockingWeaponNumber, float crossTime, BlockingAnimScriptableObject blockSo) : base(stateMachine)
    {
        this._blockingWeaponNumber = blockingWeaponNumber;
        _blockingSo = blockSo;
        // safety check, if there will not be assigned any value to crossFadeTime
        if(crossTime != 0f)
            _crossFadeDuration = crossTime;
    }

    public override void Enter()
    {
        StateMachine.Animator.CrossFadeInFixedTime(_blockingSo.AnimationName, _crossFadeDuration);
        StateMachine.EnemyDamageable.SuccesfulBlock += BlockingAnim;
        _howLongBlocking = BlockLenghtGenerator();
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        float normalizedTime = GetNormalizedTime(StateMachine.Animator,_animationTagName);

        if (_blockedHitAnim)
        {
            if(_firstCheck)
            {
                _firstCheck = false;
                normalizedTime = 0f;
            }
            if (normalizedTime >= 0.80f)
            {
                if (normalizedTime > _blockingSo.StartCounterWindow && _counterCome)
                {
                 StateMachine.SwitchState(new EnemyAttackingState2(StateMachine, StateMachine.EnemyStats.CounterList[0]));
                 return;
                }
                else
                    BackToBlockPosition();
            }
        }
        else
        {
            if( normalizedTime >= _blockingSo.StartBlockingTime && _blocking == false )
            {
                _blocking = true;
                StartBlocking();
            }
            if (normalizedTime < _blockingSo.RotateTowardsEnemyTimer)
            {
                FaceTarget();
            }

            if (normalizedTime > _howLongBlocking )
            {
                TryNextAttack();
            }
             if (normalizedTime > _blockingSo.StartCounterWindow && _counterCome)
             {
                 StateMachine.SwitchState(new EnemyAttackingState2(StateMachine, StateMachine.EnemyStats.CounterList[0]));
                 return;
             }
        }

    }
    public override void Exit()
    {
         EndBlocking();
         StateMachine.Animator.speed = 1f;
    }

    private void StartBlocking()
    {
         _blocking = true;
        StateMachine.EnemyDamageable.SetBlocking();
    }
    public void EndBlocking()
    {
        _blocking = false;
        StateMachine.EnemyDamageable.RemoveBlocking();
    }

    private void SuccesfulBlock()
    {
        //stateMachine.SwitchState(new EnemyBlockingState (stateMachine, 1, stateMachine.EnemyStats.BlockList[0].TransisionDuration, stateMachine.EnemyStats.BlockList[1]));
    }



    private void BlockingAnim()
    {
        StateMachine.EnemyDamageable.SuccesfulBlock -= BlockingAnim;
        _blockedHitAnim = true;
        _firstCheck = true;

        StateMachine.Animator.CrossFadeInFixedTime(StateMachine.EnemyStats.BlockList[1].AnimationName, _crossFadeDuration);
    }
    private void BackToBlockPosition()
    {
        _blockedHitAnim = false;
        StateMachine.EnemyDamageable.SuccesfulBlock += BlockingAnim;
        _counterCheck = CounterGenerator();

        if (_counterCheck == StateMachine.EnemyStats.CounterRate)
            _counterCome = true;

        StateMachine.Animator.CrossFadeInFixedTime(_blockingSo.AnimationName, _crossFadeDuration);
    }

    public void TryNextAttack()
    {
        StateMachine.CalculateAttackScheme();

        if(StateMachine.CurrentScheme.AttackTact[StateMachine.AttackSchemeNumber] == EnemySchemes.FightState.Attack)
        {
            StateMachine.AttackNumberIncrement();
            StateMachine.SwitchState(new EnemyAttackingState2(StateMachine, StateMachine.EnemyStats.AttackList[StateMachine.AttackNumber]));
        }
        if(StateMachine.CurrentScheme.AttackTact[StateMachine.AttackSchemeNumber] == EnemySchemes.FightState.Block)
        {
            StateMachine.SwitchState(new EnemyBlockingState(StateMachine, 1, StateMachine.EnemyStats.BlockList[0].TransisionDuration , StateMachine.EnemyStats.BlockList[0]));
        }
        if(StateMachine.CurrentScheme.AttackTact[StateMachine.AttackSchemeNumber] == EnemySchemes.FightState.Dodge)
        {
             StateMachine.SwitchState(new EnemyDodgeState(StateMachine));
        }
    }

    private float BlockLenghtGenerator()
    {
        return Random.Range(1f, 3f);
    }

    private int CounterGenerator()
    {
        return Random.Range(0, StateMachine.EnemyStats.CounterRate + 1);
    }
}
}



