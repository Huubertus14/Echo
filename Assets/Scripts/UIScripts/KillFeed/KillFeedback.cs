using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class KillFeedback : MonoBehaviour
{
    [SerializeField] private AnimationCurve fadeInCurve;
    [SerializeField] private AnimationCurve fadeOutCurve;
    private TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "";
    }

    public void SetText(string _message, int _xp)
    {
        StopAllCoroutines();
        SetAlpha(0);
        text.text = _message + " +" + _xp.ToString();
        StartCoroutine(FeedbackLife());
    }

    public void SetAlpha(float _a)
    {
        _a = Mathf.Clamp(_a,0,1);
        text.color = new Color(text.color.r, text.color.g, text.color.b, _a);
    }

    private IEnumerator FeedbackLife()
    {
        float _tweenkey = 0;
        float _alpha = 0;
        while (_tweenkey < 1)
        {
            _tweenkey += Time.deltaTime / 0.5f;
            _alpha = fadeInCurve.Evaluate(_tweenkey);
            SetAlpha(_alpha);
            yield return 0;
        }

        yield return new WaitForSeconds(0.9f);
        _tweenkey = 0;
        while (_tweenkey <1)
        {
            _tweenkey += Time.deltaTime / 0.3f;
            _alpha = fadeOutCurve.Evaluate(_tweenkey);
            SetAlpha(_alpha);
            yield return 0;
        }
        yield return 0;
        SetAlpha(0);
    }
}
