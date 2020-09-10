using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLeaderboardBehaviour : MonoBehaviour
{
    [SerializeField] private MatchScoreBehaviour entryPrefab;
    private List<MatchScoreBehaviour> savedEntries;

    private void Awake()
    {
        savedEntries = new List<MatchScoreBehaviour>();
    }

    public void CreateAndUpdateLeaderboard()
    {
        //Get all players
        PlayerBehaviour[] tempPlayers = MatchManager.SP.GetAllPlayers;

        Array.Sort(tempPlayers, delegate (PlayerBehaviour x, PlayerBehaviour y)
        {
            return y.GetMatchKills.CompareTo(x.GetMatchKills);
        });

        for (int i = 0; i < tempPlayers.Length; i++)
        {
            if (i < savedEntries.Count)
            {
                //Update that entry
                savedEntries[i].SetText(tempPlayers[i].PlayerName, tempPlayers[i].GetMatchKills.ToString());
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
        MatchScoreBehaviour entry = Instantiate(entryPrefab.gameObject, transform.position, Quaternion.Euler(0, 0, 0), transform).GetComponent<MatchScoreBehaviour>();
        entry.gameObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
        entry.SetText(pb.PlayerName, pb.GetMatchKills.ToString());
        savedEntries.Add(entry);
    }
}
