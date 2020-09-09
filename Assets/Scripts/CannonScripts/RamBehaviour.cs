using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamBehaviour : SubWeaponAbstract
{
    [SerializeField] private float ramDamage = 15;
    [SerializeField] private float bounceForce = 5;

    public override void Start()
    {
        ramDamage = pb.CannonSettings.baseDamage;
        bounceForce = pb.CannonSettings.bounceForce;
    }

    public override void Fire(PhotonMessageInfo info)
    {
        //TODO charge the sub? or charge the ram?

        // add animation
    }



    public override void CreatePool()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerHealth _ph = collision.gameObject.GetComponent<PlayerHealth>();
        if (_ph != null)
        {
            _ph.PlayerHit(pb, ramDamage);
            pb.GetPlayerMovement.BounceAway(transform.position, bounceForce);
        }
    }
}
