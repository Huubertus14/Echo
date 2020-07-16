using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EndGameListController : MonoBehaviour
{
    [SerializeField] private LeaderboardsEntry entryPrefab;

    public void CreateEndGameUI()
    {
        PlayerBehaviour[] tempPlayers = MatchManager.SP.GetAllPlayers;
        tempPlayers.OrderBy(a => a.GetMatchKills);

        for (int i = 0; i < tempPlayers.Length; i++)
        {
            CreateEntry(tempPlayers[i]);
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
