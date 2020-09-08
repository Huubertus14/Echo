using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SubWeaponAbstract : MonoBehaviour
{
    [Header("WeaponSettings")]
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected int maxBulletCount;

    protected PlayerBehaviour pb;
    protected SubBaseBehaviour sbb;

    protected GameObject bulletsParent;

    public virtual void Start()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("No bullet cached");
            return;
        }
    }

    public virtual void GiveValues(PlayerBehaviour _pb, SubBaseBehaviour _sbb)
    {
        pb = _pb;
        sbb = _sbb;
    }

    public abstract void CreatePool();

    public abstract void Fire(PhotonMessageInfo info);

    public GameObject GetBulletParents => bulletsParent;
}
