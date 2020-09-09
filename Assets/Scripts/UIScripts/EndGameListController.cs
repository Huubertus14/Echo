using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System;

public class EndGameListController : MonoBehaviour
{
    [SerializeField] private LeaderboardsEntry entryPrefab;
    [SerializeField] private TextMeshProUGUI winText;

    private void OnEnable()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
    }

    public void CreateEndGameUI()
    {
        PlayerControlls.SP.SetControllImages(false); //disable controll buttons

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
        if (tempPlayers[0].photonView.IsMine)
        {
            //Player won
            winText.text = "You Won!";
        }
        else
        {
            winText.text = "You Lose!";
        }
    }

    private void CreateEntry(PlayerBehaviour pb)
    {
        LeaderboardsEntry entry = Instantiate(entryPrefab.gameObject, transform.position, Quaternion.Euler(0, 0, 0), transform).GetComponent<LeaderboardsEntry>();
        entry.gameObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
        entry.SetEndScoreText(pb);
    }

    public void LeaveGame()
    {
        GameManager.SP.LeaveGame();
    }
}
