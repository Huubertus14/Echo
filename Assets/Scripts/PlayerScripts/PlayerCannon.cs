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

    private PlaceHolderSubBehaviour placeholder;

    private GameObject bulletsParent;

    private Queue<BulletBehaviour> bulletPool;

    private PlayerBehaviour pb;

    private bool bulletExist = false;

    private void Start()
    {
        if (bullet == null)
        {
            Debug.LogError("No bullet cached");
            return;
        }

        pb = GetComponent<PlayerBehaviour>();
        bulletPool = new Queue<BulletBehaviour>();

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

        bulletExist = true;

    }

    private void CreateBulletPool()
    {
        GameObject _bul = Instantiate(bullet.gameObject, transform.position, Quaternion.identity, bulletsParent.transform);
        _bul.SetActive(false);
        BulletBehaviour _behav = _bul.GetComponent<BulletBehaviour>();
        _behav.CreatePool("PlayerName", pb.GetPlayerColor);
        bulletPool.Enqueue(_behav);
    }

    [PunRPC]
    public void RPC_Shoot(PhotonMessageInfo info)
    {
        if (!bulletExist) return;

        float lag = (float)(PhotonNetwork.Time - info.SentServerTime);
        BulletBehaviour _shot = bulletPool.Dequeue();
        _shot.gameObject.SetActive(true);

        _shot.FireTorpedo(pb, Place.GetShootPointAt(0).transform.position, Quaternion.LookRotation(pb.SubMesh.transform.right, pb.SubMesh.transform.up), lag);

        bulletPool.Enqueue(_shot);
    }

    public void DestroyPool()
    {
        photonView.RPC(nameof(RPC_DestroyPool), RpcTarget.All);
    }

    [PunRPC]
    private void RPC_DestroyPool()
    {
        BulletBehaviour[] temp = bulletPool.ToArray();
        for (int i = 0; i < bulletPool.Count; i++)
        {
            temp[i].DestroySonarPool();
            Destroy(temp[i].gameObject);
        }
        Destroy(bulletsParent);
    }

    bool placeSet = false;
    private PlaceHolderSubBehaviour Place
    {
        get
        {
            if (!placeSet)
            {
                placeholder = GetComponentInChildren<PlaceHolderSubBehaviour>();
                placeSet = true;
            }
            return placeholder;
        }
    }

}
