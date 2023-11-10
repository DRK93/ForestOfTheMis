using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _MyScripts.Combat;

namespace _MyScripts.StateMachines.EnemyStates
{
    public class EnemyAttackingState2 : EnemyBaseState, IAttackLogic
{
    private string _animationTagName = "Attack";
    private bool _alreadyAppliedForce;
    private bool _sequenceAttack;
    private bool _armedAttack;
    private bool _blocked;
    private int _attackIndexer;
    private int _attackMoveIndexer;
    private float _enemyNextAttackWindow;
    private bool _comboWindow;
    private bool _succesfulAttack;
    private float _comboWindowTimer;
    private int _diceRoll;
    private AttackScriptableObject _attackSo;
    public EnemyAttackingState2(EnemyStateMachine stateMachine, AttackScriptableObject attackSo) : base(stateMachine)
    {
        this._attackSo = attackSo;
        _sequenceAttack = attackSo.ComboAniamtion;
        _attackIndexer = 0;
        _attackMoveIndexer = 0;

        if(stateMachine.EnemyStats.SwordmanshipLevel == 1)
            _enemyNextAttackWindow = stateMachine.EnemyStats.AttackList[stateMachine.AttackNumber].EndAttackTime - 0.02f;
        if(stateMachine.EnemyStats.SwordmanshipLevel == 2)
            _enemyNextAttackWindow = (stateMachine.EnemyStats.AttackList[stateMachine.AttackNumber].NextAttackWindow 
            + stateMachine.EnemyStats.AttackList[stateMachine.AttackNumber].EndAttackTime)/2;
        if(stateMachine.EnemyStats.SwordmanshipLevel == 3)
            _enemyNextAttackWindow = stateMachine.EnemyStats.AttackList[stateMachine.AttackNumber].NextAttackWindow + 0.02f;
    }

    public override void Enter()
    {
        StateMachine.Animator.CrossFadeInFixedTime(_attackSo.AnimationName, _attackSo.TransisionDuration); 
        StateMachine.EnemyDamageable.RecoilWeapon += BlockedHit;
        StateMachine.EnemyDamageable.SuccesfulAttack += AttackSucces;
        
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        float normalizedTime = GetNormalizedTime(StateMachine.Animator, _animationTagName);
        if (normalizedTime < _attackSo.RotateTowardsEnemyTimer)
            FaceTarget();
        if(normalizedTime + 0.1f >= _attackSo.ArmAttackTimes[_attackIndexer] && !_armedAttack && normalizedTime < _attackSo.DisarmAttackTimes[_attackIndexer] && !_blocked)
        //if (normalizedTime >= _attackSo.ArmAttackTimes[_attackIndexer] && !_armedAttack && normalizedTime < _attackSo.DisarmAttackTimes[_attackIndexer])
        {
            ArmAttack();
        }

        if (normalizedTime >= _attackSo.DisarmAttackTimes[_attackIndexer] && _armedAttack)
        {
            DisarmAttack();
        }

        if (normalizedTime >= _attackSo.NextAttackWindow)
        {
            WindowForNextAttack();
        }

        if (_comboWindow)
        {
            _comboWindowTimer += Time.deltaTime;
            if (_comboWindowTimer > 0.3f)
            {
                WindowForNextAttackExit();
            }
        }

        /*if (_blocked && normalizedTime <= _attackSo.ReDoBlockedAttackTimers[_attackIndexer])
        {
            StateMachine.Animator.SetFloat("Speed", 1f);
            _blocked = false;
            ArmAttack();
        }*/
        // need to add a reDoAttack mechanic on Enemy Attack State////////////////
        if (normalizedTime < _attackSo.EndAttackTime)
        {
            StateMachine.Animator.speed = _attackSo.AnimationSpeedCurve.Evaluate(normalizedTime);
            if (_blocked)
            {
                //Debug.Log("blocked");
                //Debug.Log(normalizedTime);
                //if (normalizedTime <= attackSO.ReDoBlockedAttackTimers[attackIndexer])
                // if( normalizedTime <= 0.1f) 
                if (normalizedTime <= _attackSo.ReDoBlockedAttackTimers[_attackIndexer] - 0.1f)
                {
                    if(_diceRoll < 5)
                        TryNextAttack();
                    else
                    {
                        _blocked = false;
                        StateMachine.Animator.SetFloat("Speed", 1f);
                    }
                }
            }
            else
            {
                if (!_alreadyAppliedForce)
                {
                    if (normalizedTime >= _attackSo.ForceTimers[_attackMoveIndexer])
                    {
                        TryApplyMoveForce();
                    }
                }

                if (normalizedTime >= _enemyNextAttackWindow)
                {
                    TryNextAttack();
                }
            }

        }
        else
        {
            EndState();
        }
    }

    public override void Exit()
    {
        // double check for any changes in animator speed
        StateMachine.Animator.SetFloat("Speed", 1f);
        StateMachine.EnemyDamageable.RecoilWeapon -= BlockedHit;
        StateMachine.EnemyDamageable.SuccesfulAttack -= AttackSucces;
        StateMachine.Animator.speed = 1f;
        UnsetWeapons();
    }


    public void ArmAttack()
    {
        _armedAttack = true;
        SetWeapons();
    }

    public void DisarmAttack()
    {
        _armedAttack = false;
        UnsetWeapons();
        if (_attackSo.ArmAttackTimes.Count > _attackIndexer +1)
        {
            _attackIndexer++;
        }
    }
    private void SetWeapons()
    {
        if (_attackSo.RightHand[_attackIndexer])
        {
            StateMachine.WeaponHandlerer.EnableMainWeapon(_attackSo.Damages[_attackIndexer], _attackSo.Knockbacks[_attackIndexer], _attackSo.BlockableAttack, _attackSo.BlockCost[_attackIndexer]);
        }
        else
        {
            StateMachine.WeaponHandlerer.SecondWeaponEnable(_attackSo.Damages[_attackIndexer], _attackSo.Knockbacks[_attackIndexer],_attackSo.BlockableAttack, _attackSo.BlockCost[_attackIndexer]);
        }
    }
    
    private void UnsetWeapons()
    {
        if (_attackSo.RightHand[_attackIndexer])
        {
            StateMachine.WeaponHandlerer.DisableMainWeapon();
        }
        else
        { StateMachine.WeaponHandlerer.SecondWeaponDisable(); 
        }
    }

    public void TryNextAttack()
    {
        if(IsInAttackRange(0f))
        {
            StateMachine.CalculateAttackScheme();
            //Debug.Log(stateMachine.AttackSchemeNumber);

            if(StateMachine.CurrentScheme.AttackTact[StateMachine.AttackSchemeNumber] == EnemySchemes.FightState.Attack)
            {
                //if (_succesfulAttack)
                if(!_blocked)
                {   
                    StateMachine.AttackNumberIncrement();
                    StateMachine.SwitchState(new EnemyAttackingState2(StateMachine, StateMachine.EnemyStats.AttackList[StateMachine.AttackNumber]));
                }
                else
                {
                    StateMachine.AttackNumberReset();
                    StateMachine.SwitchState(new EnemyAttackingState2(StateMachine, StateMachine.EnemyStats.AttackList[StateMachine.AttackNumber]));
                }
            }
            if(StateMachine.CurrentScheme.AttackTact[StateMachine.AttackSchemeNumber] == EnemySchemes.FightState.Block)
            {
                //stateMachine.AttackNumberIncrement();
                StateMachine.SwitchState(new EnemyBlockingState(StateMachine, 1, StateMachine.EnemyStats.BlockList[0].TransisionDuration, StateMachine.EnemyStats.BlockList[0]));
            }
            if(StateMachine.CurrentScheme.AttackTact[StateMachine.AttackSchemeNumber] == EnemySchemes.FightState.Dodge)
            {
                //stateMachine.AttackNumberIncrement();
            StateMachine.SwitchState(new EnemyDodgeState(StateMachine));
            }
        }
        else
        {
            StateMachine.SwitchState(new EnemyChasingState(StateMachine));
        }
    }

    public void TryApplyMoveForce()
    {
        if(CheckIfTooClose(StateMachine.Target))
        {

        }
        else
        {
            if(_attackMoveIndexer <= _attackSo.MoveForces.Count -1)
            {
                StateMachine.ForceReceiver.AddForce(StateMachine.transform.forward * _attackSo.MoveForces[_attackMoveIndexer]);
                _attackMoveIndexer++;
            }
            else
            {
                if(!_alreadyAppliedForce)
                {
                    _alreadyAppliedForce = true;
                }
            }
            // after increment attackIndexer if hits the border make sure, that it will not come back to this method
            if (_attackMoveIndexer > _attackSo.MoveForces.Count -1)
            {
                _alreadyAppliedForce = true;
            }
        }

    }
    private bool CheckIfTooClose(GameObject enemy)
    {
        float distance = CalculateDistance(enemy.transform);
        //Debug.Log("Enemy Distance  " + distance);
        // temporary check until all attacks have their parameters set
        if (_attackSo.AttackRange.Count > 0)
        {
            if (1.15f < distance && distance < _attackSo.AttackRange[_attackIndexer])
                return true;
            else if (distance <= 1.15f)
            {
                StateMachine.ForceReceiver.AddForce(StateMachine.transform.forward * -5f);
                return false;
            }
            else
            {
                return false; 
            }
        }
        else
        {
            if (distance < 1.3f)
                return true;
            else
                return false;
        }
  
    }
    private float CalculateDistance(Transform enemyTransform)
    {
        return Mathf.Sqrt(Mathf.Pow(StateMachine.transform.position.x - enemyTransform.position.x , 2) 
                          + Mathf.Pow(StateMachine.transform.position.z - enemyTransform.position.z, 2));
    }
    private bool IsInAttackRange(float correction)
    {
        if(StateMachine.Target.GetComponent<Damageable>().IsDead) {return false;}
        float distanceSquare = (StateMachine.Target.transform.position - StateMachine.transform.position).sqrMagnitude;
        return distanceSquare <= (StateMachine.AttackRange - correction ) * (StateMachine.AttackRange - correction);
    }
    public void BlockedHit()
    {
        DisarmAttack();
        StateMachine.EnemyDamageable.RecoilWeapon -= BlockedHit;
        _diceRoll = Random.Range(0, 6);
        _blocked = true;
        StateMachine.Animator.SetFloat("Speed", -0.4f);
        }

    public void ParriedHit()
    {
        
    }

    public void ReDoAttack()
    {
        
    }
    public void EndState()
    {
        StateMachine.Animator.SetFloat("Speed", 1f);
        if(StateMachine.Target == null)
        {
            StateMachine.SwitchState(new EnemyIdleState(StateMachine));
        }
        else
        {
            StateMachine.SwitchState(new EnemyChasingState(StateMachine));
        }
    }
    private void WindowForNextAttack()
    {
        _comboWindow = true;
        StateMachine.Animator.SetFloat("Speed", 0.1f);

    }
    private void WindowForNextAttackExit()
    {
        _comboWindow = false;
        StateMachine.Animator.SetFloat("Speed", 1f);
    }
    private void AttackSucces()
    {
        _succesfulAttack = true;
    }
}
}

