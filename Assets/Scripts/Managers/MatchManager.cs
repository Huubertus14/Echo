using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatchManager : SingetonMonobehaviour<MatchManager>
{
    [SerializeField] private List<PlayerBehaviour> players;
    private PlayerBehaviour localPlayer;

    private void Start()
    {
        players = new List<PlayerBehaviour>();
    }

    public void CreateAndAssignNewPlayer(GameObject _spawnObject)
    {
        if (PhotonNetwork.IsConnected)
        {
            if (_spawnObject != null)
            {
                GameObject newPlayer = PhotonNetwork.Instantiate(_spawnObject.name, new Vector3(1500,1500,1500), Quaternion.identity);

                PlayerBehaviour pl = newPlayer.GetComponent<PlayerBehaviour>();

                if (pl == null)
                {
                    Debug.LogError("Local player is null - ", gameObject);
                    return;
                }

                if (pl.photonView.IsMine)
                {
                    localPlayer = pl;
                    CameraController.SP.SetTarget(pl.SubMesh.transform);

                    //Assing player to controlls
                    PlayerControlls.SP.GivePlayerBehaviour(pl);
                }

                //SubCreatorManager.SP.SetSubMesh(false);
                UpdatePlayerList();
            }
            else
            {
                Debug.LogError("Spawn Object is null");
            }
        }
        else
        {
            Debug.LogWarning("Not connected to servers - " + gameObject);

        }
    }

    public void CreateNewPlayer(GameObject _spawnObject)
    {
        if (PhotonNetwork.IsConnected)
        {
            if (_spawnObject != null)
            {
                GameObject newPlayer = PhotonNetwork.Instantiate(_spawnObject.name, SpawnPointManager.SP.GetEmptySpawn.position, Quaternion.identity);
            }
        }
    }

    private void UpdatePlayerList()
    {
        players.Clear();
        PlayerBehaviour[] tempPlay = GameObject.FindObjectsOfType<PlayerBehaviour>();
        foreach (var item in tempPlay)
        {
            players.Add(item);
        }
    }
   
    public void RemoveSelfFromList()
    {
        players.Remove(localPlayer);
    }

    public void DestroyOwnObject()
    {
        PhotonNetwork.Destroy(localPlayer.photonView);
    }

    #region Property's

    public PlayerBehaviour LocalPlayerBehaviour => localPlayer;

    public PlayerBehaviour[] GetAllPlayers
    {
        get
        {
           // Debug.Log("Watchout! Expensive method");
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i] == null)
                {
                    players.RemoveAt(i);
                }
            }
            if (players.Count < PhotonNetwork.PlayerList.Length)
            {
                //List is not up to date. Update the list now
                UpdatePlayerList();
            }
            else if (players.Count > PhotonNetwork.PlayerList.Length)
            {
                UpdatePlayerList();
            }

            return players.ToArray();
        }
    }

    #endregion
}
