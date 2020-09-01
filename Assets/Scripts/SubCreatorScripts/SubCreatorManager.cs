using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SubBaseType
{
    None,
    Medium,
    Heavy,
    Light
}

public enum SubEngineType
{
    None,
    Light,
    Medium,
    Heavy
}

public enum SubCannonType
{
    None,
    Torpedo,
    Minigun,
    Ram
}

public enum SubSpecialType
{
    None
}

public class SubCreatorManager : SingetonMonobehaviour<SubCreatorManager>
{
    [Header("Base prefabs")]
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

    private void Start()
    {
        CreateSub(SubBaseType.Light, SubEngineType.Medium, SubCannonType.Minigun, SubSpecialType.None);
    }

    public GameObject CreateSub(SubBaseType _base, SubEngineType _engine, SubCannonType _cannon, SubSpecialType _special)
    {
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

        SubBaseBehaviour subBehaviour = _tempBase.GetComponent<SubBaseBehaviour>();
        Debug.Log(subBehaviour);

        switch (_engine)
        {
            case SubEngineType.None:
                break;
            case SubEngineType.Light:
                _tempEngine = Instantiate(lightEngine, subBehaviour.GetEnginePlace.transform.position, Quaternion.identity, _tempBase.transform);
                break;
            case SubEngineType.Medium:
                _tempEngine = Instantiate(mediumEngine, subBehaviour.GetEnginePlace.transform.position, Quaternion.identity, _tempBase.transform);
                break;
            case SubEngineType.Heavy:
                _tempEngine = Instantiate(heavyEngine, subBehaviour.GetEnginePlace.transform.position, Quaternion.identity, _tempBase.transform);
                break;
            default:
                break;
        }

        switch (_cannon)
        {
            case SubCannonType.None:
                break;
            case SubCannonType.Torpedo:
                _tempCannon = Instantiate(torpedoCannon, subBehaviour.GetCannonPlace.transform.position, Quaternion.identity, _tempBase.transform);
                break;
            case SubCannonType.Minigun:
                _tempCannon = Instantiate(minigunCannon, subBehaviour.GetCannonPlace.transform.position, Quaternion.identity, _tempBase.transform);
                break;
            case SubCannonType.Ram:
                _tempCannon = Instantiate(ramCannon, subBehaviour.GetCannonPlace.transform.position, Quaternion.identity, _tempBase.transform);
                break;
            default:
                break;
        }
        subBehaviour.CannonObject = _tempCannon;
        subBehaviour.EngineObject = _tempEngine;

        return subBehaviour.gameObject;
    }
}
