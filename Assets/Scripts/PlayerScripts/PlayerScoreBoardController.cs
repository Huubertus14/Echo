using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreBoardController : SingetonMonobehaviour<PlayerScoreBoardController>
{
    [Header("Go refs:")]
    [SerializeField] private GameObject ownGameScoreObject;

    [Header("Player Values")]
    [SerializeField] private TextMeshProUGUI killText;
    [SerializeField] private TextMeshProUGUI assistText;
    [SerializeField] private TextMeshProUGUI deathText;
    [SerializeField] private TextMeshProUGUI damageText;

    [Header("leaderboard:")]
    [SerializeField] private GameLeaderboardBehaviour leaderboard;
    [SerializeField] private EndGameListController endListController;

    private ImageFade[] ownScoreFades;

    private void Awake()
    {
        ownScoreFades = ownGameScoreObject.GetComponentsInChildren<ImageFade>();
    }

    private IEnumerator Start()
    {
        endListController.gameObject.SetActive(false);

        for (int i = 0; i < ownScoreFades.Length; i++)
        {
            ownScoreFades[i].SetAlpha(0);
        }

        yield return new WaitForSeconds(1.2f);

        //Fadein all TMP
        for (int i = 0; i < ownScoreFades.Length; i++)
        {
            ownScoreFades[i].FadeIn(Random.Range(0.8f, 1.5f));
        }
    }

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

    public void CreateEndScore()
    {
        StartCoroutine(EndScoreCoroutine());

        //TODO Calculate of player has won
    }

    private IEnumerator EndScoreCoroutine()//Do this for a fancy animation later
    {
        //Destroy all buttons
        PlayerControlls.SP.SetControllImages(false);

        yield return new WaitForSeconds(1.5f);
        endListController.gameObject.SetActive(true);
        endListController.CreateEndGameUI();
    }
}
