using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : SingetonMonobehaviour<GameManager>
{
    public GameObject basePlayer;
    public PlayerData playerData;

    GameObject wholeScene;
    private GameModeAbstract gameMode;
    private PlayerBehaviour ownPlayerBehaviour;


    private SubType subType;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        Application.targetFrameRate = 30;
    }

    public void LeaveGame()
    {
        SharedCanvasBehaviour.SP.SetLoadingScreen(true);
        StartCoroutine(LeavingGame());
        //Unassign camera
    }

    private IEnumerator LeavingGame()
    {
        wholeScene = GameObject.Find("Scene");
        while (wholeScene == null)
        {
            yield return 0;
        }

        yield return 0;
        MatchManager.SP.LocalPlayerBehaviour.DestroyLinkedItems();
        yield return 0;

        MatchManager.SP.RemoveSelfFromList();
        //MatchManager.SP.DestroyOwnObject();
        yield return 0;
        //Destroy(wholeScene);

        yield return 0;

        GetPlayerB = null;
        PhotonNetwork.LeaveRoom();
        yield return 0;
        SceneManager.sceneLoaded += MainMenuLoaded;
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);

    }

    private void MainMenuLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log("Scene loaded: " + arg0.ToString());
        SharedCanvasBehaviour.SP.SetLoadingScreen(false);
        //Reconnect using photon

        SceneManager.sceneLoaded -= MainMenuLoaded;
    }

    public void PlayGame()
    {
        StartCoroutine(NetworkManager.SP.JoinGame());
    }

    public void LoadGame(PlayerData data)
    {
        playerData = data;
    }

    public GameModeAbstract GetGameMode()
    {
        if (gameMode == null)
        {
            gameMode = GameObject.Find("Scene").GetComponentInChildren<GameModeAbstract>();
        }

        return gameMode;
    }

    public void SaveGame()
    {
        SaveData.Save(playerData);
    }

    #region Props

    public SubType GetSelectedSub => subType;

    public PlayerBehaviour GetPlayerB
    {
        get
        {
            return ownPlayerBehaviour;
        }
        set
        {
            ownPlayerBehaviour = value;
        }
    }

    #endregion

}

