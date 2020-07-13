using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarObject : MonoBehaviour, ISonarable
{
    public bool isHit;
   private int iterator = 0;

    private MeshRenderer rend;

    private static float durationTo = 120f;
    private static float durationFrom = 80;

    public float DurationTo
    {
        get { return durationTo; }
        set { durationTo = value; }
    }

    public float DurationFrom
    {
        get { return durationFrom; }
        set { durationFrom = value; }
    }

    // Use this for initialization
    void Start()
    {
        //All init values
        rend = GetComponent<MeshRenderer>();
        isHit = false;

       // Debug.Log(photonView);
    }

    public bool IsHit
    {
        get { return isHit; }
        set { value = isHit; }
    }


    [PunRPC]
    public void RPC_HitBySonar(Color col, Vector3 firstParticlePosition)
    {
            iterator = 0;

            if (!isHit)
            {
                rend.material.SetVector("_SonarWaveVector", firstParticlePosition);
                rend.material.SetColor("_SonarWaveColor", col);
                StartCoroutine(FadeInOut());
            }
            else
            {
                Color temp = rend.material.GetColor("_SonarWaveColor") + col;
                temp.r = Mathf.Clamp(temp.r, 0f, 1f);
                temp.g = Mathf.Clamp(temp.g, 0f, 1f);
                temp.b = Mathf.Clamp(temp.b, 0f, 1f);
                rend.material.SetColor("_SonarWaveColor", temp);
            }
    }

    IEnumerator FadeInOut()
    {
        isHit = true;
        rend.material.EnableKeyword("VISIBLE");
        for (int i = 0; i < durationTo; i++)
        {
            rend.material.SetFloat("_SonarStep", (float)i / durationTo);
            yield return new WaitForSeconds(1f / 50f);
        }
        yield return null;

        rend.material.DisableKeyword("VISIBLE");
        while (iterator < durationFrom)
        {
            rend.material.SetFloat("_SonarStep", (float)iterator / durationFrom);
            iterator++;
            yield return new WaitForSeconds(1f / 25f);
        }

        isHit = false;

        yield return null;
    }

    public void HitBySonar(Color col, Vector3 firstParticlePosition)
    {
        throw new System.NotImplementedException();
    }
}
