using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCreatorUIManager : MonoBehaviour
{
    [Header("Refs:")]
    [SerializeField] private GameObject subObjectParent;
    [Space]
    [SerializeField] private GameObject subBaseTemplate;

    [Header("prefabs:")]
    [SerializeField] private SubObjectData objectPrefab; //empty prefab



    [Header("Selectable Objects:")]
    [SerializeField] private SubObjectData[] subBases;
    [SerializeField] private SubObjectData[] subEngines;
    [SerializeField] private SubObjectData[] subCannons;
    [SerializeField] private SubObjectData[] subSpecial;

    private int selectedHeader = 0;//0 = base, 1 = engine, 2 = cannon, 3 = special
    private int currentSelected = 0;
    private SubObjectData[] selectedArray;

    private void Awake()
    {
        subBases = GetSubBases();
        subEngines = GetSubEngines();
        subCannons = GetSubCannons();
        subSpecial = GetSubSpecial();
    }

    //LANGTODO
    private SubObjectData[] GetSubBases()
    {
        SubObjectData[] temp = new SubObjectData[] {
            CreateSubObject("Normal base", "The basic submarine base, can be used in many different situations", "1", null, SubBaseType.Medium),
            CreateSubObject("Light base", "Light submarine base, fast in movement but lacks defences", "1", null, SubBaseType.Light),
            CreateSubObject("Heavy base", "Heavy can to battle in, very strong at taking damage, but does not move fast at al", "1", null, SubBaseType.Heavy)
        };
        return temp;
    }

    private SubObjectData[] GetSubEngines()
    {
        SubObjectData[] temp = new SubObjectData[] {
            CreateSubObject("Heavy Engine", "", "1", null, SubEngineType.Heavy ),
            CreateSubObject("Normal Engine", "", "1", null,  SubEngineType.Medium),
            CreateSubObject("Light Engine", "", "1", null, SubEngineType.Light)
        };
        return temp;
    }

    private SubObjectData[] GetSubCannons()
    {
        SubObjectData[] temp = new SubObjectData[] {
            CreateSubObject("Torpedo cannons", "", "1", null,SubCannonType.Torpedo),
            CreateSubObject("Minigun", "", "1", null, SubCannonType.Minigun),
            CreateSubObject("Charge Ram", "", "1", null, SubCannonType.Ram)
        };
        return temp;
    }

    private SubObjectData[] GetSubSpecial()
    {
        SubObjectData[] temp = new SubObjectData[] {
            CreateSubObject("todo1", "temp1", "1", null, SubSpecialType.None),
            CreateSubObject("todo2", "temp2", "1", null, SubSpecialType.None)
        };
        return temp;
    }

    private void DisableAllSubObjects()
    {
        for (int i = 0; i < subBases.Length; i++)
        {
            subBases[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < subCannons.Length; i++)
        {
            subCannons[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < subEngines.Length; i++)
        {
            subEngines[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < subSpecial.Length; i++)
        {
            subSpecial[i].gameObject.SetActive(false);
        }
    }

    private SubObjectData CreateSubObject(string title, string desc, string level, Sprite img, SubBaseType _type)
    {
        GameObject temp = Instantiate(objectPrefab.gameObject, subObjectParent.transform.position, Quaternion.identity, subObjectParent.transform);//create ui object
        SubObjectData _data = temp.GetComponent<SubObjectData>();
        _data.CreateObject(this, title, desc, level, img, _type);
        return _data;
    }
    private SubObjectData CreateSubObject(string title, string desc, string level, Sprite img, SubEngineType _type)
    {
        GameObject temp = Instantiate(objectPrefab.gameObject, subObjectParent.transform.position, Quaternion.identity, subObjectParent.transform);//create ui object
        SubObjectData _data = temp.GetComponent<SubObjectData>();
        _data.CreateObject(this, title, desc, level, img, _type);
        return _data;
    }
    private SubObjectData CreateSubObject(string title, string desc, string level, Sprite img, SubCannonType _type)
    {
        GameObject temp = Instantiate(objectPrefab.gameObject, subObjectParent.transform.position, Quaternion.identity, subObjectParent.transform);//create ui object
        SubObjectData _data = temp.GetComponent<SubObjectData>();
        _data.CreateObject(this, title, desc, level, img, _type);
        return _data;
    }
    private SubObjectData CreateSubObject(string title, string desc, string level, Sprite img, SubSpecialType _type)
    {
        GameObject temp = Instantiate(objectPrefab.gameObject, subObjectParent.transform.position, Quaternion.identity, subObjectParent.transform);//create ui object
        SubObjectData _data = temp.GetComponent<SubObjectData>();
        _data.CreateObject(this, title, desc, level, img, _type);
        return _data;
    }

    private void OnEnable()
    {
        SetselectedHeader(0);
        SubCreatorManager.SP.SetSubMesh(true);
    }

    private void OnDisable()
    {
        SubCreatorManager.SP.SetSubMesh(false);
    }

    public void SetselectedHeader(int _index)
    {
        selectedHeader = _index;
        selectedHeader = Mathf.Clamp(selectedHeader, 0, 3);
        DisableAllSubObjects();

        if (_index == 0)
        {
            //Set base
            selectedArray = subBases;
        }
        if (_index == 1)
        {
            //set eng
            selectedArray = subEngines;
        }
        if (_index == 2)
        {
            //set cann
            selectedArray = subCannons;
        }
        if (_index == 3)
        {
            //set special
            selectedArray = subSpecial;
        }

        //Get right one
        currentSelected = 0;

        selectedArray[currentSelected].gameObject.SetActive(true);
    }

    public void ScrollThroughObject(int direction)
    {
        selectedArray[currentSelected].gameObject.SetActive(false);
        currentSelected += direction;
        if (currentSelected > selectedArray.Length - 1)
        {
            currentSelected = 0;
        }
        else if (currentSelected < 0)
        {
            currentSelected = selectedArray.Length - 1;
        }

        selectedArray[currentSelected].gameObject.SetActive(true);
    }


}


