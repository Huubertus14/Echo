using Photon.Pun;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviourPun, ISonarable, IPunObservable
{
    private PlayerMovement pm;
    private ParticleBehaviour pb;

    private PlayerCannon pc;
    private PlayerHealth ph;
    private Quaternion orginRotationParticleSystem;
    private Quaternion orginRotationPlayerUI;

    [SerializeField] private SubBaseSettings baseSettings;
    [SerializeField] private SubEnineSettings engineSettings;
    [SerializeField] private SubCannonSettings cannonSettings;

    private SonarPool sp; 
    private bool isAlive;
    private bool isInitialized = false;

    [SerializeField] private Color playerColor;
    [SerializeField] private Renderer[] gameMeshes;

    [Header("Editor cached")]
    [SerializeField] private GameObject subObject;
    [SerializeField] private GameObject playerCanvas;

    [Header("Game values")]
    [SerializeField] private string playerName = "Submarine Commander";
    [SerializeField] private int playerScore;
    [SerializeField] private int matchXP;
    

    [Header("Score values")]
    [SerializeField] private int matchKills;
    [SerializeField] private int matchDeaths;
    [SerializeField] private int matchDamageDealth;
    [SerializeField] private int matchAssists;


    private float currentHealth;
    private int outlineSizeID = 0;
    private int outlineColorID = 0;
    private float outlineSize = 0;

    private void Awake()
    {
        if (photonView.IsMine)
        {
            photonView.RPC(nameof(RPC_SetSubSettings), RpcTarget.AllBufferedViaServer,
                GameManager.SP.playerData.subBaseSelected,
                GameManager.SP.playerData.subEngineSelected,
                GameManager.SP.playerData.subCannonSelected);
        }

        sp = GetComponent<SonarPool>();
        pm = GetComponent<PlayerMovement>();
        pb = GetComponentInChildren<ParticleBehaviour>();
        pc = GetComponent<PlayerCannon>();
        ph = GetComponent<PlayerHealth>();
    }

    private IEnumerator Start()
    {
        playerName = photonView.Owner.NickName;

        orginRotationParticleSystem = pb.gameObject.transform.rotation;
        orginRotationPlayerUI = playerCanvas.transform.localRotation;

        outlineSizeID = Shader.PropertyToID("_Outline");
        outlineColorID = Shader.PropertyToID("_OutlineColor");

        yield return 0;

        if (photonView.IsMine)
        {
            playerColor = Color.blue; //For now self is blue, enemy are red

            outlineSize = 0.15f; //size of the outline shader

            GameManager.SP.GetPlayerB = this;
            photonView.RPC(nameof(RPC_CreateSubMesh), RpcTarget.AllBufferedViaServer,
                GameManager.SP.playerData.subBaseSelected,
                GameManager.SP.playerData.subEngineSelected,
                GameManager.SP.playerData.subCannonSelected,
                GameManager.SP.playerData.subSpecialSelected);
        }
        else
        {
            playerColor = Color.red;
            pm.enabled = false;
            outlineSize = 0;
        }

        yield return 0;

        while (gameMeshes == null || gameMeshes.Length == 0)
        {
            gameMeshes = subObject.gameObject.GetComponentsInChildren<Renderer>();
            yield return 0;
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

        sp.CreatePool(PlayerName);
        sp.SetPoolColor(playerColor);

        SetMeshColor(Color.black, gameMeshes); //Set self to black

        isAlive = false;

        pm.GiveValues(baseSettings, engineSettings);
        ph.SetInitValues(baseSettings.health);

        subObject.SetActive(false);
        yield return 0;
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

    [PunRPC]
    private void RPC_SetSubSettings(int _baseSelected, int _engineSelected, int _cannonSelected)
    {
        //Get values and set them
        cannonSettings = SubValues.GetCannonSettings((SubCannonType)_cannonSelected);
        engineSettings = SubValues.GetEngineSettings((SubEngineType)_engineSelected);
        baseSettings = SubValues.GetBaseSettings((SubBaseType)_baseSelected);
    }

    [PunRPC]
    private void RPC_CreateSubMesh(int _base, int _engine, int _cannon, int _special)
    {
        GameObject toSpawn = SubCreatorManager.SP.CreateSub((SubBaseType)_base, (SubEngineType)_engine, (SubCannonType)_cannon, (SubSpecialType)_special);
        GameObject temp = Instantiate(toSpawn, Vector3.zero, Quaternion.identity, subObject.transform);
        Destroy(toSpawn);
        temp.transform.localPosition = Vector3.zero;
    }

    [PunRPC]
    private void RPC_ResetPlayerValues()
    {
        currentHealth = baseSettings.health;
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
        if (pc.CanShoot())
        {
            photonView.RPC(nameof(pc.RPC_Shoot), RpcTarget.All);
            pc.HasShot();
        }
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
            beginSpeed = baseSettings.basePingBeginSpeed;
        }
        if (lifeTime == 0)
        {
            lifeTime = baseSettings.basePingLifeTime;
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

        float fadeTime = Time.time + 7.5f;//TODO change this to variable value

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

    public void PlayerGameEnd()
    {
        //Add all player values to the save file
        GameManager.SP.playerData.damageDone += matchDamageDealth;
        GameManager.SP.playerData.playerXP += matchXP;
        GameManager.SP.playerData.totalDeaths += matchDeaths;
        GameManager.SP.playerData.totalKills += matchKills;

        //Calculate gold
        GameManager.SP.playerData.gold += GameUtils.CalculateEarnedGold(playerScore, matchXP);

        GameManager.SP.SaveGame();
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
        subObject.SetActive(false);
        if (photonView.IsMine)
        {
            matchDeaths += 1;
        }
        PlayerScoreBoardController.SP.UpdateScoreBoard();
        StartCoroutine(Respawn());
        ph.SetInitValues(baseSettings.health);
    }

    [PunRPC]
    public void RPC_Spawn()
    {
        // Debug.Log("Spawning PLayer");
        subObject.SetActive(true);
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
            if (matchKills == GameManager.SP.GetGameMode().GetKillLimit - 1)
            {
                //Player is on last kill
            }

            //LANGTODO
            KillFeedController.SP.SetKillFeedback("Kill", GameConstants.KILL_XP);
            AddXP(GameConstants.KILL_XP);
            AddScore(GameConstants.KILL_XP);

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

        SolidObjectBehaviour solObj = collision.gameObject.GetComponent<SolidObjectBehaviour>();
        if (solObj != null)
        {
            //Hit a solid object
            ph.PlayerHit(this, 3f);
            pm.BounceAway(collision.contacts[0].point, 2f);
        }
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

    #region Props

    public PlayerMovement GetPlayerMovement => pm;

    public Color GetPlayerColor => playerColor;

    public GameObject SubMesh => subObject;

    public bool IsAlive => isAlive;

    public bool IsInitiated => isInitialized;

    public int GetMatchKills => matchKills;
    public int GetMatchAssist => matchAssists;
    public int GetMatchDamage => 0;
    public int GetMatchDeaths => matchDeaths;

    public void AddDamage(int _value)
    {
        matchDamageDealth += _value;
    }
 
    public void AddScore(int _value)
    {
        playerScore += _value;
    }
    public void AddXP(int _value)
    {
        matchXP += _value;
    }

    public SubBaseSettings BaseSettings => baseSettings;
    public SubCannonSettings CannonSettings => cannonSettings;
    public SubEnineSettings EngineSettings => engineSettings;
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
    #endregion
}
