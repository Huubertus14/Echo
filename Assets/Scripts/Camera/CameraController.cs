using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : SingetonMonobehaviour<CameraController>
{
    [SerializeField]
    private Vector3 offSet = new Vector3(0, 15, 0);

    [SerializeField]
    private Transform target;

    bool hasTarget = false;

    private void Start()
    {
        if (target != null)
        {
            hasTarget = true;
        }
        else
        {
           // Debug.LogError("No target specified", gameObject);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (hasTarget) //use bool here as null checks are expensive.
        {
            transform.position = target.position + offSet;
        }
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }
}
