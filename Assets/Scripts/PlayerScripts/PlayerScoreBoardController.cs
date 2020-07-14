using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreBoardController : SingetonMonobehaviour<PlayerScoreBoardController>
{
    [Header("Player Values")]
    [SerializeField] private TextMeshProUGUI killText;
    [SerializeField] private TextMeshProUGUI assistText;
    [SerializeField] private TextMeshProUGUI deathText;
    [SerializeField] private TextMeshProUGUI damageText;

    [Header("leaderboard:")]
    [SerializeField] private GameLeaderboardBehaviour leaderboard;

    public void SetKillText(int kills)
    {
        killText.text = "Kills: " + kills;
    }

    public void SetAssistText(int kills)
    {
        assistText.text = "Assists: " + kills;
    }

    public void SetDeathText(int kills)
    {
        deathText.text = "Deaths: " + kills;
    }

    public void SetDamageText(int kills)
    {
        damageText.text = "Damage: " + kills;
    }

    public void UpdateScoreBoard()
    {
        leaderboard.CreateAndUpdateLeaderboard();
    }
}
