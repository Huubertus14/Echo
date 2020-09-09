using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;



public class SubCreatorManager : SingetonMonobehaviour<SubCreatorManager>
{
    [Header("Managed Refs:")]
    [SerializeField] private GameObject currentSub;
    private SubBaseBehaviour currentSubBehaviour;

    private SubBaseType baseType = SubBaseType.None;
    private SubEngineType engineType = SubEngineType.None;
    private SubCannonType cannonType = SubCannonType.None;
    private SubSpecialType specialType = SubSpecialType.None;

    [Header("Base prefabs:")]
    [SerializeField] private GameObject heavyBase;
    [SerializeField] private GameObject lightBase;
    [SerializeField] private GameObject mediumBase;


    [Header("Engine prefabs:")]
    [SerializeField] private GameObject heavyEngine;
    [SerializeField] private GameObject mediumEngine;
    [SerializeField] private GameObject lightEngine;

    [Header("Cannon Prefabs:")]
    [SerializeField] private GameObject torpedoCannon;
    [SerializeField] private GameObject minigunCannon;
    [SerializeField] private GameObject ramCannon;
    /*
        [Header("Special Prefabs:")]
        public GameObject temp;*/


    public void CreateSubOnBoot()
    {
        baseType = (SubBaseType)GameManager.SP.playerData.subBaseSelected;
        engineType = (SubEngineType)GameManager.SP.playerData.subEngineSelected;
        cannonType = (SubCannonType)GameManager.SP.playerData.subCannonSelected;
        specialType = (SubSpecialType)GameManager.SP.playerData.subSpecialSelected;

        currentSub = CreateSub(baseType, engineType, cannonType, specialType);
        SetMeshPosition(new Vector3(5.6f, 3.1f, 0));

        DontDestroyOnLoad(currentSub);
    }

    public GameObject CreateSub(SubBaseType _base, SubEngineType _engine, SubCannonType _cannon, SubSpecialType _special)
    {
        baseType = _base;
        engineType = _engine;
        cannonType = _cannon;
        specialType = _special;

        GameObject _tempBase = null;
        GameObject _tempEngine = null;
        GameObject _tempCannon = null;

        switch (_base)
        {
            case SubBaseType.None:
                Debug.LogError("No base selected");

                break;
            case SubBaseType.Medium:
                _tempBase = Instantiate(mediumBase, transform.position, Quaternion.identity);
                break;
            case SubBaseType.Heavy:
                _tempBase = Instantiate(heavyBase, transform.position, Quaternion.identity);
                break;
            case SubBaseType.Light:
                _tempBase = Instantiate(lightBase, transform.position, Quaternion.identity);
                break;
            default:
                break;
        }

        currentSubBehaviour = _tempBase.GetComponent<SubBaseBehaviour>();

        switch (_engine)
        {
            case SubEngineType.None:
                break;
            case SubEngineType.Light:
                _tempEngine = Instantiate(lightEngine, currentSubBehaviour.GetEnginePlace.transform.position, Quaternion.identity, _tempBase.transform);
                break;
            case SubEngineType.Medium:
                _tempEngine = Instantiate(mediumEngine, currentSubBehaviour.GetEnginePlace.transform.position, Quaternion.identity, _tempBase.transform);
                break;
            case SubEngineType.Heavy:
                _tempEngine = Instantiate(heavyEngine, currentSubBehaviour.GetEnginePlace.transform.position, Quaternion.identity, _tempBase.transform);
                break;
            default:
                break;
        }

        switch (_cannon)
        {
            case SubCannonType.None:
                break;
            case SubCannonType.Torpedo:
                _tempCannon = Instantiate(torpedoCannon, currentSubBehaviour.GetCannonPlace.transform.position, Quaternion.identity, _tempBase.transform);
                break;
            case SubCannonType.Minigun:
                _tempCannon = Instantiate(minigunCannon, currentSubBehaviour.GetCannonPlace.transform.position, Quaternion.identity, _tempBase.transform);
                break;
            case SubCannonType.Ram:
                _tempCannon = Instantiate(ramCannon, currentSubBehaviour.GetCannonPlace.transform.position, Quaternion.identity, _tempBase.transform);
                break;
            default:
                break;
        }
        currentSubBehaviour.CannonObject = _tempCannon;
        currentSubBehaviour.EngineObject = _tempEngine;

        //_tempBase.transform.position = new Vector3(-4.55f,0.25f,-0.4f);
        currentSubBehaviour.transform.Rotate(90,0,0);
        return currentSubBehaviour.gameObject;
    }

    public void ChangeComponent(SubBaseType _baseType)
    {
        //Destroy Sub
        Destroy(currentSub.gameObject);
        baseType = _baseType;
        //Create new sub
        currentSub = CreateSub(_baseType, engineType, cannonType, specialType);
        SetMeshPosition(new Vector3(-4.55f, 0.25f, -0.4f));
    }

    public void ChangeComponent(SubEngineType _engineType)
    {
        GameObject _tempEngine = null;
        Destroy(currentSubBehaviour.EngineObject.gameObject);
        engineType = _engineType;
        switch (_engineType)
        {
            case SubEngineType.None:
                break;
            case SubEngineType.Light:
                _tempEngine = Instantiate(lightEngine, currentSubBehaviour.GetEnginePlace.transform.position, Quaternion.identity, currentSub.transform);
                break;
            case SubEngineType.Medium:
                _tempEngine = Instantiate(mediumEngine, currentSubBehaviour.GetEnginePlace.transform.position, Quaternion.identity, currentSub.transform);
                break;
            case SubEngineType.Heavy:
                _tempEngine = Instantiate(heavyEngine, currentSubBehaviour.GetEnginePlace.transform.position, Quaternion.identity, currentSub.transform);
                break;
            default:
                break;
        }
        currentSubBehaviour.EngineObject = _tempEngine;

    }

    public void ChangeComponent(SubCannonType _cannonType)
    {
        GameObject _tempCannon = null;
        Destroy(currentSubBehaviour.CannonObject.gameObject);
        cannonType = _cannonType;
        switch (_cannonType)
        {
            case SubCannonType.None:
                break;
            case SubCannonType.Torpedo:
                _tempCannon = Instantiate(torpedoCannon, currentSubBehaviour.GetCannonPlace.transform.position, Quaternion.identity, currentSub.transform);
                break;
            case SubCannonType.Minigun:
                _tempCannon = Instantiate(minigunCannon, currentSubBehaviour.GetCannonPlace.transform.position, Quaternion.identity, currentSub.transform);
                break;
            case SubCannonType.Ram:
                _tempCannon = Instantiate(ramCannon, currentSubBehaviour.GetCannonPlace.transform.position, Quaternion.identity, currentSub.transform);
                break;
            default:
                break;
        }

        currentSubBehaviour.CannonObject = _tempCannon;
    }

    public void ChangeComponent(SubSpecialType _specialType)
    {
        specialType = _specialType;
    }

    public void SetSubMesh(bool _value)
    {
        currentSub.gameObject.SetActive(_value);
    }

    public void SetMeshPosition(Vector3 _newPos)
    {
        currentSub.transform.position = _newPos;
    }

    public GameObject GetCurrentSub => currentSub;
}
