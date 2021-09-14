using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WorkshopData", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class WorkshopScriptableObject : ScriptableObject
{
	public List<WorkshopItem> WorkshopItems;
}

[System.Serializable]
public class WorkshopItem
{
	public GameObject WorkshopPrefab;
	public Sprite WorkshopImage;
	public string ItemName;
	public string ItemDescription;
	public int Price;
}