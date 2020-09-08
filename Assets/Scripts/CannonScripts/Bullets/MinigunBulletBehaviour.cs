using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunBulletBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerBehaviour owner;

    [Header("Bullet values")]
    [SerializeField] private float force;
    [SerializeField] private float bulletDamage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FireBullet(PlayerBehaviour _owner, Vector3 _spawnPos, Quaternion _rotation, float lag)
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
}
