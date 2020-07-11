using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFade : MonoBehaviour
{
    private Image fadeImage;
    private bool hasSetImage = false;

    private void Start()
    {
        Image img = GetComponent<Image>();
        FadeImage = img;
    }

    private void SetAlpha(float a)
    {
        a = Mathf.Clamp(a, 0, 1);
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, a);
    }

    /// <summary>
    /// Start the fade, if in == 0 the image will not fade in
    /// is out ==0 it will not fade out
    /// </summary>
    /// <param name="inDuration"></param>
    /// <param name="visibleDuration"></param>
    /// <param name="outDuration"></param>
    public void StartFade(float inDuration, float visibleDuration = 0f, float outDuration = 0)
    {
        if (hasSetImage)
        {
            StartCoroutine(Fade(inDuration, visibleDuration, outDuration));
        }
        else
        {
            Debug.LogWarning("No image set " +gameObject);
        }
    }

    private IEnumerator Fade(float fadeIn, float visible, float fadeOut)
    {
        float fadeValue = 0;
        if (fadeIn != 0)
        {

            while (fadeValue < 1)
            {
                fadeValue += Time.deltaTime / fadeIn;
                SetAlpha(fadeValue);
                yield return 0;
            }

        }
        fadeValue = 0;
        while (fadeValue < 1)
        {
            fadeValue += Time.deltaTime / visible;
            SetAlpha(1);
            yield return 0;
        }

        if (fadeOut != 0)
        {
            fadeValue = 1;
            while (fadeValue > 0)
            {
                fadeValue -= Time.deltaTime / fadeOut;
                SetAlpha(fadeValue);
                yield return 0;
            }
            SetAlpha(0);
        }
        yield return 0;
    }

    public Image FadeImage
    {
        get { return fadeImage; }
        set
        {
            hasSetImage = true;
            fadeImage = value;
            SetAlpha(0);
        }
    }
}
