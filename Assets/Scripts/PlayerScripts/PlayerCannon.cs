using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Each sub has its own bullets, it pool this self
/// </summary>
public class PlayerCannon : MonoBehaviourPun, IPlayerAttack
{
    [Header("Cannon values")]
    [SerializeField] private BulletBehaviour bullet;
    [SerializeField] private int maxBulletCount;
    [SerializeField] private GameObject bulletsParent;
    [SerializeField] private GameObject ShootPoint;

    private Queue<BulletBehaviour> bulletPool;

    private PlayerBehaviour pb;

    private void Start()
    {
        if (bullet == null)
        {
            Debug.LogError("No bullet cached");
            return;
        }

        pb = GetComponent<PlayerBehaviour>();

        bulletPool = new Queue<BulletBehaviour>();

        //Create the pool of bullets
        for (int i = 0; i < maxBulletCount; i++)
        {
            //photonView.RPC(nameof(RPC_CreateBulletPool), RpcTarget.AllBufferedViaServer);
            CreateBulletPool();
        }
    }

    private void CreateBulletPool()
    {
        GameObject _bul = Instantiate(bullet.gameObject, transform.position, Quaternion.identity, bulletsParent.transform);
        _bul.SetActive(false);
        BulletBehaviour _behav = _bul.GetComponent<BulletBehaviour>();
        bulletPool.Enqueue(_behav);
    }

    [PunRPC]
    public void RPC_Shoot()
    {
        BulletBehaviour _shot = bulletPool.Dequeue();
        _shot.gameObject.SetActive(true);

        _shot.FireTorpedo(pb, ShootPoint.transform.position, Quaternion.LookRotation(transform.right, transform.up));

        bulletPool.Enqueue(_shot);
    }
}
