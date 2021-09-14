using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class WorkshopTypeSelectButton : MonoBehaviour
{
	[SerializeField]private string _sectionName;
	[SerializeField] private string _scriptableLoadName;

	private WorkshopScriptableObject _loadedObjects;

	private void Awake()
	{
		GetComponent<Button>().onClick.AddListener(OnClick);
		_loadedObjects = Resources.Load<WorkshopScriptableObject>($"Workshop/{_scriptableLoadName}");

		//Create a list of items and dissable it
	}

	private void OnClick()
	{
		//Set this as active
	}
}
