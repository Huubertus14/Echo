using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMeshCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] enginePlaces;
    [SerializeField] private GameObject[] weaponPlaces;

    public void CreateWeapons(GameObject weaponPrefab)
    {
        if (weaponPlaces.Length > 1)
        {
            //Create multiple weapons
        }
        else
        {
            //Create normal weapon
            GameObject tempWeapon = Instantiate(weaponPrefab, weaponPlaces[0].transform.position, Quaternion.identity, transform);
        }
    }

    public void CreateEngines(GameObject enginePrefab)
    {
        if (enginePlaces.Length > 1)
        {
            //Create multiple weapons
        }
        else
        {
            //Create normal weapon
            GameObject tempEngine = Instantiate(enginePrefab, enginePlaces[0].transform.position, Quaternion.identity, transform);
        }


    }
}
