using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSelectorController : MonoBehaviour
{
    public void SelectSub(int _index)
    {
        GameManager.SP.SetSubType(_index);
        MainMenu.SP.SetSubSettingsText();

        GameManager.SP.playerData.subTypeSelected = _index;
        GameManager.SP.SaveGame();
    }
}
