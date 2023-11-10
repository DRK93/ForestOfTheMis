using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NGS.ExtendableSaveSystem;
using UnityEngine.UI;
using _MyScripts.GameMenu;
using _MyScripts.StateMachines.PlayerStates;
using _MyScripts.SkillsDragDropUI;

namespace _MyScripts.HUD.HUD_Bars
{
    public class SkillBar : MonoBehaviour, ISavableComponent
{
    [SerializeField] private int _uniqueID;
    [SerializeField] private int _executionOrder;

    // get AttackItem
    [field: SerializeField] public ItemSkill SkillSlot1 { get; private set; }
    [field: SerializeField] public ItemSkill SkillSlot2 { get; private set; }
    [field: SerializeField] public ItemSkill SkillSlot3 { get; private set; }
    [field: SerializeField] public ItemSkill SkillSlot4 { get; private set; }
    [field: SerializeField] public ItemSkill SkillSlot5 { get; private set; }
    [field: SerializeField] public bool AttackBar { get; private set; }
    [field: SerializeField] private bool wasActive;
    [field: SerializeField] private bool starterBar;
    private int _slotAttackNumber1;
    private int _slotAttackNumber2;
    private int _slotAttackNumber3;
    private int _slotAttackNumber4;
    private int _slotAttackNumber5;
    public int uniqueID => _uniqueID;
    public int executionOrder => _executionOrder;
    
    private void OnEnable()
    {
        if (AttackBar)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerStateMachine>().AttackBar = this;
        }
        else
            GameObject.FindWithTag("Player").GetComponent<PlayerStateMachine>().SkillBar = this;
        wasActive = true;
    }

    private void Start()
    {
        if(!starterBar)
            gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        wasActive = false;
    }

    public ComponentData Serialize()
    {
        ExtendedComponentData data = new ExtendedComponentData();
        SetCorrectAttack();
        data.SetInt("slot1Number", _slotAttackNumber1);
        data.SetInt("slot2Number", _slotAttackNumber2);
        data.SetInt("slot3Number", _slotAttackNumber3);
        data.SetInt("slot4Number", _slotAttackNumber4);
        data.SetInt("slot5Number", _slotAttackNumber5);
        data.SetBool("wasActiveBar", wasActive);
        return data;

    }

    public void Deserialize(ComponentData data)
    {
        ExtendedComponentData unPacked = (ExtendedComponentData)data;
        _slotAttackNumber1 = unPacked.GetInt("slot1Number");
        _slotAttackNumber2 = unPacked.GetInt("slot2Number");
        _slotAttackNumber3 = unPacked.GetInt("slot3Number");
        _slotAttackNumber4 = unPacked.GetInt("slot4Number");
        _slotAttackNumber5 = unPacked.GetInt("slot5Number");
        wasActive = unPacked.GetBool("wasActiveBar");
        GetCorrectAttack();
    }

    private void SetCorrectAttack()
    {
        Debug.Log("CorrectAttack setting");
        if (SkillSlot1.attackID != 0)
        {
            Debug.Log("Test1" + SkillSlot1.attackID);
            _slotAttackNumber1 = SkillSlot1.attackID;
        }
        if (SkillSlot2.attackID != 0)
        {
            Debug.Log("Test2 " + SkillSlot2.attackID);
            _slotAttackNumber2 = SkillSlot2.attackID;
        }
        if (SkillSlot3.attackID != 0)
        {
            Debug.Log("Test3 " + SkillSlot3.attackID);
            _slotAttackNumber3 = SkillSlot3.attackID;
        }
        if (SkillSlot4.attackID != 0)
        {
            Debug.Log("Test4 " + SkillSlot4.attackID);
            _slotAttackNumber4 = SkillSlot4.attackID;
        }
        if (SkillSlot5.attackID != 0)
        {
            Debug.Log("Test5 " + SkillSlot5.attackID);
            _slotAttackNumber5 = SkillSlot5.attackID;
        }
    }
    private void GetCorrectAttack()
    {
        if (_slotAttackNumber1 != 0)
        {
            SkillSlot1.attackID = _slotAttackNumber1;
        }
        if (_slotAttackNumber2 != 0)
        {
            SkillSlot2.attackID = _slotAttackNumber2;
        }
        if (_slotAttackNumber3 != 0)
        {
            SkillSlot3.attackID = _slotAttackNumber3;
        }
        if (_slotAttackNumber4 != 0)
        {
            SkillSlot4.attackID = _slotAttackNumber4;
        }
        if (_slotAttackNumber5 != 0)
        {
            SkillSlot5.attackID = _slotAttackNumber5;
        }

        StartCoroutine(DelayAfterLoad());
    }

    private IEnumerator DelayAfterLoad()
    {
        yield return new WaitForSeconds(0.8f);
        GetFromSkillManager();
    }

    private void GetFromSkillManager()
    {
        if (_slotAttackNumber1 != 0)
        {
            SkillSlot1.SetItem(GameObject.FindGameObjectWithTag("SkillManager").GetComponent<ItemSkillManager>().FindThatSkill(_slotAttackNumber1));
            SkillSlot1.GetComponent<Image>().sprite = SkillSlot1.itemSkillIcon;
        }
        if (_slotAttackNumber2 != 0)
        {
            SkillSlot2.SetItem(GameObject.FindGameObjectWithTag("SkillManager").GetComponent<ItemSkillManager>().FindThatSkill(_slotAttackNumber2));
            SkillSlot2.GetComponent<Image>().sprite = SkillSlot2.itemSkillIcon;
        }
        if (_slotAttackNumber3 != 0)
        {
            SkillSlot3.SetItem(GameObject.FindGameObjectWithTag("SkillManager").GetComponent<ItemSkillManager>().FindThatSkill(_slotAttackNumber3));
            SkillSlot3.GetComponent<Image>().sprite = SkillSlot3.itemSkillIcon;
        }
        if (_slotAttackNumber4 != 0)
        {
            SkillSlot4.SetItem(GameObject.FindGameObjectWithTag("SkillManager").GetComponent<ItemSkillManager>().FindThatSkill(_slotAttackNumber4));
            SkillSlot4.GetComponent<Image>().sprite = SkillSlot4.itemSkillIcon;
        }
        if (_slotAttackNumber5 != 0)
        {
            SkillSlot5.SetItem(GameObject.FindGameObjectWithTag("SkillManager").GetComponent<ItemSkillManager>().FindThatSkill(_slotAttackNumber5)); ;
            SkillSlot5.GetComponent<Image>().sprite = SkillSlot5.itemSkillIcon;
        }
        HideSkillBar();
    }

    private void HideSkillBar()
    {
        if (!wasActive)
        {
            gameObject.SetActive(false);
        }
    }
}
}

