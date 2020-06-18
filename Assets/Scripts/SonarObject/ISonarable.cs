using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISonarable
{
    void HitBySonar(Color col, Vector3 firstParticlePosition);
    void RPC_HitBySonar(Color col, Vector3 firstParticlePosition);
}
