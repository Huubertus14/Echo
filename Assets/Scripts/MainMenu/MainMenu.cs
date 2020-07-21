using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class MainMenu : SingetonMonobehaviour<MainMenu>
{
    [Header("Menu refs:")]
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private TextMeshProUGUI goldText;

    [Header("Settings Refs:")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private NameChangePanelBehaviour playerNameInputField;

    [Header("SubValues Refs:")]
    [SerializeField] private TextMeshProUGUI subNameText;
    [SerializeField] private TextMeshProUGUI subLevelText;
    [SerializeField] private TextMeshProUGUI subHealthText;
    [SerializeField] private TextMeshProUGUI subSpeedText;
    [SerializeField] private TextMeshProUGUI subAccelerationText;
    [SerializeField] private TextMeshProUGUI subPingIntervalText;
    [SerializeField] private TextMeshProUGUI subDamageText;
    [SerializeField] private TextMeshProUGUI subAttackSpeedText;
    [Space]
    [SerializeField] private GameObject[] subMeshes;
    [SerializeField]private GameObject meshSpawnPosition;
    private GameObject selectedSubMesh;

    private void OnEnable()
    {
        ToggleSettingsPanel(false);
        ToggleNameInputPanel(false);

        SetMenuText();
    }

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
        //Debug.Log(GameManager.SP.playerData.playerName);

        if (String.IsNullOrEmpty(GameManager.SP.playerData.playerName))
        {
            ToggleNameInputPanel(true);
        }
        else
        {
            ToggleNameInputPanel(false);
        }

        //LANGTODO:
        playerNameText.text = "Name: " + GameManager.SP.playerData.playerName;
        xpText.text = "XP: " + GameManager.SP.playerData.playerXP;
        goldText.text = "Gold: " + GameManager.SP.playerData.gold;
    }

    public void SetSubSettingsText()
    {
        //LANGTODO:
        SubSettings set = SubValues.GetValues(GameManager.SP.GetSelectedSub);

        subNameText.text = set.subName;
        subLevelText.text = "Sub Level: ---";

        subHealthText.text = "Health: " + set.health.ToString();
        subAccelerationText.text = "Acceleration: " + set.movementSpeed.ToString();
        subSpeedText.text = "Max Speed: " + set.maxVelocity.ToString();

        subAttackSpeedText.text = "Attack Speed: " + set.shootInterval.ToString();
        subDamageText.text = "Attack Damage: ---";
        subPingIntervalText.text = "Ping Speed: " + set.pingInterval.ToString();

        //Create PlaceHolder On place
        if (selectedSubMesh == null || selectedSubMesh != subMeshes[(int)GameManager.SP.GetSelectedSub])
        {
            selectedSubMesh = subMeshes[(int)GameManager.SP.GetSelectedSub];
            GameObject _tempGO = Instantiate(selectedSubMesh, meshSpawnPosition.transform.position, Quaternion.identity);
            _tempGO.name = "PlaceHolderObject";
            _tempGO.transform.Rotate(90,0,0);
            PlaceHolderSubBehaviour _temp = _tempGO.GetComponent<PlaceHolderSubBehaviour>();
            
            
            //_temp.ChangeMeshColor(GameManager.SP.playerData.GetColor()); //Set ouline color

            //Replace mesh and color etc
        }
        else
        {
            selectedSubMesh = subMeshes[(int)GameManager.SP.GetSelectedSub];
           //ChangeMeshColor(GameManager.SP.playerData.playerColor);
        }
    }

    public void ToggleSettingsPanel(bool toggle)
    {
        settingsPanel.SetActive(toggle);//WorkAround so here can be extra code
    }
    public void ToggleNameInputPanel(bool toggle)
    {
        playerNameInputField.gameObject.SetActive(toggle);
    }
}
