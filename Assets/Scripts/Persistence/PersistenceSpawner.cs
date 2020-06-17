using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistenceSpawner : MonoBehaviour
{

    [SerializeField] private GameObject persistencePrefab;
    private void Awake()
    {
       GameObject per = Instantiate(persistencePrefab, transform.position, Quaternion.identity);
        per.name = "Persistence";
    }

}

