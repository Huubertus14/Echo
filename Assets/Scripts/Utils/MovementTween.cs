using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTween : MonoBehaviour
{
    [Header("Editor Refs:")]
    [SerializeField] [Tooltip("Is set in script if null")]private GameObject beginPointGo;
    private Vector3 beginPoint;
    [SerializeField] private GameObject endPointGo;
    private Vector3 endPoint;

    [Header("Tween Values:")]
    [SerializeField] private AnimationCurve tweenCurve;
    private float tweenDuration = 1;
    private float tweenValue;
    private float timeTweenKey;

    private void Start()
    {
        timeTweenKey = 2;
        if (beginPointGo == null)
        {
            beginPointGo = gameObject;
        }
        beginPoint = beginPointGo.transform.position;
        if (endPointGo == null)
        {
            Debug.LogWarning("No endpoint set", gameObject);
        }
        endPoint = endPointGo.transform.position;
    }

    public void DoTween(float _duration,bool _beginToEnd)
    {
        tweenDuration = _duration;
        StopAllCoroutines();
        if (_beginToEnd)
        {
            StartCoroutine(Tween(beginPoint, endPoint));
        }
        else
        {
            StartCoroutine(Tween(endPoint, beginPoint));
        }
    }

    private IEnumerator Tween(Vector3 _beginPos, Vector3 _endPos)
    {
        timeTweenKey = 0;
        transform.position = _beginPos;
        while (timeTweenKey < 1)
        {
            timeTweenKey += Time.deltaTime / tweenDuration;
            tweenValue = tweenCurve.Evaluate(timeTweenKey);

            transform.position = Vector3.MoveTowards(transform.position, _endPos, tweenValue);

            yield return 0;
        }

        yield return 0;
    }

}
