using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Runtime.InteropServices;

public class KillFeedObjectBehaviour : MonoBehaviour
{
    [Header("Refs:")]
    [SerializeField] private Image killTypeImage;
    [SerializeField] private TextMeshProUGUI killerText;
    [SerializeField] private TextMeshProUGUI victimText;

    private const float lifeTime = 2.6f;
    private const float fadeInTime = 0.4f;
    private const float fadeOutTime = 0.8f;
    private float fadeTweenkey = 2;
    private bool visible = false;

    [Header("Fade values")]
    [SerializeField] private AnimationCurve fadeInCurve;
    [SerializeField] private AnimationCurve fadeOutCurve;

    


    public void ShowKillFeed(string _killer, string _victim, Sprite _killImage)
    {
        SetAlpha(0);
        killTypeImage.sprite = _killImage;
        killerText.text = _killer;
        victimText.text = _victim;
        StartCoroutine(KillFeedLife());
    }

    private IEnumerator KillFeedLife()
    {
        visible = true;
        float _alpha = 0;
        fadeTweenkey = 0;
        while (fadeTweenkey < 1)
        {
            fadeTweenkey += Time.deltaTime / fadeInTime;
            _alpha = fadeInCurve.Evaluate(fadeTweenkey);
            SetAlpha(_alpha);
            yield return 0;
        }

        yield return new WaitForSeconds(lifeTime);

        fadeTweenkey = 0;
        while (fadeTweenkey < 1)
        {
            fadeTweenkey += Time.deltaTime / fadeOutTime;
            _alpha = fadeOutCurve.Evaluate(fadeTweenkey);
            SetAlpha(_alpha);
            yield return 0;
        }

        SetAlpha(0);
        visible = false;
    }

    public void Remove()
    {
        StopAllCoroutines();
        SetAlpha(0);
        visible = false;
    }

    public void SetAlpha(float _a)
    {
        _a = Mathf.Clamp(_a,0,1);
        killTypeImage.color = new Color(killTypeImage.color.r, killTypeImage.color.g, killTypeImage.color.b, _a);
        killerText.color = new Color(killerText.color.r, killerText.color.g, killerText.color.b, _a);
        victimText.color = new Color(victimText.color.r, victimText.color.g, victimText.color.b, _a);
    }

    public bool Visible => visible;

}
