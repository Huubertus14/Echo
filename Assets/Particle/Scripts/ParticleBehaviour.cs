using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour
{
   [SerializeField] private ParticleSystem.MainModule pMain;
    [SerializeField] private ParticleSystem pSystem;
    List<ParticleCollisionEvent> collisionEvents;
    Color particleSystemColor;

    private void Awake()
    {
        pMain = GetComponent<ParticleSystem>().main;
        pSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    public void SetLifeTime(float _value)
    {
        pMain.startLifetime = _value;
    }

    public void SetStartSpeed(float _value)
    {
        pMain.startSpeed = _value;
    }

    public void SetColor(Color col)
    {
        pMain.startColor = col;
        particleSystemColor = col;
    }

    public void PlayParticle()
    {
        pSystem.Play();
    }

    void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(pSystem, other, collisionEvents);
        ISonarable obj = other.GetComponent<ISonarable>();
        if (obj != null)
        {
            Vector3 hitPos = collisionEvents[0].intersection;
            obj.RPC_HitBySonar(particleSystemColor, hitPos);
        }

    }
}
