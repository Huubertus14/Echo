using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System;

public class NetworkManager : MonoBehaviourPunCallbacks, IMatchmakingCallbacks
{
    public static NetworkManager SP; //Cannot inherit singleton

    private void Awake()
    {
        if (SP == null)
        {
            SP = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }



    bool searching = false;
    public IEnumerator JoinGame()
    {
        float TIME_OUT = Time.time + 150;

        while (Time.time < TIME_OUT && !searching)
        {
            Debug.Log("Searching...");
            if (!PhotonNetwork.InLobby)
            {
                //Try to join a a game
                PhotonNetwork.JoinRandomRoom();
            }
            yield return new WaitForSeconds(0.15f);
        }

        CreateAndJoinRandomRoom();
        yield return null;
    }

    public void CreateRoom()
    {
        string _randomRoomName = "Room1234";

        RoomOptions roomOptions = new RoomOptions
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 8
        };

        PhotonNetwork.CreateRoom(_randomRoomName, roomOptions);
    }

    public void JoinSpecificRoom()
    {
        PhotonNetwork.JoinRoom("Room1234");
    }

    private void CreateAndJoinRandomRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            return;
        }

        // DebugManager.Instance.DebugText("Create New Room");

        string _randomRoomName = "Room " + UnityEngine.Random.Range(1000, 10000); ;

        RoomOptions roomOptions = new RoomOptions
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 2
        };

        PhotonNetwork.CreateRoom(_randomRoomName, roomOptions);
        // DebugManager.Instance.DebugText("Max players = " + roomOptions.MaxPlayers.ToString());
    }



    #region CallBacks

    public override void OnJoinedRoom()
    {

        //StopCoroutine(JoinRandomRoom());
        // DebugManager.Instance.DebugText(PhotonNetwork.NickName + " Joined to " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("GameScene");

        Debug.Log("[PUN] Joined room");

        SceneManager.sceneLoaded += OnLoadedScene;


        searching = true;
        StopAllCoroutines();
        StopCoroutine(JoinGame());
    }

    private void OnLoadedScene(Scene _scene, LoadSceneMode arg1)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }


        MatchManager.SP.CreateAndAssignNewPlayer(GameManager.SP.basePlayer);
        SharedCanvasBehaviour.SP.SetLoadingScreen(true);
        SharedCanvasBehaviour.SP.SetLoadingMessage("Joining a room");

        SceneManager.sceneLoaded -= OnLoadedScene;
    }

    public override void OnJoinedLobby()
    {
        MatchManager.SP.CreateNewPlayer(GameManager.SP.basePlayer);
        if (PhotonNetwork.InLobby)
        {
            StopAllCoroutines();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("[PUN] Connected to master");
    }

    public override void OnConnected()
    {
        // DebugManager.Instance.DebugText("--- Connect to Internet ---");
        Debug.Log("[PUN] Connected");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogWarning("[PUN] failed to join Room: " + message);
        CreateAndJoinRandomRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        StopAllCoroutines();
    }

    public override void OnLeftRoom()
    {

    }
    #endregion
}
