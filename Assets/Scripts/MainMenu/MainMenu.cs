using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
   


    public void OnPlayClicked()
    {
        // GameManager.SP.PlayGame();
        NetworkManager.SP.JoinSpecificRoom();
    }


    public void CreateRoom()
    {
        NetworkManager.SP.CreateRoom();
    }
    
}
