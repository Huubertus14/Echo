using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviourPun
{
    PlayerMovement pm;

    [SerializeField] private ParticleSystem ps;

    Quaternion orginRot;
    private void Start()
    {

        pm = GetComponent<PlayerMovement>();
        ps = GetComponentInChildren<ParticleSystem>();
        orginRot = ps.gameObject.transform.rotation;

        if (!photonView.IsMine)
        {
            pm.enabled = false;
            return;
        }


    }

    private void FixedUpdate()
    {
        photonView.RPC(nameof(RPC_FixParticle), RpcTarget.All);
    }

    public void Ping()
    {
        if (photonView.IsMine)
        {
            photonView.RPC(nameof(RPC_Ping), RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    private void RPC_FixParticle()
    {
        ps.transform.rotation = orginRot;
    }

    [PunRPC]
    private void RPC_Ping()
    {
        ps.Play();
    }

    public PlayerMovement GetPlayerMovement => pm;
}
