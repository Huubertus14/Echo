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

    private MainMenuXPSlider xpSlider;

    protected override void Awake()
    {
        base.Awake();
        xpSlider = GetComponentInChildren<MainMenuXPSlider>();
    }

    private void OnEnable()
    {
        ToggleSettingsPanel(false);
        ToggleNameInputPanel(false);
        ToggleSubSelectPanel(false);

        SetMenuText(false);

       // SubCreatorManager.SP.SetSubMesh(true);
    }

    public void OnPlayClicked()
    {
        GameManager.SP.PlayGame();
    }


    public void CreateRoom()
    {
        NetworkManager.SP.CreateRoom();
    }

    public void SetMenuText(bool _onBoot)
    {
        //Debug.Log(GameManager.SP.playerData.playerName);

        if (string.IsNullOrEmpty(GameManager.SP.playerData.playerName))
        {
            ToggleNameInputPanel(true);
        }
        else
        {
            ToggleNameInputPanel(false);
        }

        //LANGTODO:
        playerNameText.text = "Name: " + GameManager.SP.playerData.playerName;
        xpText.text = "Level: " + GameUtils.CalculateLevel(GameManager.SP.playerData.playerXP);
        goldText.text = "Gold: " + GameManager.SP.playerData.gold;
        winText.text = "Wins: " + GameManager.SP.playerData.wins;

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

        if (_onBoot)
        {
            xpSlider.InitSlider();
        }
        else
        {
            Debug.Log("Add earned xp");
        }

    }

    public void SetSubSettingsText()
    {
        //LANGTODO:
        //Get values and set them
        SubCannonSettings cannonSettings = SubValues.GetCannonSettings((SubCannonType)GameManager.SP.playerData.subCannonSelected);
        SubEnineSettings engineSettings = SubValues.GetEngineSettings((SubEngineType)GameManager.SP.playerData.subEngineSelected);
        SubBaseSettings baseSettings = SubValues.GetBaseSettings((SubBaseType)GameManager.SP.playerData.subBaseSelected);

        //int selectedSubIndex = (int)GameManager.SP.GetSelectedSub;

        subNameText.text = "TODO";
        subLevelText.text = "Sub Level: ---";

        subHealthText.text = "Health: " + baseSettings.health.ToString();
        subAccelerationText.text = "Acceleration: " + engineSettings.acceleration.ToString();
        subSpeedText.text = "Max Speed: " + engineSettings.maxVelocity.ToString();

        subAttackSpeedText.text = "Attack Speed: " + cannonSettings.shootInterval.ToString();
        subDamageText.text = "Attack Damage: " + cannonSettings.baseDamage.ToString();
        subPingIntervalText.text = "Ping Speed: " + baseSettings.pingInterval.ToString();

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
    }
}
