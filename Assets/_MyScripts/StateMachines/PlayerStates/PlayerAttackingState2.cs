using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _MyScripts.Targeting;

namespace _MyScripts.StateMachines.PlayerStates
{
    public class PlayerAttackingState2 : PlayerBaseState
{
    //private float previousFrameTime;
    private bool _alreadyAppliedForce;
    private string _animationTagName = "Attack";
    private float _animationSpeedBeforeStop;
    private AttackScriptableObject _attackSo;
    private bool _sequence;
    private bool _slowedTime;
    private bool _sequnceAttack;
    private bool _armedAttack;
    private bool _nextAttack;
    private bool _blocked;
    private bool _parried;
    private bool _secondAttack;
    private int _attackIndexer;
    private int _attackMoveIndexer;
    private bool _dodgeOn;
    private bool _comboWindow;
    private float _comboWindowTimer;
    private bool _nextAttackImpossible;
    private LayerMask _obstacleLayres = LayerMask.GetMask("Enemy");
    public PlayerAttackingState2(PlayerStateMachine stateMachine, AttackScriptableObject attackSo) : base(stateMachine)
    {
        this._attackSo = attackSo;
        //sequence = attackSO.ComboAniamtion;
        _attackIndexer = 0;
        _attackMoveIndexer = 0;
    }

    public override void Enter()
    {
        StateMachine.WeaponGripManager.ChangeGrip(_attackSo.AttackGripNumber);
        StateMachine.WeaponChangeAllowed = false;
        StateMachine.Animator.CrossFadeInFixedTime(_attackSo.AnimationName, _attackSo.TransisionDuration); 
        StateMachine.InputReader.DodgeEvent += DoDodge;
        StateMachine.InputReader.TargetEvent += OnTarget;
        _comboWindowTimer = 0f;
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        float normalizedTime = GetNormalizedTime(StateMachine.Animator,_animationTagName);
        StateMachine.SkillUIManager.MoveAttackArrow(normalizedTime, StateMachine.AttackNumber);
        if(normalizedTime < _attackSo.RotateTowardsEnemyTimer)
            FaceTarget();

        if(normalizedTime >= _attackSo.ArmAttackTimes[_attackIndexer] && !_armedAttack && normalizedTime < _attackSo.DisarmAttackTimes[_attackIndexer])
        {
            ArmAttack();
        }
        if(normalizedTime >= _attackSo.DisarmAttackTimes[_attackIndexer] && _armedAttack)
        {
            DisarmAttack();
        }
        // EndAttackTime indicates that the animation ended and could change for another attack,

        if(normalizedTime < _attackSo.EndAttackTime)
        {
            if(!_slowedTime)
                StateMachine.Animator.speed = _attackSo.AnimationSpeedCurve.Evaluate(normalizedTime);
            else
            {
                if (normalizedTime <= _attackSo.ReDoBlockedAttackTimers[_attackIndexer])
                {
                    EndState();
                }
                if(StateMachine.InputReader.IsAttacking && _secondAttack == false)
                {
                    ReDoAttack();
                }
                // end slow Time while redoattack
                if(normalizedTime > _attackSo.ReDoBlockedAttackTimers[_attackIndexer])
                {
                    SlowTime();
                }
            }

            if(!_alreadyAppliedForce)
            {
                if (normalizedTime >= _attackSo.ForceTimers[_attackMoveIndexer])
                {
                    TryApplyMoveForce();
                }
            }

            if(StateMachine.InputReader.IsAttacking && normalizedTime >= _attackSo.NextAttackWindow && !_nextAttack && !_slowedTime)
            {
                if(!_nextAttackImpossible)
                    _nextAttack = true;
            }
            if(StateMachine.InputReader.IsAttacking && normalizedTime < _attackSo.NextAttackWindow && normalizedTime> 0.25f && !_nextAttackImpossible  && !_slowedTime)
            {
                _nextAttackImpossible = true;
            }
            // Next attack check
            if (normalizedTime >= _attackSo.NextAttackWindow)
            {
                WindowForCombo();
            }
            if ( _comboWindow)
            {
                _comboWindowTimer += Time.deltaTime;
                if(normalizedTime >= _attackSo.NextAttackStartTime)
                    if (_comboWindowTimer > 0.3f)
                    {
                        WindowForComboExit();
                    }
            }
            if(normalizedTime >= _attackSo.NextAttackStartTime && _nextAttack)
            {
                TryNextAttack();
            }
            if (normalizedTime >= _attackSo.NextAttackWindow + 0.2f)
            {
                if(StateMachine.InputReader.IsBlocking)
                {
                    _nextAttack = false;
                    StateMachine.WeaponGripManager.ReturnToBaseGrip(_attackSo.AttackGripNumber / 10);
                    StateMachine.SwitchState(new PlayerBlockingState(StateMachine, 1, _attackSo.BlockCrossFadeDuration, StateMachine.currentBlockingAnimation));
                    return;
                }
                if(_dodgeOn)
                {
                    StateMachine.WeaponGripManager.ReturnToBaseGrip(_attackSo.AttackGripNumber / 10);
                    StateMachine.SwitchState(new PlayerDodgingState(StateMachine, StateMachine.InputReader.MovementValue));
                    
                }
                
            }
            // if(stateMachine.InputReader.IsBlocking && normalizedTime >= attackSO.NextAttackWindow + 0.2f)
            // {
            //     nextAttack = false;
            //     stateMachine.SwitchState(new PlayerBlockingState(stateMachine, 1, stateMachine.BlockingAnimList[0]));
            // }
            // return to combat idle after block/parried without reDoAttack mechanic
            
            // probably this part is not needed
            if (_blocked || _parried)
            {
                //if (normalizedTime <= attackSO.BlockedAnimTimers[attackIndexer])
                if (normalizedTime <= _attackSo.ReDoBlockedAttackTimers[_attackIndexer] + 0.02f)
                {
                    EndState();
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
        //StateMachine.WeaponGripManager.ReturnToBaseGrip(_attackSo.AttackGripNumber / 10);
        // need to be sure that logic from this class will not affect outside of it, in next state
        Time.timeScale = 1f;
        StateMachine.Animator.SetFloat("Speed", 1f);
        UnsetWeapons();
        WindowForComboExit();

        StateMachine.InputReader.DodgeEvent -= DoDodge;
        StateMachine.InputReader.TargetEvent -= OnTarget;
        StateMachine.Animator.speed = 1f;
    }

    private void EndState()
    {
        StateMachine.ResetAttackNumber();
        if (StateMachine.Targeter.CurrentTarget != null)
        {
            //stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            StateMachine.WeaponGripManager.ReturnToBaseGrip(_attackSo.AttackGripNumber / 10);
            StateMachine.SwitchState( new PlayerFreeLookState(StateMachine));
        }
        else
        {
            StateMachine.WeaponGripManager.ReturnToBaseGrip(_attackSo.AttackGripNumber / 10);
            StateMachine.SwitchState( new PlayerFreeLookState(StateMachine));
        }
    }
    private void ArmAttack()
    {
        _armedAttack = true;
        SetWeapons();
    }
    private void DisarmAttack()
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
            StateMachine.WeaponHandlerer.EnableMainWeapon(_attackSo.Damages[_attackIndexer], _attackSo.Knockbacks[_attackIndexer],
                _attackSo.BlockableAttack, _attackSo.BlockCost[_attackIndexer]);
        }
        else
        {
            StateMachine.WeaponHandlerer.SecondWeaponEnable(_attackSo.Damages[_attackIndexer], _attackSo.Knockbacks[_attackIndexer],
                _attackSo.BlockableAttack, _attackSo.BlockCost[_attackIndexer]);
        }
    }
    private void UnsetWeapons()
    {
        if (!_sequence)
        {
            if (_attackSo.RightHand[0])
            {
                StateMachine.WeaponHandlerer.DisableMainWeapon();
            }
            else
            {
                StateMachine.WeaponHandlerer.SecondWeaponDisable();
            }
        }
        else
        {
            if (_attackSo.RightHand[_attackIndexer])
            {
                StateMachine.WeaponHandlerer.DisableMainWeapon();
            }
            else
            {
                StateMachine.WeaponHandlerer.SecondWeaponDisable();
            }
        }
    }
    private void TryApplyMoveForce()
    {
        // need to check if there is place for movement
        if (CheckPossibleCollision())
        {
            if(!_alreadyAppliedForce)
            {
                _alreadyAppliedForce = true;
            }
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
            // There should be some changes to work properly when redo attack has movement force timer in
        }
    }
    
    private void BlockedHit()
    {
        _blocked = true;
        if (true)
        {
            StateMachine.Animator.SetFloat("Speed", -1f);
        }
        else
        {
            ReAttackWindow();
        }
    }
    private void ReAttackWindow()
    {
        _blocked = false;
        StateMachine.Animator.SetFloat("Speed", -0.4f);
        SlowTime();
    }

    private void SlowTime()
    {
        // value need to be get from somewhere, not just here
        _slowedTime = !_slowedTime;
        if(_slowedTime)
            Time.timeScale = 0.1f;
        else
            Time.timeScale = 1f;
    }

    private void ReDoAttack()
    {
        _secondAttack = true;
        //ArmAttack();
        StateMachine.Animator.SetFloat("Speed", 1f);
        Time.timeScale = 0.5f;
        //SlowTime();
    }
    private void DoDodge()
    {
        if(StateMachine.InputReader.MovementValue == Vector2.zero) {return;}
            _dodgeOn = true;
    }
    private void WindowForCombo()
    {
        _comboWindow = true;
        //stateMachine.Animator.SetFloat("Speed", 0.1f);
        StateMachine.SkillUIManager.LightAttackArrowOn();
    }
    private void WindowForComboExit()
    {
        _comboWindow = false;
        StateMachine.SkillUIManager.LightAttackArrowOff();
        //stateMachine.Animator.SetFloat("Speed", 1f);
    }
        private bool CheckPossibleCollision()
    {
        if (Physics.SphereCast(StateMachine.transform.position, 0.5f, StateMachine.transform.forward,
                out RaycastHit hit, 4f, StateMachine.layerMask))
            {
                hit.transform.TryGetComponent<Target>(out Target enemy);
                return CheckIfTooClose(enemy);
            }
            else
            {
                return false;
            }
        
    }
    private bool CheckIfTooClose(Target enemy)
    {
        float distance = CalculateDistance(enemy.transform);
        //Debug.Log("Distance  " + distance);
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

}
}

