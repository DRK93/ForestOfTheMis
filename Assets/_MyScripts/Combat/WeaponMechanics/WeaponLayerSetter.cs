using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.Combat.WeaponMechanics
{
    public class WeaponLayerSetter : MonoBehaviour
{
    [SerializeField] private WeaponStatsScriptableObject _weaponData;
    public WeaponStatsScriptableObject WeaponData
    {
        get { return _weaponData; }
    }
    private Transform _parentTransform;
    private Damageable _parentDamageable;
    private WeaponHandlerer _weaponHandlerer;
    private bool _initDone;
    private bool _startDone;
    public Transform WeaponParentTransform
    {
        get => _parentTransform;
        private set => _parentTransform = value;
    }
    public Damageable WeaponParentDamageable
    {
        get => _parentDamageable;
        private set => _parentDamageable = value;
    }
    
    void Start()
    {
        SearchForParentDamageable(transform.gameObject);
    }

    private void OnEnable()
    {
        if (_initDone)
        {
            StartCoroutine(DelayedCheck());
        }
        _initDone = false;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        if(_startDone)
            if(!_initDone)
            {
                _initDone = true;
            }
    }

    private void SearchForParentDamageable(GameObject checkObject)
    {
        if (checkObject.transform.parent.TryGetComponent<Damageable>(out Damageable damageable))
        {
            _parentDamageable = damageable;
            _parentTransform = damageable.transform;
            _weaponHandlerer = damageable.transform.GetComponent<WeaponHandlerer>();
            // Setting correct LayerMask for weapon
            //if (_parentTransform.gameObject.layer == LayerMask.GetMask("Player"))
            if (_parentTransform.gameObject.layer == 7)
            {
                this.transform.gameObject.layer = 10;
            }
            else if (_parentTransform.gameObject.layer == 8)
            {
                this.transform.gameObject.layer = 12;
            }
            // add to player weapon Manager
                /*if (transform.parent.TryGetComponent<SecondWeapon>(out SecondWeapon secondWeapon))
                {
                    _weaponHandlerer.SecondWeaponLogic = this.gameObject;
                    //_parentTransform.gameObject.GetComponent<WeaponManager>().SetWeaponObject(weaponData, true, this.transform.gameObject);
                }
                else
                {
                    _weaponHandlerer.MainWeaponLogic = this.gameObject;
                    //_parentTransform.gameObject.GetComponent<WeaponManager>().SetWeaponObject(weaponData, false, this.transform.gameObject);
                }*/
            //else if (_parentTransform.gameObject.layer == LayerMask.GetMask("Enemy"))
            if (!_startDone)
            {
                if (transform.parent.TryGetComponent<SecondWeapon>(out SecondWeapon secondWeapon))
                {
                    _weaponHandlerer.SecondWeaponLogic = this.gameObject;
                }
                else
                {
                    _weaponHandlerer.MainWeaponLogic = this.gameObject;
                }
                _startDone = true;
            }
        }
        else
        {
            SearchForParentDamageable(checkObject.transform.parent.gameObject);
        }
    }

    private IEnumerator DelayedCheck()
    {
        yield return new WaitForSeconds(0.2f);
        if (transform.parent.TryGetComponent<SecondWeapon>(out SecondWeapon secondWeapon))
        {
            _weaponHandlerer.SecondWeaponLogic = this.transform.gameObject;
        }
        else
        {
            _weaponHandlerer.MainWeaponLogic = this.transform.gameObject;
        }
    }
}
}

