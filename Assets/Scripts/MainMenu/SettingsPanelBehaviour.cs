using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelBehaviour : MonoBehaviour
{
    [SerializeField] private RectTransform backGround;
    [SerializeField] private RectTransform foreGround;

    private void Awake()
    {
        backGround.sizeDelta = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        foreGround.sizeDelta = new Vector2(Screen.currentResolution.width *0.7f, Screen.currentResolution.height *0.7f);
    }


}
