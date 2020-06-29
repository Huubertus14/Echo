using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class created to make it easy to make a lot of pings at once
/// </summary>
public class SonarPool : MonoBehaviour
{
    [Header("Pool values")]
    public int poolSize;
    public GameObject poolParent;
    [Space]
    [SerializeField] private ParticleBehaviour poolObjectPrefab;
    private Queue<ParticleBehaviour> pool;



  

    public void CreatePool()
    {
        pool = new Queue<ParticleBehaviour>();

        for (int i = 0; i < poolSize; i++)
        {
            CreatePoolObject();
        }
    }

    private void CreatePoolObject()
    {
        ParticleBehaviour _part = Instantiate(poolObjectPrefab.gameObject, transform.position, poolObjectPrefab.transform.rotation).GetComponent<ParticleBehaviour>();
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

    private void OnDestroy()
    {
        //Set all pool object as child objects, so it will be destroyed with the player
        for (int i = 0; i < pool.Count; i++)
        {
            ParticleBehaviour p = pool.Dequeue();
            p.gameObject.transform.SetParent(transform);
        }
    }
}
