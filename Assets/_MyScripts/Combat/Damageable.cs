using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NGS.ExtendableSaveSystem;

namespace _MyScripts.Combat
{
    public class Damageable : MonoBehaviour, ISavableComponent
{
    public bool blocking;
    private bool _isInvurneable;
    private int _damage;
    private int _blockResource;
    private int _blockValue;
    private float _knockback;
    private float _blockDamageFactor;
    private float _directionDamageFactor;
    private bool _isDead;
    private bool _inComabt;
    public bool InCombat
    {
        get { return _inComabt; }
        set { _inComabt = value; }
    }
    [SerializeField] private int maxHealth = 100;
    public int MaxHealth() => maxHealth;
    private int _health;
    public int Health() => _health;
    [SerializeField] private int maxMana = 20;
    public int MaxMana() => maxMana;
    private int _mana;
    public int Mana() => _mana;
    [SerializeField] private int maxPower = 50;
    public int MaxPower() => maxPower;
    private int _power;
    public int Power() => _power;

    public event Action OnTakeDamage;
    public event Action OnDie;
    public event Action SuccesfulBlock;
    public event Action RecoilWeapon;
    public event Action SuccesfulAttack;
    public event Action UseMana;
    public event Action UsePower;
    public event Action LevelUp;
    public event Action AtributeChanged;
    public event Action RegenUpdate;
    public Action InBattle;
    public Action OutOfBattle;
    
    public bool IsDead => _health == 0;
    
    [SerializeField] private int _uniqueID;
    [SerializeField] private int _executionOrder;
    public int uniqueID => _uniqueID;
    public int executionOrder => _executionOrder;
    private void Awake()
    {
        SetStartHealth();
        SetStartMana();
        SetStartPower();
    }

    private void SetStartHealth()
    {
        _health = maxHealth;
    }

    private void SetStartMana()
    {
        _mana = maxMana;
    }

    private void SetStartPower()
    {
        _power = maxPower;
    }
    public void IncreaseMaxMana()
    {
        maxMana += 1;
    }
    public void IncreaseMaxPower()
    {
        maxPower += 3;
    }
    public void IncreaseMaxHealth()
    {
        maxHealth += 8;
    }

    public void ManaCost(int amount)
    {
        _mana -= amount;
        UseMana?.Invoke();
    }

    public void PowerCost(int amount)
    {
        if(_power - amount > 0)
            _power -= amount;
        else
        {
            _power = 0;
        }
        UsePower?.Invoke();
    }

    public void RegenerationTime(int healthRegen, int manaRegen, int powerRegen)
    {
        if (_health + healthRegen > maxHealth)
        {
            _health = maxHealth;
        }
        else
        {
            _health += healthRegen;
        }

        if (_mana + manaRegen > maxMana)
        {
            _mana = maxMana;
        }
        else
        {
            _mana += manaRegen;
        }

        if (_power + powerRegen > maxPower)
        {
            _power = maxPower;
        }
        else
        {
            _power += powerRegen;
        }
        RegenUpdate?.Invoke();
        // Update HUD
    }
    public void DoneBlock()
    {
        SuccesfulBlock?.Invoke();
    }
    public void AttackBlocked()
    {
        RecoilWeapon?.Invoke();
    }
    public void AttackSuccesful()
    {
        SuccesfulAttack?.Invoke();
    }
    public void SetInvulnerable(bool isInvurneable)
    {
        this._isInvurneable = isInvurneable;
    }
    public void RemoveInvulnerable()
    {
        _isInvurneable = false;
    }
    public void SetBlocking()
    {
        blocking = true;
    }
    public void RemoveBlocking()
    {
        blocking = false;
    }

    public void CalculateDamage(int damage, float knockback, Transform attacker)
    {
        Vector3 diretcionToAttacker = (this.transform.position - attacker.position).normalized;
        Vector3 directionFromAttacker = (attacker.position - this.transform.position).normalized;
        float dotProd = Vector3.Dot(diretcionToAttacker, this.transform.forward);
        
        if(_isInvurneable)
        {

        }
        else 
        {
            DealDamage(damage);
            if(this.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))    
            {
                forceReceiver.AddForce(diretcionToAttacker * knockback);
            }
        }

    }

    public void DealDamage(int damageValue)
    {
        if(_health <= 0) {return;}
        else
        {
            _health = Mathf.Max(_health - damageValue, 0);
            OnTakeDamage?.Invoke();
        }


        if(_health == 0)
        {
            OnDie?.Invoke();
        }
    }


    public ComponentData Serialize()
    {
        ExtendedComponentData data = new ExtendedComponentData();
        data.SetTransform("transform", transform);
        data.SetInt("health", _health);
        data.SetInt("power", _power);
        data.SetInt("mana", _mana);
        return data;
    }

    public void Deserialize(ComponentData data)
    {
        ExtendedComponentData unPacked = (ExtendedComponentData)data;
        unPacked.GetTransform("transform", transform);
        _health = unPacked.GetInt("health");
        _power = unPacked.GetInt("power");
        _mana = unPacked.GetInt("mana");
    }
}

}
