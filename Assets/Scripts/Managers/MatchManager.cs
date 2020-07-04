using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : SingetonMonobehaviour<MatchManager>
{
    private PlayerBehaviour localPlayer;

    public void CreateAndAssignNewPlayer(GameObject _spawnObject)
    {
        if (PhotonNetwork.IsConnected)
        {
            if (_spawnObject != null)
            {
                GameObject newPlayer = PhotonNetwork.Instantiate(_spawnObject.name, SpawnPointManager.SP.GetEmptySpawn.position, Quaternion.identity);

                PlayerBehaviour pl = newPlayer.GetComponent<PlayerBehaviour>();
                if (pl.photonView.IsMine)
                {
                    localPlayer = pl;
                    CameraController.SP.SetTarget(pl.SubMesh.transform);

                    //Assing player to controlls
                    PlayerControlls.SP.GivePlayerBehaviour(pl);
                }

                if (pl == null)
                {
                    Debug.LogError("Local player is null - ", gameObject);
                }
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

    #region Property's

    public PlayerBehaviour LocalPlayerBehaviour => localPlayer;

    public PlayerBehaviour GetAllPlayers
    {
        get
        {
            return null;
        }
    }

    #endregion
}
