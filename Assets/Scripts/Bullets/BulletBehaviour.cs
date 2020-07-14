using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviourPun
{
    private Rigidbody rb;
    private PlayerBehaviour owner;
    private SonarPool sonarPool;
    [Header("Bullet values")]
    [SerializeField] private float force;
    [SerializeField] private float bulletDamage;
    [Space]
    [SerializeField] private float pingInterval;
    [SerializeField] private float bulletPingSpeed;
    [SerializeField] private float bulletPingLifetime;
    private float interval;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sonarPool = GetComponent<SonarPool>();

        interval = Time.time + pingInterval;
    }

    public void CreatePool(string _name, Color _playerColor)
    {
        sonarPool.CreatePool("Bullet (" + _name + ")");
        sonarPool.SetPoolColor(_playerColor);
    }

    public void FireTorpedo(PlayerBehaviour _owner, Vector3 _spawnPos, Quaternion _rotation, float lag)
    {
        rb.Sleep();
        owner = _owner;
        transform.position = _spawnPos;
        transform.rotation = _rotation;
        transform.Rotate(new Vector3(90, 0, 0));

        rb.AddForce(transform.up * force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        PlayerHealth ph = collision.gameObject.GetComponent<PlayerHealth>();
        if (ph)
        {
            if (ph.GetPlayer.IsAlive)
            {
                ph.PlayerHit(owner, bulletDamage);
            }
        }
    }

    private void FixedUpdate()
    {
        if (Time.time > interval)
        {
            sonarPool.CreateSonar(transform.position, bulletPingLifetime, bulletPingSpeed);
            interval = Time.time + pingInterval;
        }
    }

    public void DestroySonarPool()
    {
        sonarPool.DestroyPool();
    }
      

}
