using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : SingetonMonobehaviour<SpawnPointManager>, IPunObservable
{
    [SerializeField] private SpawnPointBehaviour[] spawnPoints;

    PhotonView photonView;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<SpawnPointBehaviour>();
        photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        photonView.RPC(nameof(CheckAllSpawns), RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void CheckAllSpawns()
    {
        foreach (var item in spawnPoints)
        {
            item.CanSpawn();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        CheckAllSpawns();
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
}
