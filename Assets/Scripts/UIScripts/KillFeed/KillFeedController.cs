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
    [SerializeField] private GameObject killText; //show local the feedback and xp etc.
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
            _tempFeed.transform.localPosition = Vector3.zero;
            _tempFeed.transform.localRotation = Quaternion.Euler(0,0,0);
            killFeedQueue.Enqueue(_tempFeed.GetComponent<KillFeedObjectBehaviour>());
            _tempFeed.SetActive(false);
        }
    }

    public void CreateKillFeed(string _killer, string _victim, KillType _killType)
    {
        KillFeedObjectBehaviour _temp = killFeedQueue.Dequeue();
        _temp.gameObject.SetActive(true);
        _temp.gameObject.transform.SetAsFirstSibling();
        _temp.ShowKillFeed(_killer, _victim, null);
        killFeedQueue.Enqueue(_temp);
    }

    public void SetKillFeedback(string _kill, int _xp)
    {

    }
}
