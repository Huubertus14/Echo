using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using UnityEditor;

public class PlayerMovement : MonoBehaviour, IPunObservable
{
    [SerializeField] private GameObject subMesh;
    [Space]
    //Engine settings
    [SerializeField] private float accelerationSpeed = 10.0f;
    [SerializeField] private float maxSpeed = 10.0f;

    //Base settings
    [SerializeField] private float waterResistence = 0.91f;

    private Rigidbody rb;
    private PlayerBehaviour pb;
    private bool hasRigidbody = false;

    private Quaternion networkRotation;
    private float angle = 0;
    private bool firstTake = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pb = GetComponent<PlayerBehaviour>();
        hasRigidbody = true;
    }

    private void OnEnable()
    {
        firstTake = true;
    }

    public void GiveValues(SubBaseSettings _base, SubEnineSettings _engine)
    {
        accelerationSpeed = _engine.acceleration;
        maxSpeed = _engine.maxVelocity;
        waterResistence = _base.resistence;
    }

    private void Update()
    {
        if (hasRigidbody)
        {
            SubMeshRotation();
        }
    }

    public void WaterResistance()
    {
        rb.velocity = rb.velocity * waterResistence;
    }

    public void BounceAway(Vector3 _orginPosition, float _force)
    {
        Vector3 _bounceDir = _orginPosition - subMesh.transform.position;
        _bounceDir.Normalize();
        rb.AddForce(_bounceDir*_force, ForceMode.Impulse);
    }

    private void SubMeshRotation()
    {
        if (!pb.photonView.IsMine)
        {
            subMesh.transform.rotation = Quaternion.RotateTowards(subMesh.transform.rotation, networkRotation, angle * 1.0f / PhotonNetwork.SerializationRate);
        }
    }

    public void Accelerate()
    {
        if (rb.velocity.magnitude < pb.EngineSettings.maxVelocity)
        {
            //Debug.Log(rb.velocity);
            rb.AddForce(subMesh.transform.right * accelerationSpeed);
        }
        else
        {
            //Slow down sub a little bit
            WaterResistance();
        }
    }


    public void JoyStickControlls(Joystick joy)
    {
        //Rotation in direction of joystick
        Vector3 dir = new Vector3(-joy.Vertical, 0, joy.Horizontal);

        if (dir != Vector3.zero)
        {
            dir.Normalize();
            subMesh.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(subMesh.transform.rotation);
        }
        else
        {
            networkRotation = (Quaternion)stream.ReceiveNext();
            if (firstTake)
            {
                angle = 0;
                subMesh.transform.rotation = networkRotation;
            }
            else
            {
                angle = Quaternion.Angle(subMesh.transform.rotation, networkRotation);
            }
        }
    }

    private void OnDestroy()
    {
        hasRigidbody = false;
    }

}
