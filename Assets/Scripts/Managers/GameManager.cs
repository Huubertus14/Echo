using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : SingetonMonobehaviour<GameManager>
{
    public GameObject basePlayer;
    public PlayerData playerData;


    private void Awake()
    {
        DontDestroyOnLoad(this);
        Application.targetFrameRate = 30;
    }

    public void LeaveGame()
    {
        PhotonNetwork.LeaveRoom();
        //Unassign camera
    }

    public void PlayGame()
    {
        StartCoroutine(NetworkManager.SP.JoinGame());
    }

    public void LoadGame(PlayerData data)
    {
        playerData = data;
        MainMenu.SP.SetMenuText();
    }

}

