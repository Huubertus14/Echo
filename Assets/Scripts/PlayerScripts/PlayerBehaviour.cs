using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviourPun, ISonarable
{
    private PlayerMovement pm;
    private ParticleBehaviour pb;
    private PlayerCannon pc;
    private Quaternion orginRot;
    private SubSettings settings;
    private SubType subType;

    [SerializeField] private Color playerColor;
    private Renderer[] meshRenderers;

    [Header("Game values")]
    [SerializeField] private float playerScore;
    [SerializeField] private float matchXP;

    private float currentHealth;

    private void Start()
    {
        //Get values and set them
        settings = SubValues.GetValues(subType);

        pm = GetComponent<PlayerMovement>();
        pb = GetComponentInChildren<ParticleBehaviour>();
        pc = GetComponent<PlayerCannon>();

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

        //Set local values
        pm.movementSpeed = settings.movementSpeed;
        pm.waterResistence = settings.resistence;
        //Set values of cannon shooter
    }


    [PunRPC]
    private void RPC_ResetPlayerValues()
    {
        currentHealth = settings.health;
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
            RPC_HitBySonar(playerColor, transform.position);
        }

    }


    public void Shoot()
    {
        photonView.RPC(nameof(pc.RPC_Shoot), RpcTarget.AllBufferedViaServer);
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

    public void RPC_HitBySonar(Color col, Vector3 firstParticlePosition)
    {
        StartCoroutine(ColorFade(playerColor));
    }

    private IEnumerator ColorFade(Color col)
    {
        SetMeshColor(col);
        yield return 0;

        float fadeTime = Time.time + 7.5f;

        while (Time.time < fadeTime)
        {
            col = Color.Lerp(col, Color.black, 0.1f / 7.5f);
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

    public void HitBySonar(Color col, Vector3 firstParticlePosition)
    {

    }

    public PlayerMovement GetPlayerMovement => pm;
}
