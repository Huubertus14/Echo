using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using System;

public class NameChangePanelBehaviour : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;

    private void OnEnable()
    {
        //Set nameinput to player name
        if (String.IsNullOrEmpty(GameManager.SP.playerData.playerName))
        {
            nameInput.text = "Commander " + UnityEngine.Random.Range(100,5000);
        }
        else
        {
            nameInput.text = GameManager.SP.playerData.playerName;
        }
    }

    public void SetPlayerName()
    {
        string pName = nameInput.text;
        GameManager.SP.playerData.playerName = pName;
        MainMenu.SP.SetMenuText(false);
        PhotonNetwork.NickName = pName;

        GameManager.SP.SaveGame();
    }
}
