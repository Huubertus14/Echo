using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KillType
{
    Collision,
    MiniGun,
    Torpedo,
    CollisionWithRock,
    Suicide,
    Ram
}

public class KillFeedController : SingetonMonobehaviour<KillFeedController>
{
    [Header("Refs:")]
    [SerializeField] private GameObject killFeedPrefab; //Used in the killfeed on all clients
    [SerializeField] private GameObject killTextPrefab; //show local the feedback and xp etc.
    [Space]
    [SerializeField] private GameObject killFeedParent;

    private Queue<KillFeedObjectBehaviour> killFeedQueue;

    protected override void Awake()
    {
        base.Awake();
        killFeedQueue = new Queue<KillFeedObjectBehaviour>();
        for (int i = 0; i < 8; i++)//make 8 things
        {
            GameObject _tempFeed = Instantiate(killFeedPrefab, transform.position, Quaternion.identity, killFeedParent.transform);
            killFeedQueue.Enqueue(_tempFeed.GetComponent<KillFeedObjectBehaviour>());
            _tempFeed.SetActive(false);
        }
    }

    public void CreateKillFeed(string _killer, string _victim, KillType _killType)
    {
        KillFeedObjectBehaviour _temp = killFeedQueue.Dequeue();
        _temp.gameObject.SetActive(true);
        _temp.ShowKillFeed(_killer, _victim, null);
        killFeedQueue.Enqueue(_temp);
    }
}
