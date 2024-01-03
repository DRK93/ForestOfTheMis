using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using _MyScripts.Combat;
using _MyScripts.Combat.WeaponMechanics;
using _MyScripts.Targeting;
using _MyScripts.StateMachines;

namespace _MyScripts.StateMachines.EnemyStates
{
    public class EnemyStateMachine : StateMachine
{
    protected EnemyStateMachine StateMachine;
    [field: SerializeField] public Animator Animator {get; private set;}
    [field: SerializeField] public CharacterController Controller {get; private set;}
    [field: SerializeField] public ForceReceiver ForceReceiver{get; private set;}
    [field: SerializeField] public NavMeshAgent Agent{get; private set;}
    [field: SerializeField] public Target TargetComponent {get; private set;}
    [field: SerializeField] public Ragdoll Ragdoll {get; private set;}
    [field: SerializeField] public Damageable EnemyDamageable {get; private set;}
    [field: SerializeField] public WeaponHandlerer WeaponHandlerer {get; private set;}
    [field: SerializeField] public WeaponGripManager WeaponGripManager { get; private set; }

    public EnemiesPredictionCenter EnemiesPredictionCenter;
    //
    [field: SerializeField] public float MovementSpeed {get; private set;}
    [field: SerializeField] public float WalkSpeed {get; private set;}
    public bool isRunning;
    public int locomotionNumber;
    public string locomotionName;
    [field: SerializeField] public float DetectionRange {get; private set;}
    [field: SerializeField] public float PlayerAttackAwareRange { get; private set; }
    [field: SerializeField] public bool PlayerAwareness { get; set; }
    [field: SerializeField] public float AttackRange {get; private set;}
    [field: SerializeField] public int AttackDamage {get; private set;}
    [field: SerializeField] public float AttackKnockback {get; private set;}
    //
    [field: SerializeField] public int AttackNumber{get; private set;}
    [field: SerializeField] public int AttackSchemeNumber{get; private set;}
    [field: SerializeField] public int AttackSchemeTact{get; private set;}
    [field: SerializeField] public EnemyStatsScriptableObject EnemyStats {get; private set;}
    [field: SerializeField] public EnemySchemes CurrentScheme {get; private set;}
    [field: SerializeField] public GameObject Target {get; private set;}
    
    private void Start()
    {
        // this one will be only for testing purpose, there will other way for targets in future
        Target = GameObject.FindGameObjectWithTag("Player");
        Agent.updatePosition = false;
        Agent.updateRotation = false;
        CurrentScheme = EnemyStats.EnemySchemes[0];
        SwitchState(new EnemyIdleState(this));
    }

    private void OnDrawGizmoSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
    }

    private void OnEnable()
    {
        EnemyDamageable.OnTakeDamage += HandleTakeDamage;
        EnemyDamageable.OnDie += HandleDie;
    }
    private void OnDisable()
    {
        EnemyDamageable.OnTakeDamage -= HandleTakeDamage;
        EnemyDamageable.OnDie -= HandleDie;
    }

    private void HandleTakeDamage()
    {
        //Debug.Log("Switching");
        SwitchState(new EnemyImpactState(this));
        //return;
    }

    private void HandleDie()
    {
        SwitchState(new EnemyDeadState(this));
    }
    public void AttackNumberIncrement()
    {
        if ( AttackNumber < EnemyStats.AttackList.Count -1 )
        {
            AttackNumber++;
        }
        else
        {
            AttackNumberReset();
        }
    }
    public void AttackNumberReset()
    {
        AttackNumber = 0;
    }
    public void CalculateAttackScheme()
    {
        if ( AttackSchemeNumber + 1 >= CurrentScheme.AttackTact.Count)
        {
            AttackSchemeNumber = 0;
            if ( AttackSchemeTact + 1 > 2)
            {
                AttackSchemeTact = 0;
                SetCurrentScheme();
            } 
            else
                AttackSchemeTact++;
        }
        else
        {
            AttackSchemeNumber++;
        }
            
    }
    private void SetCurrentScheme()
    {
        // lottery for next attack scheme
        // how to validate this schemes?
        var rand = new System.Random();
        int randomNumber = rand.Next(101);
        if ( randomNumber <= 25)
        {
            CurrentScheme = EnemyStats.EnemySchemes[0];
        }
        if (randomNumber > 25 && randomNumber <= 45)
        {
            CurrentScheme = EnemyStats.EnemySchemes[1];
        }
        if (randomNumber > 45 && randomNumber <= 65)
        {
            CurrentScheme = EnemyStats.EnemySchemes[2];
        }
        if (randomNumber > 65 && randomNumber <= 80)
        {           
            CurrentScheme = EnemyStats.EnemySchemes[3];
        } 
        if (randomNumber > 80 && randomNumber <= 90)
        {           
            CurrentScheme = EnemyStats.EnemySchemes[4];
        }
        if (randomNumber > 90 && randomNumber <= 100)
        {           
            CurrentScheme = EnemyStats.EnemySchemes[5];
        }
    }

    public void CalculatePlayerAttackReaction()
    {
        
    }
}
}

