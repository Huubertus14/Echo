using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubBaseBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject enginePlace;
    [SerializeField] private GameObject cannonPlace;

    private GameObject engineObject;
    private GameObject cannonObject;

    public GameObject GetEnginePlace => enginePlace;
    public GameObject GetCannonPlace => cannonPlace;

    public GameObject EngineObject
    {
        get { return engineObject; }
        set { engineObject = value; }
    }

    public GameObject CannonObject
    {
        get { return cannonObject; }
        set { cannonObject= value; }
    }
}
