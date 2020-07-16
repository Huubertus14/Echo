using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : SingetonMonobehaviour<SpawnPointManager>
{
    [Header("Spawn settings:")]
    public float spawnDistance = 4f;

    [SerializeField] private SpawnPointBehaviour[] spawnPoints;
    PhotonView photonView;
    public bool allSpawnsChecked = false;

    protected override void Awake()
    {
        base.Awake();
        spawnPoints = GetComponentsInChildren<SpawnPointBehaviour>();
        photonView = GetComponent<PhotonView>();
    }

    public IEnumerator CheckAllSpawns()
    {
        //Debug.Log("Check all spawns");
        if (spawnPoints.Length < 1)
        {
            Debug.LogError("Spawn points are empty");
        }
        //Debug.Log("Checking all spawns ; " + MatchManager.SP.GetAllPlayers.Length);
        allSpawnsChecked = false;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i].CheckSpawnPoint(MatchManager.SP.GetAllPlayers);
            yield return 0;
        }
        yield return 0;
       
        allSpawnsChecked = true;
        //Debug.Log("Done Checking all spawns");
        yield return 0;
    }


    public Transform GetEmptySpawn
    {
        get
        {
            if (spawnPoints == null)
            {
                spawnPoints = GetComponentsInChildren<SpawnPointBehaviour>();
            }

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                spawnPoints[i].CheckSpawnPoint(MatchManager.SP.GetAllPlayers);
                if (spawnPoints[i].CanSpawn())
                {
                    return spawnPoints[i].transform;
                }
            }

            Debug.LogWarning("No empty spawnpoint found");
            int x = (int)Random.Range(1, spawnPoints.Length);
            return spawnPoints[x].transform;
        }
    }

    public Transform GetRandomSpawn
    {
        get
        {
            if (spawnPoints == null)
            {
                spawnPoints = GetComponentsInChildren<SpawnPointBehaviour>();
            }

            int x = (int)Random.Range(1, spawnPoints.Length);
            if (!spawnPoints[x].CanSpawn())
            {
                return GetRandomSpawn;
            }
            else
            {
                return spawnPoints[x].transform;
            }
        }
    }

    public bool AllChecked => allSpawnsChecked;

    public SpawnPointBehaviour[] GetAllSpawnPoints => spawnPoints;
}
