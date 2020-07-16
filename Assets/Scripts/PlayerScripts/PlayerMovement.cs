using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class PlayerMovement : MonoBehaviour, IPunObservable
{
    [SerializeField] private GameObject subMesh;
    [Space]
    public float rotationSpeed = 0.2f;
    public float movementSpeed = 10.0f;
    public float waterResistence = 0.91f;

    private Rigidbody rb;
    private PlayerBehaviour pb;

    private Quaternion networkRotation;
    private float angle = 0;
    private bool firstTake = false;

    private bool hasRigidbody = false;

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

    private void Update()
    {
        if (hasRigidbody)
        {
            ComputerControlls();
            SubMeshRotation();
        }
    }

    public void WaterResistance()
    {
        rb.velocity = rb.velocity * waterResistence;
    }

    private void ComputerControlls()
    {
        //transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * rotationSpeed, 0));
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeTorque(transform.up * -rotationSpeed, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeTorque(transform.up * rotationSpeed, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(transform.right * -movementSpeed);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pb.Ping();
        }

    }

    private void SubMeshRotation()
    {
        if (!pb.photonView.IsMine)
        {
            subMesh.transform.rotation = Quaternion.RotateTowards(subMesh.transform.rotation, networkRotation, angle * 1.0f/PhotonNetwork.SerializationRate);
        }
    }

    public void Accelerate()
    {
        if (rb.velocity.x < pb.Settings.maxVelocity && rb.velocity.z < pb.Settings.maxVelocity)
        {
            //Debug.Log(rb.velocity);
            rb.AddForce(subMesh.transform.right * movementSpeed);
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
