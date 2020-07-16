using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameModeAbstract : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private float timeLimit;
    [SerializeField] private bool gameStarted;
    private int killLimit = 7;

    protected virtual void Start()
    {
        gameStarted = false;

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Only do this on MC");
            timeLimit = 180f;
        }
    }

    protected virtual void Update()
    {
        if (!gameStarted)
        {
            SharedCanvasBehaviour.SP.SetLoadingMessage("Waiting for enough players");
            //Wait unitil game started
            if (PhotonNetwork.IsMasterClient)
            {
                if (MatchManager.SP.GetAllPlayers.Length > 0) //Or time limit
                {
                    StartGame();
                }
            }
        }
        else
        {
            if (PhotonNetwork.IsMasterClient)
            {
                timeLimit -= Time.deltaTime;
            }
        }
    }

    private void StartGame()
    {
        gameStarted = true;

        //Remove loading screen of all players
        photonView.RPC(nameof(RPC_StartGameOnAllClients), RpcTarget.AllBufferedViaServer);
    }

    [PunRPC]
    protected void RPC_StartGameOnAllClients()
    {
        SharedCanvasBehaviour.SP.SetLoadingScreen(false);
    }

    public void EndGame()
    {
        photonView.RPC(nameof(RPC_EndGameOnAllClients), RpcTarget.AllBufferedViaServer);
    }

    [PunRPC]
    protected void RPC_EndGameOnAllClients()
    {
         PlayerScoreBoardController.SP.CreateEndScore();
        //Disable Player Controlls
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(timeLimit);
            }
        }
        if (stream.IsReading)
        {
            timeLimit = (float)stream.ReceiveNext();
        }
    }

    public int GetKillLimit
    {
        get
        {
            return killLimit;
        }
        set
        {
            Debug.Log("Kill limit changed");
            killLimit = value;
        }
    }
}
