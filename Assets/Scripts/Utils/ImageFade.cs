using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImageFade : MonoBehaviour
{
    [Header("Curves:")]
    [SerializeField] private AnimationCurve fadeInCurve;
    [SerializeField] private AnimationCurve fadeOutCurve;

    [Header("FadeValues:")]
    [SerializeField] private float randomDelayMedian;
    private float minDelay;
    private float maxDelay;

    [Header("Refs:")]
    [SerializeField] private Image fadeImg;
    [SerializeField] private TextMeshProUGUI[] fadeText;

    private bool hasImage, hasText;
    private float fadeDuration;

    private void Awake()
    {
        if (fadeImg == null)
        {
            fadeImg = GetComponentInChildren<Image>();
        }
        if (fadeText == null)
        {
            fadeText = GetComponentsInChildren<TextMeshProUGUI>();
        }

        hasImage = (fadeImg != null);
        hasText = (fadeText != null) && fadeText.Length > 0;
    }

    private void Start()
    {
        minDelay = randomDelayMedian * 0.5f;
        maxDelay = randomDelayMedian * 1.5f;
    }

    public void FadeIn(float _duration, bool randomDelay = true)
    {
        StopAllCoroutines();
        fadeDuration = _duration;
        StartCoroutine(Fade(fadeInCurve, randomDelay, true));
    }
    public void FadeOut(float _duration, bool randomDelay = true)
    {
        StopAllCoroutines();
        fadeDuration = _duration;
        StartCoroutine(Fade(fadeOutCurve, randomDelay, false));
    }

    private IEnumerator Fade(AnimationCurve _curve, bool _delay, bool _fadeIn)
    {
        if (_delay)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }
        float timeTweenKey = 0;
        float tweenValue = 0;
        if (!_fadeIn)
        {
            tweenValue = 1;
        }

        SetAlpha(tweenValue);

        while (timeTweenKey < 1)
        {
            timeTweenKey += Time.deltaTime / fadeDuration;
            tweenValue = fadeInCurve.Evaluate(timeTweenKey);
            SetAlpha(tweenValue);
            yield return 0;
        }

        if (_fadeIn)
        {
            SetAlpha(1);
        }
        else
        {
            SetAlpha(0);
        }
    }

    public void SetAlpha(float _a)
    {
        _a = Mathf.Clamp(_a, 0, 1);
        if (hasImage)
        {
            fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, _a);
        }
        if (hasText)
        {
            for (int i = 0; i < fadeText.Length; i++)
            {
                fadeText[i].color = new Color(fadeText[i].color.r, fadeText[i].color.g, fadeText[i].color.b, _a);
            }
        }
    }

    public bool HasText
    {
        get { return hasText; }
        set { hasText = value; }
    }

    public bool HasImage
    {
        get { return hasImage; }
        set { hasImage = value; }
    }
}
