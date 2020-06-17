using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviourPun, ISonarable
{
    PlayerMovement pm;
    private ParticleBehaviour pb;
    Quaternion orginRot;

    [SerializeField] Color playerColor;

    Renderer[] meshRenderers;

    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
        pb = GetComponentInChildren<ParticleBehaviour>();
        meshRenderers = GetComponentsInChildren<Renderer>();

        orginRot = pb.gameObject.transform.rotation;

        if (photonView.IsMine)
        {
            playerColor = Color.blue; //For now self is blue, enemy are red
        }
        else
        {
            playerColor = Color.red;
            pm.enabled = false;
        }

        //Give particle system the color
        pb.SetColor(playerColor);
        SetMeshColor(Color.black); //Set self to black
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
        pb.transform.rotation = orginRot;
    }

    [PunRPC]
    private void RPC_Ping()
    {
        pb.PlayParticle();
    }

    public void HitBySonar(Color col, Vector3 firstParticlePosition)
    {
        StartCoroutine(ColorFade(col));
    }

    private IEnumerator ColorFade(Color col)
    {
        SetMeshColor(col);
        yield return 0;

        float fadeTime = Time.time + 7.5f;

        while (Time.time < fadeTime)
        {
            col = Color.Lerp(col,Color.black, 0.1f/ 7.5f);
            SetMeshColor(col);
            yield return 0;
        }
        SetMeshColor(Color.black);
    }

    private void SetMeshColor(Color col)
    {
        foreach (var item in meshRenderers)
        {
            item.material.color = col;
        }
    }

    public PlayerMovement GetPlayerMovement => pm;
}
