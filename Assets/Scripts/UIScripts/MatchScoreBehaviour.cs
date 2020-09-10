using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MatchScoreBehaviour : MonoBehaviour
{

    private TextMeshProUGUI dataText;
    private ImageFade fade;
    private void Awake()
    {
        dataText = GetComponent<TextMeshProUGUI>();
        fade = GetComponent<ImageFade>();
    }

    private IEnumerator Start()
    {
        fade.SetAlpha(0);
        yield return new WaitForSeconds(0.2f);
        fade.FadeIn(0.9f, false);
    }

    public void SetText(string _pName, string _score)
    {
        dataText.text = _pName + ": " + _score;
    }
}
