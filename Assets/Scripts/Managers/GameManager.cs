using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingetonMonobehaviour<GameManager>
{
    public GameObject basePlayer;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Application.targetFrameRate = 30;
    }

    public void PlayGame()
    {
        StartCoroutine(NetworkManager.SP.JoinGame());
    }

}
