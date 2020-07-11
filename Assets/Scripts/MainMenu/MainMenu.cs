using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu : SingetonMonobehaviour<MainMenu>
{
    [Header("Menu refs:")]
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private TextMeshProUGUI goldText;


    public void OnPlayClicked()
    {
        // GameManager.SP.PlayGame();
        NetworkManager.SP.JoinSpecificRoom();
    }


    public void CreateRoom()
    {
        NetworkManager.SP.CreateRoom();
    }
    
    public void SetMenuText()
    {
        playerNameText.text = "Name: " + GameManager.SP.playerData.playerName;
        xpText.text = "XP: " + GameManager.SP.playerData.xp;
        goldText.text = "Gold: " + GameManager.SP.playerData.gold;
    }


    public void TempAddGold()
    {
        GameManager.SP.playerData.gold++;
        SetMenuText();

        SaveData.Save(GameManager.SP.playerData);
    }
}
