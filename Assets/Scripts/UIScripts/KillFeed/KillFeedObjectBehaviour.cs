using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KillFeedObjectBehaviour : MonoBehaviour
{
    [Header("Refs:")]
    [SerializeField] private Image killTypeImage;
    [SerializeField] private TextMeshProUGUI killerText;
    [SerializeField] private TextMeshProUGUI victimText;
    [SerializeField] private ImageFade fade;

    private const float lifeTime = 2.6f;

    public void ShowKillFeed(string _killer, string _victim, Sprite _killImage)
    {
        fade.SetAlpha(0);
        killTypeImage.sprite = _killImage;
        killerText.text = _killer;
        victimText.text = _victim;
        fade.FadeIn(0.5f, false);
        StartCoroutine(KillFeedLife());
    }

    private IEnumerator KillFeedLife()
    {
        yield return new WaitForSeconds(lifeTime);
        Remove();
    }

    public void Remove()
    {
        StartCoroutine(RemoveObject());
    }

    private IEnumerator RemoveObject()
    {
        float fadeoutTime = 0.8f;
        //fade.FadeOut(fadeoutTime, fade);
        yield return new WaitForSeconds(fadeoutTime);
        //Dequeue
       // gameObject.SetActive(false);
    }

}
