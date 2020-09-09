using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarImageFade : MonoBehaviour
{
    [SerializeField] private Image[] fadeImage;
    [SerializeField] private AnimationCurve curve;

    private float tweenValue;
    private float timeTweenKey;
    private float fadeDuration;

    private void Awake()
    {
        fadeImage = GetComponentsInChildren<Image>();
    }

    private void Start()
    {
        timeTweenKey = 2;
        tweenValue = 0;
    }

    private void Update()
    {
        if (timeTweenKey < 1)
        {
            timeTweenKey += Time.deltaTime / fadeDuration;
            tweenValue = curve.Evaluate(timeTweenKey);
        }
        else
        {
            tweenValue = 0;
        }
        SetAlpha(tweenValue);
    }

    private void SetAlpha(float a)
    {
        a = Mathf.Clamp(a, 0, 1);
        foreach (var item in fadeImage)
        {
            item.color = new Color(item.color.r, item.color.g, item.color.b, a);
        }
    }

    public void StartFade(float duration)
    {
        timeTweenKey = 0;
        fadeDuration = duration;
    }

    public void StopFade()
    {
        timeTweenKey = 2;
        tweenValue = 0;
        SetAlpha(tweenValue);
    }



    public Image[] FadeImage
    {
        get { return fadeImage; }
        set
        {
            fadeImage = value;
            SetAlpha(0);
        }
    }
}
