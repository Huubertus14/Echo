using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SubWeaponAbstract : MonoBehaviour
{
    [Header("WeaponSettings")]
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected int maxBulletCount;

    [SerializeField] protected PlayerBehaviour pb;
    [SerializeField] protected SubBaseBehaviour sbb;

    [SerializeField] protected GameObject bulletsParent;

    [SerializeField] protected Queue<TorpedoBehaviour> bulletPool;

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

        bulletPool = new Queue<TorpedoBehaviour>();

        //Create bullet parent
        bulletsParent = new GameObject();
        bulletsParent.name = "bulletPool (PlayerName)";
        bulletsParent.transform.SetParent(PoolHolder.SP.GetBulletPool());
        bulletsParent.transform.position = Vector3.zero;

        //Create the pool of bullets
        for (int i = 0; i < maxBulletCount; i++)
        {
            //photonView.RPC(nameof(RPC_CreateBulletPool), RpcTarget.AllBufferedViaServer);
            CreateBulletPool();
        }
    }

    public abstract void Fire(PhotonMessageInfo info);

    private void CreateBulletPool()
    {
        GameObject _bul = Instantiate(projectilePrefab.gameObject, transform.position, Quaternion.identity, bulletsParent.transform);
        _bul.SetActive(false);
        TorpedoBehaviour _behav = _bul.GetComponent<TorpedoBehaviour>();
        _behav.CreatePool("PlayerName", pb.GetPlayerColor);
        bulletPool.Enqueue(_behav);
    }

    public GameObject GetBulletParents => bulletsParent;
}
