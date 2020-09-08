using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoCannonBehaviour : SubWeaponAbstract
{
    public override void Start()
    {
        base.Start();
    }

    public override void Fire(PhotonMessageInfo info)
    {
        float lag = (float)(PhotonNetwork.Time - info.SentServerTime);
        TorpedoBehaviour _shot = bulletPool.Dequeue();
        _shot.gameObject.SetActive(true);
        _shot.FireTorpedo(pb, sbb.GetCannonPlace.transform.position + sbb.GetCannonPlace.transform.right, Quaternion.LookRotation(pb.SubMesh.transform.right, pb.SubMesh.transform.up), lag);
        bulletPool.Enqueue(_shot);
    }

    
}
