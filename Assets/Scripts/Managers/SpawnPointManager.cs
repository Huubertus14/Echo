using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : SingetonMonobehaviour<SpawnPointManager>
{
   [SerializeField] private Transform[] spawnPoints;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    public Transform GetRandomSpawn
    {
        get {
            if (spawnPoints == null)
            {
                spawnPoints = GetComponentsInChildren<Transform>();
            }

            int x = (int)Random.Range(1,spawnPoints.Length);
            return spawnPoints[x]; 
        }
    }
}
