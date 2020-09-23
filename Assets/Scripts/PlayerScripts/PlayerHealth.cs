using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviourPun, IPunObservable
{
    private PlayerBehaviour pb;
    private Slider sl;
    private float maxValue;
    private float sliderValue;
    private float goalSliderValue;
    private List<PlayerBehaviour> playersDoneDamage;

    private PlayerBehaviour lastPlayerTakenDamageFrom;

    [SerializeField] private float health;
    [Space]
    [SerializeField] [Tooltip("The image of the hp bar slider")] private HealthbarImageFade healthBarFade;

    private void Awake()
    {
        sl = GetComponentInChildren<Slider>();
        pb = GetComponent<PlayerBehaviour>();
        playersDoneDamage = new List<PlayerBehaviour>();
    }

    public void SetInitValues(float maxPlayerHealth)
    {
        health = maxPlayerHealth;
        maxValue = maxPlayerHealth;
        sliderValue = maxValue;
        sl.maxValue = maxValue;
        sl.value = maxValue;
        UpdatePlayerHealthBar(maxValue);
    }

    private void UpdatePlayerHealthBar(float _newHealth)
    {
        goalSliderValue = _newHealth;

        //Show slider
    }

    public void PlayerHit(PlayerBehaviour _damageDealer, float _damage)
    {
        if (health <= 0)
        {
            //Player allready dead
            return;
        }

        if (!playersDoneDamage.Contains(_damageDealer) && _damageDealer != pb)
        {
            playersDoneDamage.Add(_damageDealer);
        }

        lastPlayerTakenDamageFrom = _damageDealer;
        _damageDealer.AddDamage((int)_damage);

        health -= _damage;
        UpdatePlayerHealthBar(health);

        healthBarFade.StartFade(0.9f);

        pb.Ping(40, 0.2f);

        if (health <= 0)
        {
            healthBarFade.StopFade();

            if (pb.photonView.IsMine)
            {
                photonView.RPC(nameof(RPC_PlayerDied), RpcTarget.AllBuffered);
                photonView.RPC(nameof(RPC_CreateKillFeed), RpcTarget.All, _damageDealer.PlayerName, pb.PlayerName);
                pb.PlayerDie();
            }

            if (GameManager.SP.GetPlayerB == _damageDealer)
            {
                KillFeedController.SP.SetKillFeedback("Kill", GameConstants.KILL_XP);
                _damageDealer.AddXP(GameConstants.KILL_XP);
                _damageDealer.AddScore(GameConstants.KILL_XP);
            }

            playersDoneDamage.Clear();
        }
    }

    [PunRPC]
    private void RPC_PlayerDied()
    {
        foreach (PlayerBehaviour play in playersDoneDamage)
        {
            if (play != lastPlayerTakenDamageFrom)
            {
                if (play != null)
                {
                    play.AssistOnPlayer(pb);
                    KillFeedController.SP.SetKillFeedback("Assist", GameConstants.BASE_ASSIST_XP);
                    play.AddXP(GameConstants.BASE_ASSIST_XP);
                    play.AddScore(GameConstants.BASE_ASSIST_XP);
                }
            }
        }

        lastPlayerTakenDamageFrom.KilledPlayer(pb);
    }

    [PunRPC]
    private void RPC_CreateKillFeed(string _killer, string _victim)
    {
        KillFeedController.SP.CreateKillFeed(_killer, _victim, KillType.Torpedo);
    }

    private void FixedUpdate()
    {
        sliderValue = Mathf.Lerp(sliderValue, goalSliderValue, Time.deltaTime * 5);
        sl.value = sliderValue;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (pb.photonView.IsMine)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(health);
            }
        }
        if (stream.IsReading)
        {
            health = (float)stream.ReceiveNext();
        }
    }

    public PlayerBehaviour GetPlayer => pb;

}
