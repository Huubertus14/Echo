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
    [SerializeField] private TextMeshProUGUI winLoseText;
    [SerializeField] private TextMeshProUGUI winText;

    [Header("Settings Refs:")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private NameChangePanelBehaviour playerNameInputField;
    [SerializeField] private SubCreatorUIManager subSelector;

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
    private PlaceHolderSubBehaviour[] selectedSubMesh = new PlaceHolderSubBehaviour[3];
    private bool meshesCreated = false;

    private IEnumerator CreateSubMeshes()
    {
        if (!meshesCreated)
        {
            for (int i = 0; i < selectedSubMesh.Length; i++)
            {
                selectedSubMesh[i] = Instantiate(subMeshes[i], meshSpawnPosition.transform.position, Quaternion.identity).GetComponent<PlaceHolderSubBehaviour>();
                selectedSubMesh[i].gameObject.transform.Rotate(90, 0, 0);
                selectedSubMesh[i].gameObject.name = "placeHolder Object " + i;
                selectedSubMesh[i].gameObject.gameObject.SetActive(false);
            }
            meshesCreated = true;
        }
        yield return 0;
        for (int i = 0; i < selectedSubMesh.Length; i++)
        {
            selectedSubMesh[i].gameObject.SetActive(true);

            selectedSubMesh[i].ChangeMeshColor(GameUtils.GetColorFromArray(GameManager.SP.playerData.GetColor()));
            //Debug.Log("Selec: " + GameManager.SP.playerData.subTypeSelected) ;
            selectedSubMesh[i].gameObject.SetActive(i == GameManager.SP.playerData.subTypeSelected);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(CreateSubMeshes());

        ToggleSettingsPanel(false);
        ToggleNameInputPanel(false);
        ToggleSubSelectPanel(false);

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
        winText.text = "Wins: " +GameManager.SP.playerData.wins;

        float wl = 0;
        if (GameManager.SP.playerData.loses > 0)
        {
            wl = GameManager.SP.playerData.wins / GameManager.SP.playerData.loses;
        }
        else
        {
            wl = GameManager.SP.playerData.wins;
        }

        winLoseText.text = "Win/Lose: " + wl;
    }

    public void SetSubSettingsText()
    {
        //LANGTODO:
        SubSettings set = SubValues.GetValues(GameManager.SP.GetSelectedSub);

        //int selectedSubIndex = (int)GameManager.SP.GetSelectedSub;

        subNameText.text = set.subName;
        subLevelText.text = "Sub Level: ---";

        subHealthText.text = "Health: " + set.health.ToString();
        subAccelerationText.text = "Acceleration: " + set.movementSpeed.ToString();
        subSpeedText.text = "Max Speed: " + set.maxVelocity.ToString();

        subAttackSpeedText.text = "Attack Speed: " + set.shootInterval.ToString();
        subDamageText.text = "Attack Damage: ---";
        subPingIntervalText.text = "Ping Speed: " + set.pingInterval.ToString();



        /*for (int i = 0; i < selectedSubMesh.Length; i++)
        {
            selectedSubMesh[i].gameObject.SetActive(true);

            selectedSubMesh[i].ChangeMeshColor(GameUtils.GetColorFromArray(GameManager.SP.playerData.GetColor()));

            selectedSubMesh[i].gameObject.SetActive(selectedSubIndex == i);
        }*/
    }

    public void ToggleSettingsPanel(bool toggle)
    {
        settingsPanel.SetActive(toggle);//WorkAround so here can be extra code
    }
    public void ToggleNameInputPanel(bool toggle)
    {
        playerNameInputField.gameObject.SetActive(toggle);
    }

    public void ToggleSubSelectPanel(bool toggle)
    {
        subSelector.gameObject.SetActive(toggle);
        for (int i = 0; i < selectedSubMesh.Length; i++)
        {
            if (toggle)
            {
                selectedSubMesh[i].gameObject.SetActive(false);
            }
            else
            {
                selectedSubMesh[i].ChangeMeshColor(GameUtils.GetColorFromArray(GameManager.SP.playerData.GetColor()));
                selectedSubMesh[i].gameObject.SetActive((int)GameManager.SP.GetSelectedSub == i);
            }
            
        }
    }
}
