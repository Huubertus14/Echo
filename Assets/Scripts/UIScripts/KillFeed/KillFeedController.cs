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
    [SerializeField] private GameObject feedbackParent;

    private Queue<KillFeedObjectBehaviour> killFeedQueue;
    private Queue<KillFeedback> feedbackQueue;

    protected override void Awake()
    {
        base.Awake();
        killFeedQueue = new Queue<KillFeedObjectBehaviour>();
        feedbackQueue = new Queue<KillFeedback>();
        for (int i = 0; i < 8; i++)//make 8 things
        {
            GameObject _tempFeed = Instantiate(killFeedPrefab, transform.position, Quaternion.identity, killFeedParent.transform);
            _tempFeed.transform.localPosition = Vector3.zero;
            _tempFeed.transform.localRotation = Quaternion.Euler(0, 0, 0);
            killFeedQueue.Enqueue(_tempFeed.GetComponent<KillFeedObjectBehaviour>());
            _tempFeed.SetActive(false);
        }

        for (int i = 0; i < 4; i++) //make 4 killfeedback things
        {
            GameObject _feedback = Instantiate(killTextPrefab, transform.position, Quaternion.identity, feedbackParent.transform);
            _feedback.transform.localPosition = Vector3.zero;
            _feedback.transform.localRotation = Quaternion.Euler(0, 0, 0);
            feedbackQueue.Enqueue(_feedback.GetComponent<KillFeedback>());
            _feedback.SetActive(false);
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
        KillFeedback fb = feedbackQueue.Dequeue();
        fb.gameObject.SetActive(true);
        fb.transform.SetAsFirstSibling();
        fb.SetText(_kill, _xp);
        feedbackQueue.Enqueue(fb);
    }
}
