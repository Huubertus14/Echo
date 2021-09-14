using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapInitializer : MonoBehaviour
{
    void Awake()
    {
        SceneManager.sceneLoaded += FirstSceneLoaded;
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);//Load menu scene

        //Load loading screen scene
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
    }

	private void FirstSceneLoaded(Scene arg0, LoadSceneMode arg1)
	{
        SceneManager.sceneLoaded -= FirstSceneLoaded;
        Debug.Log($"Scene Loaded");
    }
}
