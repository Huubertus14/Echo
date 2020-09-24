using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunBehaviour : SubWeaponAbstract
{
    [SerializeField] protected Queue<MinigunBulletBehaviour> bulletPool;

    public override void Start()
    {
        base.Start();
    }

    public override void Fire(PhotonMessageInfo info)
    {
        float lag = (float)(PhotonNetwork.Time - info.SentServerTime);
        MinigunBulletBehaviour _shot = bulletPool.Dequeue();
        _shot.gameObject.SetActive(true);
        _shot.FireBullet(pb, sbb.GetCannonPlace.transform.position + sbb.GetCannonPlace.transform.right, Quaternion.LookRotation(pb.SubMesh.transform.right, pb.SubMesh.transform.up), lag);
        bulletPool.Enqueue(_shot);
    }

    private void CreateBulletPool()
    {
        GameObject _bul = Instantiate(projectilePrefab.gameObject, transform.position, Quaternion.identity, bulletsParent.transform);
        _bul.SetActive(false);
        MinigunBulletBehaviour _behav = _bul.GetComponent<MinigunBulletBehaviour>();
        bulletPool.Enqueue(_behav);
    }

    public override void CreatePool()
    {
        bulletPool = new Queue<MinigunBulletBehaviour>();

        //Create bullet parent
        bulletsParent = new GameObject();
        bulletsParent.name = "bulletPool (" + pb.PlayerName +")";
        bulletsParent.transform.SetParent(PoolHolder.SP.GetBulletPool());
        bulletsParent.transform.position = Vector3.zero;

        //Create the pool of bullets
        for (int i = 0; i < maxBulletCount; i++)
        {
            //photonView.RPC(nameof(RPC_CreateBulletPool), RpcTarget.AllBufferedViaServer);
            CreateBulletPool();
        }
    }
}
