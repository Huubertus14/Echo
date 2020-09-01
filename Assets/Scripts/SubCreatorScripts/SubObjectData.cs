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

    SubBaseType baseType = SubBaseType.None;
    SubCannonType cannonType = SubCannonType.None;
    SubEngineType engineType = SubEngineType.None;
    SubSpecialType specialType = SubSpecialType.None;
    
    public void CreateObject(SubCreatorUIManager _manager,string _title, string _description, string _leveltxt, Sprite _img, SubBaseType _base)
    {
        manager = _manager;
        objectTitle.text = _title;
        objectDescription.text = _description;
        objectLevelText.text = _leveltxt;
        previewImage.sprite = _img;

        baseType = _base;
    }

    public void CreateObject(SubCreatorUIManager _manager, string _title, string _description, string _leveltxt, Sprite _img, SubEngineType _engine)
    {
        manager = _manager;
        objectTitle.text = _title;
        objectDescription.text = _description;
        objectLevelText.text = _leveltxt;
        previewImage.sprite = _img;

        engineType = _engine;

    }

    public void CreateObject(SubCreatorUIManager _manager, string _title, string _description, string _leveltxt, Sprite _img, SubCannonType _cannon)
    {
        manager = _manager;
        objectTitle.text = _title;
        objectDescription.text = _description;
        objectLevelText.text = _leveltxt;
        previewImage.sprite = _img;

        cannonType = _cannon;
    }

    public void CreateObject(SubCreatorUIManager _manager, string _title, string _description, string _leveltxt, Sprite _img, SubSpecialType _special)
    {
        manager = _manager;
        objectTitle.text = _title;
        objectDescription.text = _description;
        objectLevelText.text = _leveltxt;
        previewImage.sprite = _img;

        specialType = _special;
    }

    public void SelectType()
    {
        if (baseType != SubBaseType.None)
        {
            SubCreatorManager.SP.ChangeComponent(baseType);
        }
        if (engineType != SubEngineType.None)
        {
            SubCreatorManager.SP.ChangeComponent(engineType);
        }
        if (cannonType != SubCannonType.None)
        {
            SubCreatorManager.SP.ChangeComponent(cannonType);
        }
        if (specialType != SubSpecialType.None)
        {
            SubCreatorManager.SP.ChangeComponent(specialType);
        }
    }
}
