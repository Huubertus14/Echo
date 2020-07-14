using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// Class created to make it easy to make a lot of pings at once
/// </summary>
public class SonarPool : MonoBehaviourPun
{
    [Header("Pool values")]
    public int poolSize;
    private GameObject poolParent;
    [Space]
    [SerializeField] private ParticleBehaviour poolObjectPrefab;
    private Queue<ParticleBehaviour> pool;

    bool poolExist = false;

    private void Awake()
    {
        pool = new Queue<ParticleBehaviour>();
    }

    public void CreatePool(string objectName)
    {
        poolParent = new GameObject();
        poolParent.name = "SonarPool (" + objectName +")";
        poolParent.transform.SetParent(PoolHolder.SP.GetPingPool());
        poolParent.transform.position = Vector3.zero;


        for (int i = 0; i < poolSize; i++)
        {
            CreatePoolObject();
        }

        poolExist = true;
    }

    private void CreatePoolObject()
    {
        ParticleBehaviour _part = Instantiate(poolObjectPrefab.gameObject, transform.position, poolObjectPrefab.transform.rotation, poolParent.transform).GetComponent<ParticleBehaviour>();
        _part.gameObject.SetActive(false);
        pool.Enqueue(_part);
    }

    public void SetPoolColor(Color _col)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            ParticleBehaviour p = pool.Dequeue();
            p.SetColor(_col);
            pool.Enqueue(p);

        }
    }

    public void CreateSonar(Vector3 _pos, float lifeTime = 0, float startSpeed = 0)
    {
        if (!poolExist) return;
        
        ParticleBehaviour _part = pool.Dequeue();
        _part.gameObject.SetActive(true);
        _part.gameObject.transform.position = _pos;

        if (lifeTime > 0)
        {
            _part.SetLifeTime(lifeTime);
        }

        if (startSpeed > 0)
        {
            _part.SetStartSpeed(startSpeed);
        }

        _part.PlayParticle();

        pool.Enqueue(_part);
    }

    public void DestroyPool()
    {
        if (photonView != null)
        {
            photonView.RPC(nameof(RPC_DestroyPool), RpcTarget.All);
        }
        else
        {
            RPC_DestroyPool();
        }
    }

    private void RPC_DestroyPool()
    {
        ParticleBehaviour[] parts = pool.ToArray();
        for (int i = 0; i < parts.Length; i++)
        {
            Destroy(parts[i].gameObject);
        }
        Destroy(poolParent);
    }

    private void OnDestroy()
    {
        //Set all pool object as child objects, so it will be destroyed with the player
        Destroy(poolParent.gameObject);
    }
}
