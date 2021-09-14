using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingController : MonoBehaviour
{
    public static LoadingController Instance;

	[SerializeField]private GameObject _loadingScreen;

    void Awake()
    {
		if(Instance!= null)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
		}
    }

   public void ToggleLoadingScreen(bool toggle)
	{
		_loadingScreen.SetActive(toggle);
	}

}
