using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviourPun, ISonarable, IPunObservable
{
    private PlayerMovement pm;
    private ParticleBehaviour pb;
    private PlayerCannon pc;
    private PlayerHealth ph;
    private Quaternion orginRotationParticleSystem;
    private Quaternion orginRotationPlayerUI;
    [SerializeField] private SubSettings settings;
    private SubType subType;
    private SonarPool sp;


    [SerializeField] private Color playerColor;
    private Renderer[] gameMeshes;

    [Header("Editor cached")]
    [SerializeField] private GameObject SubObject;
    [SerializeField] private GameObject playerCanvas;

    [Header("Game values")]
    [SerializeField] private string playerName = "Submarine Commander";
    [SerializeField] private float playerScore;
    [SerializeField] private float matchXP;
    private bool isAlive;
    private bool isInitialized = false;

    [Header("Score values")]
    [SerializeField] private int matchKills;
    [SerializeField] private int matchDeaths;
    [SerializeField] private int damageDealth;
    [SerializeField] private int matchAssists;

    private float currentHealth;
    private int outlineSizeID = 0;
    private int outlineColorID = 0;
    private float outlineSize = 0;

    private void Awake()
    {
        sp = GetComponent<SonarPool>();
        pm = GetComponent<PlayerMovement>();
        pb = GetComponentInChildren<ParticleBehaviour>();
        pc = GetComponent<PlayerCannon>();
        ph = GetComponent<PlayerHealth>();

        gameMeshes = SubObject.gameObject.GetComponentsInChildren<Renderer>();
    }

    private void Start()
    {
        playerName = photonView.Owner.NickName;

        //Get values and set them
        settings = SubValues.GetValues(subType);
        orginRotationParticleSystem = pb.gameObject.transform.rotation;
        orginRotationPlayerUI = playerCanvas.transform.localRotation;


        outlineSizeID = Shader.PropertyToID("_Outline");
        outlineColorID = Shader.PropertyToID("_OutlineColor");

        if (photonView.IsMine)
        {
            playerColor = Color.blue; //For now self is blue, enemy are red
            ChangeLocalMaterial();

            outlineSize = 0.15f; //size of the outline shader
        }
        else
        {
            playerColor = Color.red;
            pm.enabled = false;
            outlineSize = 0;

        }

        //Set outline Values && color
        foreach (var item in gameMeshes)
        {
            item.material.SetFloat(outlineSizeID, outlineSize);
            item.material.SetColor(outlineColorID, playerColor);
        }

        ResetMatchValues();

        //Give particle system the color
        pb.SetColor(playerColor);

        sp.CreatePool("Player Name01");
        sp.SetPoolColor(playerColor);

        SetMeshColor(Color.black, gameMeshes); //Set self to black



        //Set local values
        pm.movementSpeed = settings.movementSpeed;
        pm.waterResistence = settings.resistence;
        //Set values of cannon shooter

        ph.SetInitValues(settings.health);
        isAlive = false;

        SubObject.SetActive(false);
        StartCoroutine(Respawn());
        isInitialized = true;
    }


    private void ResetMatchValues()
    {
        matchAssists = 0;
        matchDeaths = 0;
        matchKills = 0;
        matchXP = 0;

        PlayerScoreBoardController.SP.SetAssistText(matchAssists);
        PlayerScoreBoardController.SP.SetDamageText(0);
        PlayerScoreBoardController.SP.SetDeathText(matchDeaths);
        PlayerScoreBoardController.SP.SetKillText(matchKills);
    }

    /// <summary>
    /// Change local player mat. 
    /// so the player can see itself all the time
    /// </summary>
    private void ChangeLocalMaterial()
    {

    }

    [PunRPC]
    private void RPC_ResetPlayerValues()
    {
        currentHealth = settings.health;
    }

    private void Update()
    {
        FixObjectRotation();
    }

    public void Ping(float beginSpeed = 0, float lifeTime = 0)
    {
        if (photonView.IsMine)
        {
            photonView.RPC(nameof(RPC_Ping), RpcTarget.AllBuffered, beginSpeed, lifeTime);
            RPC_HitBySonar(playerColor, transform.position);
        }

    }

    public void Shoot()
    {
        photonView.RPC(nameof(pc.RPC_Shoot), RpcTarget.AllBufferedViaServer);
    }

    private void FixObjectRotation()
    {
        pb.transform.rotation = orginRotationParticleSystem;
        // playerCanvas.transform.localRotation = orginRotationPlayerUI;
    }

    [PunRPC]
    private void RPC_Ping(float beginSpeed = 0, float lifeTime = 0)
    {
        if (!isInitialized) return;

        if (beginSpeed == 0)
        {
            beginSpeed = settings.basePingBeginSpeed;
        }
        if (lifeTime == 0)
        {
            lifeTime = settings.basePingLifeTime;
        }

        pb.SetStartSpeed(beginSpeed);
        pb.SetLifeTime(lifeTime);

        pb.PlayParticle();
    }

    public void RPC_HitBySonar(Color col, Vector3 firstParticlePosition)
    {
        StartCoroutine(ColorFade(playerColor));
    }

    private IEnumerator ColorFade(Color col)
    {
        SetMeshColor(col, gameMeshes);
        yield return 0;

        float fadeTime = Time.time + 7.5f;

        while (Time.time < fadeTime)
        {
            col = Color.Lerp(col, Color.black, 0.1f / 7.5f);
            SetMeshColor(col, gameMeshes);
            yield return 0;
        }
        SetMeshColor(Color.black, gameMeshes);
    }

    private void SetMeshColor(Color col, Renderer[] meshes)
    {
        foreach (var item in meshes)
        {
            item.material.color = col;
        }
    }

    public void HitBySonar(Color col, Vector3 firstParticlePosition)
    {

    }

    public void PlayerDie()
    {
        if (IsAlive)
        {
            isAlive = false;
            photonView.RPC(nameof(RPC_PlayerDie), RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    private void RPC_PlayerDie()
    {
        isAlive = false;
        SubObject.SetActive(false);
        if (photonView.IsMine)
        {
            matchDeaths += 1;
        }
        PlayerScoreBoardController.SP.UpdateScoreBoard();
        StartCoroutine(Respawn());
        ph.SetInitValues(settings.health);
    }

    [PunRPC]
    public void RPC_Spawn()
    {
        // Debug.Log("Spawning PLayer");
        SubObject.SetActive(true);
        isAlive = true;
        PlayerScoreBoardController.SP.UpdateScoreBoard();
    }

    /// <summary>
    /// needs to be an RPC
    /// </summary>
    /// <returns></returns>
    public IEnumerator Respawn()
    {
        Vector3 spawnPos = new Vector3(0, 0, 0);
        //Debug.Log("Respapwn");
        if (photonView.IsMine)
        {
            StartCoroutine(SpawnPointManager.SP.CheckAllSpawns());

            yield return new WaitUntil(() => SpawnPointManager.SP.allSpawnsChecked == true);

            /*

                    foreach (var item in SpawnPointManager.SP.GetAllSpawnPoints)
                    {
                        item.CheckSpawnPoint(MatchManager.SP.GetAllPlayers);
                        yield return 0;
                   }*/
            yield return 0;
            spawnPos = SpawnPointManager.SP.GetEmptySpawn.position;
            //Deactivate player
            PlayerScoreBoardController.SP.SetDeathText(matchDeaths);

            photonView.transform.position = spawnPos;
        }
        yield return new WaitForSeconds(0.8f);

        photonView.RPC(nameof(RPC_Spawn), RpcTarget.AllBufferedViaServer);


    }

    public void KilledPlayer(PlayerBehaviour victim)
    {
        if (photonView != victim.photonView && photonView.IsMine)
        {

            Debug.Log(PlayerName + " killed " + victim.PlayerName);
            matchKills += 1;
            PlayerScoreBoardController.SP.SetKillText(matchKills);

            if (matchKills >= GameManager.SP.GetGameMode().GetKillLimit) //TODO move to an active gamemde
            {
                GameManager.SP.GetGameMode().EndGame();
            }
        }
    }

    public void DestroyLinkedItems()
    {
        //All bullets and sonars
        pc.DestroyPool();
        sp.DestroyPool();
    }

    public void AssistOnPlayer(PlayerBehaviour victim)
    {
        matchAssists += 1;
        PlayerScoreBoardController.SP.SetAssistText(matchAssists);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint cp = collision.contacts[0];
        sp.CreateSonar(cp.point, 0.9f, 20);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Replicate kills and deaths over server So that every one can see the right values
        if (stream.IsWriting)
        {
            stream.SendNext(matchKills);
            stream.SendNext(matchDeaths);
        }
        else
        {
            matchKills = (int)stream.ReceiveNext();
            matchDeaths = (int)stream.ReceiveNext();
        }
    }

    public PlayerMovement GetPlayerMovement => pm;

    public Color GetPlayerColor => playerColor;

    public GameObject SubMesh => SubObject;

    public bool IsAlive => isAlive;

    public SubSettings Settings => settings;

    public int GetMatchKills => matchKills;
    public int GetMatchAssist => matchAssists;
    public int GetMatchDamage => 0;

    public int GetMatchDeaths => matchDeaths;

    public string PlayerName
    {
        get
        {
            return playerName;
        }
        set
        {
            playerName = value;
            //TODO UPDATE playername in photon
        }
    }
}
