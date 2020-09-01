using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class SubObjectData: MonoBehaviour
{
    private SubCreatorUIManager manager;
    [SerializeField]private TextMeshProUGUI objectTitle;
    [SerializeField] private TextMeshProUGUI objectDescription;
    [SerializeField] private TextMeshProUGUI objectLevelText;
    [SerializeField] private Image previewImage;

    /*public SubObjectData(string _title, string _description, string _leveltxt, Sprite _img, GameObject _displayObject)
    {
        objectTitle.text = _title;
        objectDescription.text = _description;
        objectLevelText.text = _leveltxt;
        previewImage.sprite = _img;
        previewObject = _displayObject;
    }*/

    public void CreateObject(SubCreatorUIManager _manager,string _title, string _description, string _leveltxt, Sprite _img, SubBaseType _base, SubEngineType _engine, SubCannonType _cannon, SubSpecialType _special)
    {
        manager = _manager;
        objectTitle.text = _title;
        objectDescription.text = _description;
        objectLevelText.text = _leveltxt;
        previewImage.sprite = _img;

    }
}
