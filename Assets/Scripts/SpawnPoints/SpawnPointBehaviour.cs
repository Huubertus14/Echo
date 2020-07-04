using GooglePlayGames.BasicApi.Multiplayer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointBehaviour : MonoBehaviour
{

   [SerializeField] private List<PlayerBehaviour> playersInRange;
    private bool isSpawnAble = false;

    private void Awake()
    {
        playersInRange = new List<PlayerBehaviour>();
    }

    private void Start()
    {
        CheckSpawnPoint(MatchManager.SP.GetAllPlayers);
    }

    public void CheckSpawnPoint(PlayerBehaviour[] players)
    {
        Debug.Log("Checking spawns...");
        if (players.Length > 0)
        {
            playersInRange.Clear();
            for (int i = 0; i < players.Length; i++)
            {
                if (Vector3.Distance(transform.position, players[i].transform.position) > SpawnPointManager.SP.spawnDistance)
                {
                    playersInRange.Add(players[i]);
                }
            }
        }
    }

    public bool CanSpawn()
    {
       return playersInRange.Count < 1;
    }
}
