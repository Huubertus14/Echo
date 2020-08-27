using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMarineBuilder : SingetonMonobehaviour<SubMarineBuilder>
{
    [SerializeField] private GameObject[] subBases;
    [SerializeField] private GameObject[] subCannons;
    [SerializeField] private GameObject[] subRears;//? right name?

    public GameObject CreateSub()
    {
        GameObject tempSub = new GameObject();
        tempSub.name = "Sub?";

        return tempSub;
    }
}
