using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class LeaderboardsEntry : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI entryText;
    private PlayerBehaviour playerOwner;
    bool hasOwner = false;
    
    public void SetText(PlayerBehaviour pb)
    {
        if (pb.photonView.IsMine)
        {
            entryText.fontStyle = FontStyles.Bold;
        }
        playerOwner = pb;
        hasOwner = true;
        entryText.text = pb.PlayerName + ": " + pb.GetMatchKills + " MC: " +PhotonNetwork.IsMasterClient;
    }

    public PlayerBehaviour GetPlayer
    {
        get {
            if (hasOwner)
            {
                return playerOwner;
            }
            else
            {
                Debug.LogWarning("No owner set yet");
                return null;
            }
        }
    }
}
