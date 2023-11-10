using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using _MyScripts.Combat;
using _MyScripts.Targeting;
using _MyScripts.StateMachines.EnemyStates;
using UnityEngine.Serialization;

namespace _MyScripts.HUD
{
    public class HUDManager : MonoBehaviour
{
    [field: Header("NeededComponents to work HUD")]
    [field: SerializeField] public Damageable PlayerDamageable { get; private set; }
    [field: SerializeField] public Targeter PlayerTargeter { get; private set;  }
    [field: SerializeField] public HUDWeaponIcon HUDWeaponIcon { get; private set; }
    
    // This TMPro will be changed automatic while playing game
    [field: Header("Text fields")]
    [field: SerializeField] public TextMeshProUGUI CurrentHp{ get; private set; }
    [field: SerializeField] public TextMeshProUGUI CurrentMp{ get; private set; }
    [field: SerializeField] public TextMeshProUGUI CurrentPower{ get; private set; }
    [field: SerializeField] public TextMeshProUGUI MaxHp{ get; private set; }
    [field: SerializeField] public TextMeshProUGUI MaxMp { get; private set; }
    [field: SerializeField] public TextMeshProUGUI MaxPower { get; private set; }
    [field: SerializeField] public TextMeshProUGUI Exp { get; private set; }
    [field: SerializeField] public TextMeshProUGUI EnemyName { get; private set; }
    [field: SerializeField] public TextMeshProUGUI EnemyLevel { get; private set; }
    [field: SerializeField] public TextMeshProUGUI BossName { get; private set; }
    [field: SerializeField] public TextMeshProUGUI BossLevel { get; private set; }
    [field: SerializeField] public TextMeshProUGUI NpcName { get; private set; }
    [field: SerializeField] public TextMeshProUGUI NpcLevel { get; private set; }
    
    [field: Header("Slider fields")]
    // This UI Objects will be changed automatic while playing game
    [field: SerializeField] public Slider PlayerHpSlider { get; private set; }
    [field: SerializeField] public Slider PlayerMpSlider { get; private set; }
    [field: SerializeField] public Slider PlayerPowerSlider { get; private set; }
    [field: SerializeField] public Slider ExpSlider { get; private set; }
    [field: SerializeField] public Slider EnemyHpSlider { get; private set; }
    [field: SerializeField] public Slider EnemyPowerSlider { get; private set; }
    [field: SerializeField] public Slider BossHpSlider { get; private set; }
    [field: SerializeField] public Slider NpcHpSlider { get; private set; }
    // This UI Objects will respond to player actions

    //Gameobjects that will be activated by UI
    [field: Header("GameObjects which will respond")]
    public GameObject enemyPanel;
    public GameObject bossPanel;
    public GameObject neutralPanel;
    
    [field: Header("Attack and Skill bars")]
    public GameObject oneHandedShieldAttackBars;
    public GameObject twoHandedAttackBars;
    public GameObject spearAttackBars;
    public GameObject oneHandShieldSkillBar;
    public GameObject twoHandedSkillBar;
    public GameObject spearSkillBar;

    public AudioSource buttonClick;
    public Action attackUpdate;
    public Action buttonClicked;
    
    private GameObject _currentAttackBar;
    private Damageable _currentTargetDamageable;
    private int _attackBarIndexer = 0;

    // Check if HUD is allowed to do anything or not
    [SerializeField] private bool isHudAllowed;
    public bool IsHudAllowed
    {
        get {return isHudAllowed;}
        set { isHudAllowed = value;}
    }
    private void Start()
    {
        SetStartValues();
        PlayerDamageable.OnTakeDamage += UpdateHp;
        PlayerDamageable.OnDie += ZeroHp;
        PlayerDamageable.UseMana += UdpateMp;
        PlayerDamageable.UsePower += UpdatePower;
        PlayerDamageable.LevelUp += SetStartValues;
        PlayerDamageable.AtributeChanged += SetStartValues;
        PlayerDamageable.RegenUpdate += UpdateHp;
        PlayerDamageable.RegenUpdate += UpdatePower;
        PlayerDamageable.RegenUpdate += UpdateHp;
        _currentAttackBar = spearAttackBars;
        buttonClicked += PlayClickAudio;
        PlayerTargeter.MainTarget += NewEnemy;
        PlayerTargeter.NoTarget += HideEnemyPanel;
    }

    private void SetStartValues()
    {
        UpdateHp();
        UdpateMp();
        UpdatePower();
    }
    private void UpdateHp()
    {
        CurrentHp.text = PlayerDamageable.Health().ToString();
        MaxHp.text = PlayerDamageable.MaxHealth().ToString();
        float curHealth = PlayerDamageable.Health();
        float maxHealth = PlayerDamageable.MaxHealth();
        PlayerHpSlider.value = curHealth/maxHealth * 100f;
    }
    private void ZeroHp()
    {
        PlayerHpSlider.value = 0f;
    }

    private void UdpateMp()
    {
        CurrentMp.text = PlayerDamageable.Mana().ToString();
        MaxMp.text = PlayerDamageable.MaxMana().ToString();
        float curMana = PlayerDamageable.Mana();
        float maxMana = PlayerDamageable.MaxMana();
        PlayerMpSlider.value = curMana/maxMana * 100f;
    }

    private void UpdatePower()
    {
        CurrentPower.text = PlayerDamageable.Power().ToString();
        MaxPower.text = PlayerDamageable.MaxPower().ToString();
        float curPower = PlayerDamageable.Power();
        float maxPower = PlayerDamageable.MaxPower();
        PlayerPowerSlider.value = curPower/maxPower * 100f;
    }

    private void OnDisable()
    {
        PlayerDamageable.OnTakeDamage -= UpdateHp;
        PlayerDamageable.OnDie -= ZeroHp;
        PlayerDamageable.UseMana -= UdpateMp;
        PlayerDamageable.UsePower -= UpdatePower;
        PlayerDamageable.LevelUp -= SetStartValues;
        PlayerDamageable.AtributeChanged -= SetStartValues;
        PlayerDamageable.RegenUpdate -= UpdateHp;
        PlayerDamageable.RegenUpdate -= UpdatePower;
        PlayerDamageable.RegenUpdate -= UpdateEnemyHP;
        buttonClicked -= PlayClickAudio;
    }

    private void NewEnemy()
    {
        if(enemyPanel !=null)
            enemyPanel.SetActive(true);

        if(_currentTargetDamageable != null)
        {
            _currentTargetDamageable.OnTakeDamage -= UpdateEnemyHP;
            _currentTargetDamageable.RegenUpdate -= UpdateEnemyHP;
            _currentTargetDamageable.RegenUpdate -= UpdateEnemyPower;
            _currentTargetDamageable.UsePower -= UpdateEnemyPower;
        }
        _currentTargetDamageable = PlayerTargeter.CurrentTarget.GetComponent<Damageable>();
        UpdateEnemyHP();
        _currentTargetDamageable.OnTakeDamage += UpdateEnemyHP;
        _currentTargetDamageable.RegenUpdate += UpdateEnemyHP;
        _currentTargetDamageable.RegenUpdate += UpdateEnemyPower;
        _currentTargetDamageable.UsePower += UpdateEnemyPower;
    }

    private void UpdateEnemyHP()
    {
        if(_currentTargetDamageable != null)
        {
            float curEnemyHP = _currentTargetDamageable.Health();
            float maxEnemyHP = _currentTargetDamageable.MaxHealth();
            EnemyHpSlider.value = curEnemyHP / maxEnemyHP * 100f;
            EnemyLevel.text = _currentTargetDamageable.GetComponent<EnemyStateMachine>().EnemyStats.EnemyLevel.ToString();
            EnemyName.text = _currentTargetDamageable.GetComponent<EnemyStateMachine>().EnemyStats.EnemyName;
        }

    }
    private void UpdateEnemyPower()
    {
        if(_currentTargetDamageable != null)
        {
            float curEnemyPower = _currentTargetDamageable.Power();
            float maxEnemyPower = _currentTargetDamageable.MaxPower();
            EnemyPowerSlider.value = curEnemyPower / maxEnemyPower * 100f;
        }

    }
    private void HideEnemyPanel()
    {
        if(enemyPanel!=null)
            enemyPanel.SetActive(false);
        _currentTargetDamageable.OnTakeDamage -= UpdateEnemyHP;
        _currentTargetDamageable.RegenUpdate -= UpdateEnemyHP;
        _currentTargetDamageable.RegenUpdate -= UpdateEnemyPower;
        _currentTargetDamageable.UsePower -= UpdateEnemyPower;
    }

    private void NewBoss()
    {
        
    }

    private void NewNeutral()
    {
        
    }

    //Based on the weapon show hide correct Special Skill Slots Bar


    public void ChangeActiveWeaponToShield()
    {
        ChangeWeaponSpecialKit(1);
        HUDWeaponIcon.ChangeActiveWeaponToShield();
    }
    public void ChangeActiveWeaponToTwoHanded()
    {
        ChangeWeaponSpecialKit(2);
        HUDWeaponIcon.ChangeActiveWeaponToTwoHanded();
    }

    public void ChangeActiveWeaponToSpear()
    {
        ChangeWeaponSpecialKit(3);
        HUDWeaponIcon.ChangeActiveWeaponToSpear();
    }
    public void ChangeWeaponSpecialKit(int weaponKit)
    {
        switch (weaponKit)
        {
            case 1:
                //oneHandedShieldAttackBars.transform.parent.gameObject.SetActive(true);
                oneHandedShieldAttackBars.SetActive((true));
                SetFirstAttackBar(oneHandedShieldAttackBars);
                twoHandedAttackBars.SetActive((false));
                spearAttackBars.SetActive((false));
                oneHandShieldSkillBar.SetActive(true);
                twoHandedSkillBar.SetActive(false);
                spearSkillBar.SetActive(false);
                break;
            case 2:
                twoHandedAttackBars.SetActive((true));
                SetFirstAttackBar(twoHandedAttackBars);
                oneHandedShieldAttackBars.SetActive((false));
                spearAttackBars.SetActive((false));
                twoHandedSkillBar.SetActive(true);
                oneHandShieldSkillBar.SetActive(false);
                spearSkillBar.SetActive(false);
                break;
            case 3:
                spearAttackBars.SetActive((true));
                SetFirstAttackBar(spearAttackBars);
                oneHandedShieldAttackBars.SetActive((false));
                twoHandedAttackBars.SetActive((false));
                spearSkillBar.SetActive(true);
                oneHandShieldSkillBar.SetActive(false);
                twoHandedSkillBar.SetActive(false);
                break;
            default:
                break;
        }
    }

    private void SetFirstAttackBar(GameObject weaponKit)
    {   
        weaponKit.transform.GetChild(0).transform.gameObject.SetActive(false);
        weaponKit.transform.GetChild(1).transform.gameObject.SetActive(false);
        weaponKit.transform.GetChild(2).transform.gameObject.SetActive(false);
        weaponKit.transform.GetChild(0).transform.gameObject.SetActive(true);
        _currentAttackBar = weaponKit;
        _attackBarIndexer = 0;
        weaponKit.transform.GetChild(1).transform.gameObject.SetActive(false);
        weaponKit.transform.GetChild(2).transform.gameObject.SetActive(false);
    }
    // Arrow change which attack bar for weapon will be active (arrow up - true, arrow down - false)
    public void ChangeAttacksKit(bool increment)
    {
        buttonClicked?.Invoke();
        if (increment)
        {
            if (_attackBarIndexer == 2)
            {
                _currentAttackBar.transform.GetChild(0).transform.gameObject.SetActive((true));
                _currentAttackBar.transform.GetChild(2).transform.gameObject.SetActive(false);
                _attackBarIndexer = 0;
            }
            else
            {
                _currentAttackBar.transform.GetChild(_attackBarIndexer +1).transform.gameObject.SetActive((true));
                _currentAttackBar.transform.GetChild(_attackBarIndexer).transform.gameObject.SetActive(false);
                _attackBarIndexer++;
            }
        }
        else
        {
            if (_attackBarIndexer == 0)
            {
                _currentAttackBar.transform.GetChild(2).transform.gameObject.SetActive((true));
                _currentAttackBar.transform.GetChild(0).transform.gameObject.SetActive(false);
                _attackBarIndexer = 2;
            }
            else
            {
                _currentAttackBar.transform.GetChild(_attackBarIndexer - 1).transform.gameObject.SetActive((true));
                _currentAttackBar.transform.GetChild(_attackBarIndexer).transform.gameObject.SetActive(false);
                _attackBarIndexer--;
            }
        }

        ReCalculateAttacks();
    }
    
    public void ReCalculateAttacks()
    {
        attackUpdate?.Invoke();
    }


    private void PlayClickAudio()
    {
        buttonClick.Play();
    }
}
}

