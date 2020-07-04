using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float rotationSpeed = 0.2f;
    public float movementSpeed = 10.0f;
    public float waterResistence = 0.91f;

    private Rigidbody rb;
    private PlayerBehaviour pb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pb = GetComponent<PlayerBehaviour>();
    }

    private void FixedUpdate()
    {
        ComputerControlls();
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

    public void JoyStickControlls(Joystick joy)
    {
        //Rotation in direction of joystick
        Vector3 dir = new Vector3(-joy.Vertical, 0, joy.Horizontal);

        if (dir != Vector3.zero)
        {
            dir.Normalize();
           transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
            rb.AddForce(transform.right * movementSpeed);
        }
        else
        {
            rb.velocity = rb.velocity * waterResistence;
        }

    }
}
