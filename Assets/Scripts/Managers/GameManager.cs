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

    private bool gameLobbyReady = false;

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
        SubCreatorManager.SP.SetSubMesh(true);
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

    public IEnumerator PollingPlayerReady()
    {
        float timeOud = Time.time + 45;

        //if mesh is created
        while (ownPlayerBehaviour == null)
        {
            yield return 0;
        }
        while (ownPlayerBehaviour.SubMesh == null)
        {
            yield return 0;
        }
        while(!ownPlayerBehaviour.IsAlive || !ownPlayerBehaviour.IsInitiated)
        {
            yield return 0;
        }

        //Disable the placeholder sub
        //SubCreatorManager.SP.GetCurrentSub.SetActive(false);
        yield return 0;
        //all values of the player are ready

        SharedCanvasBehaviour.SP.SetLoadingScreen(false);
        PlayerControlls.SP.SetControllImages(true);
        //Animate buttons in
    }

    #region Props

    public bool GameLobbyReady
    {
        get
        {
            return gameLobbyReady;
        }
        set
        {
            gameLobbyReady = value;
        }
    }

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

