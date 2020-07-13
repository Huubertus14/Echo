using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedCanvasBehaviour : SingetonMonobehaviour<SharedCanvasBehaviour>
{

    [SerializeField] private LoadingScreenBehaviour loadingScreenObject;
    public void SetLoadingScreen(bool _state)
    {
        loadingScreenObject.gameObject.SetActive(_state);
    }

    public void SetLoadingMessage(string message)
    {
        loadingScreenObject.SetMessage(message);
    }
}
