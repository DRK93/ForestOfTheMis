using System;
using System.Collections;
using System.Collections.Generic;
using _MyScripts.HUD;
using UnityEngine;
using UnityEngine.Serialization;
using NGS.ExtendableSaveSystem;

namespace _MyScripts.Combat.WeaponMechanics
{
    public class WeaponManager : MonoBehaviour, ISavableComponent
{
    [SerializeField] private HUDManager hudDManager;
    [SerializeField] private List<GameObject> mainOneHandSwordGameObjectList;
    [SerializeField] private List<GameObject> mainOneHandAxeGameObjectList;
    [SerializeField] private List<GameObject> mainOneHandMaceGameObjectList;
    [SerializeField] private List<GameObject> twoHandSwordGameObjectList;
    [SerializeField] private List<GameObject> twoHandAxeGameObjectList;
    [SerializeField] private List<GameObject> twoHandMaceGameObjectList;
    [SerializeField] private List<GameObject> spearGameObjectList;
    [SerializeField] private List<GameObject> halberdGameObjectList;
    [SerializeField] private List<GameObject> shieldGameObjectList;
    [SerializeField] private List<GameObject> bowGameObjectList;
    [SerializeField] private List<GameObject> secondHandSwordGameObjectList;
    [SerializeField] private List<GameObject> secondHandAxeGameObjectList;
    [SerializeField] private List<GameObject> secondHandMaceGameObjectList;

    
    [SerializeField] private WeaponStatsScriptableObject temporalOneHandSword;
    [SerializeField] private WeaponStatsScriptableObject temporalShied;
    [SerializeField] private WeaponStatsScriptableObject temporalTwoHandedSword;
    [SerializeField] private WeaponStatsScriptableObject temporalSpear;
    
    private bool _twoHanded;
    private bool _secondWeaponAllowed;
    private int _mainWeaponGenreNumber;
    private int _secondWeaponGenreNumber;
    private int _mainWeaponNumber;
    private int _secondWeaponNumber;

    public Action SetSpaerBlocking;
    public Action SetShieldBlocking;
    public Action SetTwoHandedBlocking;
    [SerializeField] private int _uniqueID;
    [SerializeField] private int _executionOrder;

    public void TemporalSetWeapon(int weaponKitNumber)
    {
        switch (weaponKitNumber)
        {
            case 1:
                UseWeapon(temporalOneHandSword, true);
                break;
            case 2:
                UseWeapon(temporalTwoHandedSword, true);
                
                break;
            case 3:
                UseWeapon(temporalSpear, true);
                
                break;
            case 4:
                _secondWeaponAllowed = true;
                UseWeapon(temporalShied, false);
                
                break;
            default:
                break;
        }
    }
    public void SetWeaponObject(WeaponStatsScriptableObject weaponData, bool rightHand, GameObject weaponObject)
    {
        if(weaponData.ThisWeaponType == WeaponStatsScriptableObject.WeaponType.OneHandedSword)
        {
            if (rightHand)
                secondHandSwordGameObjectList.Add(weaponObject);
            else
                mainOneHandSwordGameObjectList.Add(weaponObject);
        }

        if(weaponData.ThisWeaponType == WeaponStatsScriptableObject.WeaponType.OneHandedAxe)
        {
            if (rightHand)
                secondHandAxeGameObjectList.Add(weaponObject);
            else
                mainOneHandAxeGameObjectList.Add(weaponObject);
        }

        if(weaponData.ThisWeaponType == WeaponStatsScriptableObject.WeaponType.OneHandedMace)
        {
            if(rightHand)
                secondHandMaceGameObjectList.Add(weaponObject);
            else
                mainOneHandMaceGameObjectList.Add(weaponObject);
        }

        if(weaponData.ThisWeaponType == WeaponStatsScriptableObject.WeaponType.Shield)
        {
            shieldGameObjectList.Add(weaponObject);
        }
        if(weaponData.ThisWeaponType == WeaponStatsScriptableObject.WeaponType.TwoHandedSword)   
        {
            twoHandSwordGameObjectList.Add(weaponObject);
        }

        if(weaponData.ThisWeaponType == WeaponStatsScriptableObject.WeaponType.TwoHandedMace)
        {
            twoHandMaceGameObjectList.Add(gameObject);
        }

        if(weaponData.ThisWeaponType == WeaponStatsScriptableObject.WeaponType.TwoHandedAxe)
        {
            twoHandAxeGameObjectList.Add(gameObject);
        }

        if(weaponData.ThisWeaponType == WeaponStatsScriptableObject.WeaponType.Spear)
        {
            spearGameObjectList.Add(weaponObject);
        }

        if(weaponData.ThisWeaponType == WeaponStatsScriptableObject.WeaponType.Halberd)
        {
            halberdGameObjectList.Add(weaponObject);
        }

        if(weaponData.ThisWeaponType == WeaponStatsScriptableObject.WeaponType.Bow)
        {
            bowGameObjectList.Add(weaponObject);
        }
    }

    private void UseWeapon(WeaponStatsScriptableObject weaponData, bool rightHand)
    {
        _secondWeaponAllowed = false;
        if(weaponData.ThisWeaponType == WeaponStatsScriptableObject.WeaponType.OneHandedSword)
        {
            if (!rightHand)
            {
                MainWeaponCheck();
                if (_secondWeaponAllowed)
                {
                    WeaponCollectionBrowse(weaponData.WeaponName, secondHandSwordGameObjectList, false, _secondWeaponNumber);
                    _secondWeaponGenreNumber = 1;
                }
                    
            }
            else
            {
                _twoHanded = false;
                WeaponCollectionBrowse(weaponData.WeaponName, mainOneHandSwordGameObjectList, false, _mainWeaponNumber);
                _mainWeaponGenreNumber = 1;
            }
            // temporary, until there are no only one hand and dual weapon animation sets
            hudDManager.ChangeActiveWeaponToShield();
        }

        if(weaponData.ThisWeaponType == WeaponStatsScriptableObject.WeaponType.OneHandedAxe)
        {
            if (!rightHand)
            {
                MainWeaponCheck();
                if (_secondWeaponAllowed)
                {
                    WeaponCollectionBrowse(weaponData.WeaponName, secondHandAxeGameObjectList, false, _secondWeaponNumber);
                    _secondWeaponGenreNumber = 2;
                }
            }
            else
            {
                _twoHanded = false;
                WeaponCollectionBrowse(weaponData.WeaponName, mainOneHandAxeGameObjectList, false, _mainWeaponNumber);
                _mainWeaponGenreNumber = 2;
            }
            // temporary, until there are no only one hand and dual weapon animation sets
            hudDManager.ChangeActiveWeaponToShield();
        }

        if(weaponData.ThisWeaponType == WeaponStatsScriptableObject.WeaponType.OneHandedMace)
        {
            if (!rightHand)
            {
                MainWeaponCheck();
                if (_secondWeaponAllowed)
                {
                    WeaponCollectionBrowse(weaponData.WeaponName, secondHandMaceGameObjectList, false, _secondWeaponNumber );
                    _secondWeaponGenreNumber = 3;
                }
            }
            else
            {
                _twoHanded = false;
                WeaponCollectionBrowse(weaponData.WeaponName, mainOneHandMaceGameObjectList, false, _mainWeaponNumber);
                _mainWeaponGenreNumber = 3;
            }
            // temporary, until there are no only one hand and dual weapon animation sets
            hudDManager.ChangeActiveWeaponToShield();
        }

        if(weaponData.ThisWeaponType == WeaponStatsScriptableObject.WeaponType.Shield)
        {
            MainWeaponCheck();
            if (_secondWeaponAllowed)
            {
                WeaponCollectionBrowse(weaponData.WeaponName, shieldGameObjectList, false, _secondWeaponNumber);
                _secondWeaponGenreNumber = 4;
            }
            hudDManager.ChangeActiveWeaponToShield();
            SetShieldBlocking?.Invoke();
        }
        if(weaponData.ThisWeaponType == WeaponStatsScriptableObject.WeaponType.TwoHandedSword)   
        {
            WeaponCollectionBrowse(weaponData.WeaponName, twoHandSwordGameObjectList, true, _mainWeaponNumber);
            hudDManager.ChangeActiveWeaponToTwoHanded();
            _mainWeaponGenreNumber = 5;
            _secondWeaponGenreNumber = 0;
            SetTwoHandedBlocking?.Invoke();
        }

        if(weaponData.ThisWeaponType == WeaponStatsScriptableObject.WeaponType.TwoHandedMace)
        {
            WeaponCollectionBrowse(weaponData.WeaponName, twoHandMaceGameObjectList, true, _mainWeaponNumber);
            hudDManager.ChangeActiveWeaponToTwoHanded();
            _mainWeaponGenreNumber = 6;
            _secondWeaponGenreNumber = 0;
            SetTwoHandedBlocking?.Invoke();
        }

        if(weaponData.ThisWeaponType == WeaponStatsScriptableObject.WeaponType.TwoHandedAxe)
        {
            WeaponCollectionBrowse(weaponData.WeaponName, twoHandAxeGameObjectList, true, _mainWeaponNumber);
            hudDManager.ChangeActiveWeaponToTwoHanded();
            _mainWeaponGenreNumber = 7;
            _secondWeaponGenreNumber = 0;
            SetTwoHandedBlocking?.Invoke();
        }

        if(weaponData.ThisWeaponType == WeaponStatsScriptableObject.WeaponType.Spear)
        {
            WeaponCollectionBrowse(weaponData.WeaponName, spearGameObjectList, true, _mainWeaponNumber);
            hudDManager.ChangeActiveWeaponToSpear();
            _mainWeaponGenreNumber = 8;
            _secondWeaponGenreNumber = 0;
            SetSpaerBlocking?.Invoke();
        }

        if(weaponData.ThisWeaponType == WeaponStatsScriptableObject.WeaponType.Halberd)
        {
            WeaponCollectionBrowse(weaponData.WeaponName, halberdGameObjectList, true, _mainWeaponNumber);
            hudDManager.ChangeActiveWeaponToSpear();
            _mainWeaponGenreNumber = 9;
            _secondWeaponGenreNumber = 0;
            SetSpaerBlocking?.Invoke();
        }

        if(weaponData.ThisWeaponType == WeaponStatsScriptableObject.WeaponType.Bow)
        {
            WeaponCollectionBrowse(weaponData.WeaponName, bowGameObjectList, true, _mainWeaponNumber);
            _mainWeaponGenreNumber = 10;
            _secondWeaponGenreNumber = 0;
        }

    }
    private void WeaponCollectionBrowse(string weaponName, List<GameObject> weaponCollection, bool twoHanded, int weaponNumber)
    {
        if (twoHanded)
        {
            _twoHanded = true;
        }
        else
        {
            _twoHanded = false;
        }

        int indexer = 0;
        
        foreach (var weaponObject in weaponCollection)
        {
            if (weaponName == weaponObject.name)
            {
                weaponObject.SetActive(true);
                weaponNumber = indexer;
            }

            indexer++;
        }
    }
    private void MainWeaponCheck()
    {
        if (_twoHanded)
        {
            ShowMainNotification();
        }
        else
        {
            _secondWeaponAllowed = true;
        }
    }

    private void ShowMainNotification()
    {
        Debug.Log("second weapon not allowed");
        // notification window to not equip second weapon if main weapon is two handed or null, first change main weapon for one handed 
    }

    public int uniqueID => _uniqueID;

    public int executionOrder => _executionOrder;

    public ComponentData Serialize()
    {
        ExtendedComponentData data = new ExtendedComponentData();
        data.SetInt("mainWeaponGenre", _mainWeaponGenreNumber);
        data.SetInt("mainWeaponNumber", _mainWeaponNumber);
        data.SetInt("secondWeaponGenre", _secondWeaponGenreNumber);
        data.SetInt("secondWeaponNumber", _secondWeaponNumber);
        return data;
    }

    public void Deserialize(ComponentData data)
    {
        ExtendedComponentData unPacked = (ExtendedComponentData)data;
        _mainWeaponGenreNumber = unPacked.GetInt("mainWeaponGenre");
        _mainWeaponNumber = unPacked.GetInt("mainWeaponNumber");
        _secondWeaponGenreNumber = unPacked.GetInt("secondWeaponGenre");
        _secondWeaponNumber = unPacked.GetInt("secondWeaponNumber");

        StartCoroutine(WeaponAfterLoad());
    }

    private IEnumerator WeaponAfterLoad()
    {
        yield return new WaitForSeconds(0.2f);
        SetWeaponAfterLoad();
    }
    private void SetWeaponAfterLoad()
    {
        switch (_mainWeaponGenreNumber)
        {
            case 1:
                mainOneHandSwordGameObjectList[_mainWeaponNumber].SetActive(true);
                break;
            case 2:
                mainOneHandAxeGameObjectList[_mainWeaponNumber].SetActive(true);
                break;
            case 3:
                mainOneHandMaceGameObjectList[_mainWeaponNumber].SetActive(true);
                break;
            case 4:
                //shield number
                break;
            case 5:
                twoHandSwordGameObjectList[_mainWeaponNumber].SetActive(true);
                SetTwoHandedBlocking?.Invoke();
                break;
            case 6:
                twoHandMaceGameObjectList[_mainWeaponNumber].SetActive(true);
                SetTwoHandedBlocking?.Invoke();
                break;
            case 7:
                twoHandAxeGameObjectList[_mainWeaponNumber].SetActive(true);
                SetTwoHandedBlocking?.Invoke();
                break;
            case 8:
                spearGameObjectList[_mainWeaponNumber].SetActive(true);
                SetSpaerBlocking?.Invoke();
                break;
            case 9:
                halberdGameObjectList[_mainWeaponNumber].SetActive(true);
                SetSpaerBlocking?.Invoke();
                break;
            case 10:
                // no bows yet
                //bowGameObjectList[_mainWeaponNumber].SetActive(true);
                break;
            default:
                break;
        }
        switch (_secondWeaponGenreNumber)
        {
            case 1:
                secondHandSwordGameObjectList[_secondWeaponNumber].SetActive(true);
                break;
            case 2:
                secondHandAxeGameObjectList[_secondWeaponNumber].SetActive(true);
                break;
            case 3:
                secondHandMaceGameObjectList[_secondWeaponNumber].SetActive(true);
                break;
            case 4:
                shieldGameObjectList[_secondWeaponNumber].SetActive(true);
                SetShieldBlocking?.Invoke();
                break;
            default:
                break;
        }
    }
}
}

