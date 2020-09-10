using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class EndGameListController : MonoBehaviour
{
    [SerializeField] private GameObject leaderboardParent;
    [SerializeField] private LeaderboardsEntry entryPrefab;
    [SerializeField] private TextMeshProUGUI winText;

    private List<LeaderboardsEntry> leaderEntrys;
    private GridLayoutGroup gridGroup;

    private void Awake()
    {
        gridGroup = GetComponentInChildren<GridLayoutGroup>();
        leaderEntrys = new List<LeaderboardsEntry>();
    }

    private void OnEnable()
    {
       // GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
    }

    public void CreateEndGameUI()
    {
       //disable controll buttons

        //create first entry
        LeaderboardsEntry _entry = Instantiate(entryPrefab.gameObject, transform.position, Quaternion.Euler(0, 0, 0), leaderboardParent.transform).GetComponent<LeaderboardsEntry>();
        _entry.gameObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
        _entry.SetCustomNames("Name", "Score", "Kills", "Assists", "Deaths", "Damage Done", "K/D"); //LANGTODO

        _entry.ShowScore();

        PlayerBehaviour[] tempPlayers = MatchManager.SP.GetAllPlayers;

        Array.Sort(tempPlayers, delegate (PlayerBehaviour x, PlayerBehaviour y)
        {
            return y.GetMatchKills.CompareTo(x.GetMatchKills);
        });

        for (int i = 0; i < tempPlayers.Length; i++)
        {
            Debug.Log("endGame: " + tempPlayers[i].PlayerName + " kills: " + tempPlayers[i].GetMatchKills);
            CreateEntry(tempPlayers[i]);
        }
        //LANGTODO:
        if (tempPlayers[0].PlayerName == GameManager.SP.GetPlayerB.PlayerName)
        {
            //Player won
            winText.text = "You Won!";
        }
        else
        {
            winText.text = "You Lose!";
        }

        //gridGroup.enabled = false;

        //Animation here
        Debug.Log("End game animation comes here");
        for (int i = 0; i < leaderEntrys.Count; i++)
        {
            leaderEntrys[i].ShowScore();
        }

    }

    private void CreateEntry(PlayerBehaviour pb)
    {
        LeaderboardsEntry entry = Instantiate(entryPrefab.gameObject, transform.position, Quaternion.Euler(0, 0, 0), leaderboardParent.transform).GetComponent<LeaderboardsEntry>();
        entry.gameObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
        entry.SetEndScoreText(pb);
        leaderEntrys.Add(entry);
    }

    public void LeaveGame()
    {
        GameManager.SP.LeaveGame();
    }
}
