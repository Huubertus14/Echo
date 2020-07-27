using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaceHolderSubBehaviour : MonoBehaviour
{
    private Renderer[] ren;
    [SerializeField] private Material subMat;
    private int outlineColorID;

    [SerializeField] private GameObject[] shootPoints;

    private void Awake()
    {
        ren = GetComponentsInChildren<Renderer>();
        foreach (var item in ren)
        {
            item.material = subMat;
        }
        outlineColorID = Shader.PropertyToID("_OutlineColor");
    }

    public void ChangeMeshColor(Color _colorToSet)
    {
        foreach (var item in ren)
        {
            item.material.SetColor(outlineColorID, _colorToSet);
        }
    }

    public GameObject[] GetAllShootPoints()
    {
        return shootPoints; 
    }

    public GameObject GetShootPointAt(int _index)
    {
        if (_index >= shootPoints.Length)
        {
            _index = shootPoints.Length - 1;
            Debug.LogWarning("No index exist");
        }
        return shootPoints[_index];
    }
}
