using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolHolder : SingetonMonobehaviour<PoolHolder>
{
    private GameObject pingPool;
    private GameObject bulletPool;

    private void Start()
    {
        //Create two pools
        pingPool = new GameObject();
        pingPool.name = "Ping Pool";
        pingPool.transform.SetParent(transform);

        bulletPool = new GameObject();
        bulletPool.name = "Bullet Pool";
        bulletPool.transform.SetParent(transform);
    }

    public Transform GetPingPool()
    {
        return pingPool.transform;
    }
    public Transform GetBulletPool()
    {
        return bulletPool.transform;
    }
}
