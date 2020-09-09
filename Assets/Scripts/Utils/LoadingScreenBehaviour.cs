using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenBehaviour : MonoBehaviour
{
    [SerializeField] private Image loadImage;
    [SerializeField] private TextMeshProUGUI loadText;

    private ImageFade fade;

    private string message = "";

    private void Awake()
    {
        loadImage = GetComponentInChildren<Image>();
        loadText = GetComponentInChildren<TextMeshProUGUI>();
        fade = GetComponent<ImageFade>();
        fade.HasText = false;
        loadImage.rectTransform.sizeDelta = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        loadText.text = "loadingText";
    }

    private void OnEnable()
    {
        StartCoroutine(LoadingIconAnimation());
        fade.FadeIn(0.2f, false);
    }

    public IEnumerator DisableTimer(float _time)
    {
        fade.FadeOut(_time,false);
        yield return new WaitForSeconds(_time);
        StopAllCoroutines();
        loadText.text = string.Empty;
       // fade.FadeOut(0.4f, false);
        gameObject.SetActive(fade);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        loadText.text = string.Empty;
        //fade.FadeOut(0.4f,false);
    }

    private IEnumerator LoadingIconAnimation()
    {
        while (true)
        {
            loadText.text = "loading.    " + message; //LANGTODO
            yield return new WaitForSeconds(0.4f);
            loadText.text = "loading..   " + message;
            yield return new WaitForSeconds(0.4f);
            loadText.text = "loading...  " + message;
            yield return new WaitForSeconds(0.4f);
        }
    }

    public void SetMessage(string _mess)
    {
        message = _mess;
    }

    public ImageFade GetFade => fade;
}
