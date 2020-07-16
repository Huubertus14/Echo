using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLeaderboardBehaviour : MonoBehaviour
{
    [SerializeField] private LeaderboardsEntry entryPrefab;
    private List<LeaderboardsEntry> savedEntries;

    private void Awake()
    {
        savedEntries = new List<LeaderboardsEntry>();
    }

    public void CreateAndUpdateLeaderboard()
    {
        //Get all players
        PlayerBehaviour[] tempPlayers = MatchManager.SP.GetAllPlayers;
        tempPlayers.OrderBy(a => a.GetMatchKills);

        for (int i = 0; i < tempPlayers.Length; i++)
        {
            if (i < savedEntries.Count)
            {
                //Update that entry
                savedEntries[i].SetInGameText(tempPlayers[i]);
            }
            else
            {
                //create new entry
                CreateEntry(tempPlayers[i]);
            }
        }

        if (savedEntries.Count > tempPlayers.Length)
        {
            for (int i = tempPlayers.Length; i < savedEntries.Count; i++)
            {
                savedEntries.RemoveAt(i);
            }
        }
    }

    public void CreateEntry(PlayerBehaviour pb)
    {
        LeaderboardsEntry entry = Instantiate(entryPrefab.gameObject, transform.position, Quaternion.Euler(0,0,0), transform).GetComponent<LeaderboardsEntry>();
        entry.gameObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
        entry.SetInGameText(pb);
        savedEntries.Add(entry);
    }
}
