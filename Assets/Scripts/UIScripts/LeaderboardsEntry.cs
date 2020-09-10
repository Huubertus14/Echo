using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using GooglePlayGames.OurUtils;

public class LeaderboardsEntry : MonoBehaviour
{
    private PlayerBehaviour playerOwner;
    bool hasOwner = false;

    [Header("Refs:")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI killText;
    [SerializeField] private TextMeshProUGUI assistText;
    [SerializeField] private TextMeshProUGUI deathText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI miscText; //like kd or minutes survived or something

    private ImageFade[] fades;

    private void Awake()
    {
        fades = GetComponentsInChildren<ImageFade>();
    }

    private void Start()
    {
        foreach (var item in fades)
        {
            item.SetAlpha(0);
        }
    }

    public void ShowScore()
    {
        float _random = Random.Range(0.4f, 0.8f);
        foreach (var item in fades)
        {
            item.FadeIn(_random,false);
        }
    }

    public void SetAlpha(float _a)
    {
        foreach (var item in fades)
        {
            item.SetAlpha(_a);
        }
    }

    public void SetCustomNames(string _pName, string _pScore, string _kills, string _assists, string _deaths, string _damage, string _misc)
    {
        nameText.text = _pName;
        scoreText.text = _pScore;
        killText.text = _kills;
        assistText.text = _assists;
        deathText.text = _deaths;
        damageText.text = _damage;
        miscText.text = _misc;
    }

    public void SetEndScoreText(PlayerBehaviour pb)
    {
        if (pb.photonView.IsMine)
        {
            Debug.Log("Set bold text for my score");
        }
        playerOwner = pb;
        hasOwner = true;

        nameText.text = playerOwner.PlayerName;
        scoreText.text = "100";
        killText.text = playerOwner.GetMatchKills.ToString();
        assistText.text = playerOwner.GetMatchAssist.ToString();
        deathText.text = playerOwner.GetMatchDeaths.ToString();
        damageText.text = playerOwner.GetMatchDamage.ToString();

        string kdText;
        if (playerOwner.GetMatchDeaths <= 0)
        {
            kdText = playerOwner.GetMatchKills.ToString() + ".00";
        }
        else if (playerOwner.GetMatchKills <= 0)
        {
            kdText = "0.00";
        }
        else
        {
            kdText = (playerOwner.GetMatchKills / playerOwner.GetMatchDeaths).ToString();
        }
        miscText.text = kdText;
    }

    public PlayerBehaviour GetPlayer
    {
        get
        {
            if (hasOwner)
            {
                return playerOwner;
            }
            else
            {
                Debug.LogWarning("No owner set yet");
                return null;
            }
        }
    }
}
