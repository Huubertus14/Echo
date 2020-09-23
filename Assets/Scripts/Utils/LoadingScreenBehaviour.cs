using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenBehaviour : MonoBehaviour
{
    [SerializeField] private Image loadImage;
    [SerializeField] private TextMeshProUGUI loadText;
    [SerializeField] private ParticleBehaviour partB;

    private ImageFade fade;

    private string message = "";

    private void Awake()
    {
        loadImage = GetComponentInChildren<Image>();
        loadText = GetComponentInChildren<TextMeshProUGUI>();
        fade = GetComponent<ImageFade>();
        fade.HasText = false;
        loadImage.rectTransform.sizeDelta = new Vector2(Screen.currentResolution.width * 1.1f, Screen.currentResolution.height * 1.1f);
        loadText.text = "loadingText";
    }

    private void OnEnable()
    {
        StartCoroutine(LoadingIconAnimation());
        StartCoroutine(PingLoading());
        fade.FadeIn(0.2f, false);
        partB.gameObject.SetActive(true);
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
        partB.gameObject.SetActive(false);
        //fade.FadeOut(0.4f,false);
    }

    private IEnumerator PingLoading()
    {
        float particleRate = 2.5f;
        float timer = 0;

        partB.PlayParticle();
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > particleRate)
            {
                timer = 0;
                partB.gameObject.transform.position = new Vector3(Random.Range(-125f, 125f), Random.Range(-125f, 125f), partB.transform.position.z);
                partB.PlayParticle();
                    
            }
            yield return 0;
        }
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
