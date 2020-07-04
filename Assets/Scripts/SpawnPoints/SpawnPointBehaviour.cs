using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointBehaviour : MonoBehaviour
{
   [SerializeField] private List<PlayerBehaviour> playersInRange;

    private void Awake()
    {
        playersInRange = new List<PlayerBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerBehaviour play = other.gameObject.GetComponent<PlayerBehaviour>();
        if (play)
        {
            playersInRange.Add(play);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerBehaviour play = other.gameObject.GetComponent<PlayerBehaviour>();
        if (play)
        {
            if (playersInRange.Contains(play))
            {
                playersInRange.Remove(play);
            }
        }
    }

    public bool CanSpawn()
    {
        return playersInRange.Count < 1;
    }
}
