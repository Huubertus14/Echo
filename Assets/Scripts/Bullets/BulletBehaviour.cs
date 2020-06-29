using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerBehaviour owner;
    private ParticleBehaviour pb;
    [SerializeField]private float force;
    [SerializeField] private float pingInterval;
    private float interval;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pb = GetComponentInChildren<ParticleBehaviour>();

        interval = Time.time + pingInterval;
    }

    public void FireTorpedo(PlayerBehaviour _owner, Vector3 _spawnPos, Quaternion _rotation)
    {
        owner = _owner;
        transform.position = _spawnPos;
        transform.rotation = _rotation;
        transform.Rotate(new Vector3(90,0,0));

        rb.AddForce(transform.up * force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (Time.time > interval)
        {
            Ping();
            interval = Time.time + pingInterval;
        }
    }

    private void Ping()
    {
        pb.PlayParticle();
    }
}
