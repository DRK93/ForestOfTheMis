using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using _MyScripts.Combat;
using _MyScripts.Combat.WeaponMechanics;
using _MyScripts.HUD;
using _MyScripts.HUD.HUD_Bars;
using _MyScripts.Targeting;
using _MyScripts.Player;

namespace _MyScripts.StateMachines.PlayerStates
{
    public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader {get; private set;}
    [field: SerializeField] public CharacterController CharController{get; private set;}
    [field: SerializeField] public Animator Animator{get; private set;}
    [field: SerializeField] public Targeter Targeter{get; private set;}
    [field: SerializeField] public ForceReceiver ForceReceiver{get; private set;}
    [field: SerializeField] public WeaponDamage WeaponStats{get; private set;}
    [field: SerializeField] public WeaponHandlerer WeaponHandlerer{get; private set;}
    [field: SerializeField] public WeaponManager WeaponManager { get; private set; }
    [field: SerializeField] public WeaponGripManager WeaponGripManager { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll{get; private set;}
    [field: SerializeField] public Damageable PlayerDamageable {get; private set;}
    [field: SerializeField] public SkillUIManager SkillUIManager { get; private set; }
    [field: SerializeField] public HUDManager HUDManager { get; private set; }
    [field: SerializeField] public SkillBar AttackBar { get;  set; }
    [field: SerializeField] public SkillBar SkillBar { get; set; }
    [SerializeField] public LayerMask layerMask;
    [field: SerializeField] public bool WeaponChangeAllowed { get; set; }
    [field: SerializeField] public float FreeMovementSpeed{get; private set;}
    [field: SerializeField] public float FreeWalkingtSpeed{get; private set;}
    [field: SerializeField] public float TargetMovementSpeed{get; private set;}
    [field: SerializeField] public float RotationDamp{get; private set;}
    [field: SerializeField] public float DodgeDuration{get; private set;}
    [field: SerializeField] public float DodgeDistance{get; private set;}
    [field: SerializeField] public float JumpForce{get; private set;}
    [field: SerializeField] public int LocomotionNumber{get; private set;}
    [field: SerializeField] public int AttackNumber{get; private set;}
    
    // temporary for blocking anims
    [field: SerializeField] public List<BlockingAnimScriptableObject> blockingAnimList = new List<BlockingAnimScriptableObject>();
    [field: SerializeField] public int BlockingAnimNumber {get; private set;}
    [field: SerializeField] public List<AttackScriptableObject> counterAnimList = new List<AttackScriptableObject>();
    [field: SerializeField] public BlockingAnimScriptableObject currentBlockingAnimation;
    [field: SerializeField] public BlockingAnimScriptableObject currentBlockedHit;
    [field: SerializeField] public AttackScriptableObject CurrentCounterAttack {get; set;}
    private readonly int _freeLookBlendTreeHash = Animator.StringToHash("Locomotion");
    private readonly int _oneHandShieldLocomotion = Animator.StringToHash("1H&Sh Locomotion");
    private readonly int _twoHandedLocomotion = Animator.StringToHash("2H Locomotion");
    private readonly int _spearLocomotion = Animator.StringToHash("Spear Locomotion");

    public int currentLocomotion;
    public bool isRunning;

    public float animatorWalkSpeed;
    public float animatorRunSpeed;
    //[field: SerializeField] public AttackSriptableObject[] IdleSO {get; private set;}
    public float PreviousDodgeTime {get; private set;}  = Mathf.NegativeInfinity;
    public Transform MainCameraTransform {get; private set;}
    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        MainCameraTransform = Camera.main.transform; 
        currentLocomotion = _freeLookBlendTreeHash;
        SwitchState(new PlayerFreeLookState(this));
        WeaponManager.SetShieldBlocking += CurrentBlockingShield;
        WeaponManager.SetTwoHandedBlocking += CurrentBlockingTwoHanded;
        WeaponManager.SetSpaerBlocking += CurrentBlockingSpear;
    }

    private void OnEnable()
    {
        PlayerDamageable.OnTakeDamage += HandleTakeDamage;
        PlayerDamageable.OnDie += HandleDie;
    }
    private void OnDisable()
    {
        PlayerDamageable.OnTakeDamage -= HandleTakeDamage;
        PlayerDamageable.OnDie -= HandleDie;
        WeaponManager.SetShieldBlocking -= CurrentBlockingShield;
        WeaponManager.SetTwoHandedBlocking -= CurrentBlockingTwoHanded;
        WeaponManager.SetSpaerBlocking -= CurrentBlockingSpear;
    }

    private void HandleTakeDamage()
    {
        SwitchState(new PlayerImpactState(this));
    }

    private void HandleDie()
    {
        SwitchState(new PlayerDeadState(this));
    }
    public void SetDodgeTime(float dodgeTime)
    {
        PreviousDodgeTime = dodgeTime;
    }

    public void AttackNumberIncrement()
    {
        //AttackBar.skillSlot1.attackData
        if (AttackBar.SkillSlot1.attackData == null)
        {
            AttackNumber = 10;
        }
        else
        {
            switch (AttackNumber)
            {
                case 0:
                    if (AttackBar.SkillSlot2.attackData == null)
                    {
                        AttackNumber = 0;
                    }
                    else
                    {
                        AttackNumber = 1;
                    }
                    break;
                case 1:
                    if (AttackBar.SkillSlot3.attackData == null)
                    {
                        AttackNumber = 0;
                    }
                    else
                    {
                        AttackNumber = 2;
                    }
                    break;    
                case 2:
                    if (AttackBar.SkillSlot4.attackData == null)
                    {
                        AttackNumber = 0;
                    }
                    else
                    {
                        AttackNumber = 3;
                    }
                    break; 
                case 3:
                    if (AttackBar.SkillSlot5.attackData == null)
                    {
                        AttackNumber = 0;
                    }
                    else
                    {
                        AttackNumber = 4;
                    }
                    break;    
                case 4:
                    AttackNumber = 0;
                    break;   
                default:
                    break;
            }
        }
    }
    private void CurrentBlockingShield()
    {
        currentBlockingAnimation = blockingAnimList[0];
        currentBlockedHit = blockingAnimList[1];
        CurrentCounterAttack = counterAnimList[0];
        currentLocomotion = _oneHandShieldLocomotion;
        LocomotionNumber = 1;
        SwitchState(new PlayerFreeLookState(this));
    }

    private void CurrentBlockingTwoHanded()
    {
        currentBlockingAnimation = blockingAnimList[2];
        currentBlockedHit = blockingAnimList[3];
        CurrentCounterAttack = counterAnimList[1];
        currentLocomotion = _twoHandedLocomotion;
        LocomotionNumber = 2;
        SwitchState(new PlayerFreeLookState(this));
    }

    private void CurrentBlockingSpear()
    {
        currentBlockingAnimation = blockingAnimList[4];
        currentBlockedHit = blockingAnimList[5];
        CurrentCounterAttack = counterAnimList[2];
        currentLocomotion = _spearLocomotion;
        LocomotionNumber = 3;
        SwitchState(new PlayerFreeLookState(this));
    }

    public void ResetAttackNumber()
    {
        AttackNumber = 0;
    }
}
}

