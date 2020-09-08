using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Each sub has its own bullets, it pool this self
/// </summary>
public class PlayerCannon : MonoBehaviourPun
{
    [Header("Cannon values")]
    [SerializeField] private TorpedoBehaviour bullet;
    [SerializeField] private int maxBulletCount;

    private Queue<TorpedoBehaviour> bulletPool;

    private PlayerBehaviour pb;
    private SubBaseBehaviour sbb;

    public SubWeaponAbstract weaponAbstract;

    private bool foundWeapon = false;
    private bool foundsbb = false;

    private float shootTimer;
    private float shootInterval;

    
    private void Start()
    {
        pb = GetComponent<PlayerBehaviour>();

        shootInterval = pb.CannonSettings.shootInterval;
        StartCoroutine(FindRefs());
        shootTimer = -1;
    }

    private void FixedUpdate()
    {
        if (shootTimer >= 0)
        {
            shootTimer -= Time.deltaTime;
        }
    }

    private IEnumerator FindRefs()
    {
        float timeout = Time.time + 30;

        while (!foundsbb)
        {
            sbb = GetComponentInChildren<SubBaseBehaviour>();
            foundsbb = (sbb != null);
            if (Time.time > timeout)
            {
                Debug.LogError("Took to long to find SBB");
                yield break;
            }
            yield return 0;
        }

        while (!foundWeapon)
        {
            weaponAbstract = GetComponentInChildren<SubWeaponAbstract>();
            foundWeapon = (weaponAbstract != null);
            if (Time.time > timeout)
            {
                Debug.LogError("Took to long to find Weapon abstract");
                yield break;
            }
            yield return 0;
        }
        weaponAbstract.GiveValues(pb, sbb);
    }

    [PunRPC]
    public void RPC_Shoot(PhotonMessageInfo info)
    {
        weaponAbstract.Fire(info);
    }

    public void HasShot()
    {
        shootTimer = shootInterval;
    }

    public void DestroyPool()
    {
        photonView.RPC(nameof(RPC_DestroyPool), RpcTarget.All);
    }

    [PunRPC]
    private void RPC_DestroyPool()
    {
        Destroy(weaponAbstract.GetBulletParents.gameObject);
    }

    public bool CanShoot()
    {
        return (shootTimer < 0);
    }

}
