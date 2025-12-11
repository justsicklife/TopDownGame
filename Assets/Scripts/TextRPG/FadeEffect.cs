using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FadeState {FadeIn = 0, FadeOut,FadeInOut,FadeLoop}

public class FadeEffect : MonoBehaviour
{

    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime;
    [SerializeField]
    private AnimationCurve fadeCurve;

    FadeState fadeState;

    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnFade(FadeState state,Action onFadeComplete = null)
    {
        gameObject.SetActive(true);

        fadeState = state;

        switch (fadeState)
        {
            case FadeState.FadeIn:
                StartCoroutine(Fade(1, 0,onFadeComplete));
                break;
            case FadeState.FadeOut:
                StartCoroutine(Fade(0, 1,onFadeComplete));
                break;
            case FadeState.FadeInOut:
            case FadeState.FadeLoop:
                StartCoroutine(FadeInOut(onFadeComplete));
                break;
        }
    }
    
    private IEnumerator FadeInOut(Action onFadeComplete = null)
    {
        while (true)
        {
            yield return StartCoroutine(Fade(1, 0));

            yield return StartCoroutine(Fade(0, 1));

            if (fadeState == FadeState.FadeInOut)
            {
                break;
            }
        }

        onFadeComplete?.Invoke();
    }

    private IEnumerator Fade(float start, float end,Action onFadeComplete = null)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = image.color;
            color.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));
            image.color = color;

            yield return null;
        }

        onFadeComplete?.Invoke();
    }

}
