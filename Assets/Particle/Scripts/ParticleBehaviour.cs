using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour {
    private ParticleSystem PSystem;
    List<ParticleCollisionEvent> collisionEvents;

    

    // Use this for initialization
    void Start() {
        PSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(PSystem, other, collisionEvents);
        SonarObject obj = other.GetComponent<SonarObject>();
        if (obj != null)
        {
            Debug.Log("Hit sonar");
            Vector3 hitPos = collisionEvents[0].intersection;
            obj.HitBySonar(Color.red, hitPos);
        }

    }
}
