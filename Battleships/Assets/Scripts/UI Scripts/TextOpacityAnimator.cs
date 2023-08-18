using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextOpacityAnimator : MonoBehaviour
{
  
    private float fadeDuration = 0.75f;
    private TextMeshProUGUI textComponent;

    private void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeLoop());
    }

    private IEnumerator FadeLoop()
    {
        while (true)
        {
            yield return FadeIn();
            yield return FadeOut();
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0;
        Color startColor = textComponent.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1);

        while (elapsedTime < fadeDuration)
        {
            textComponent.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        textComponent.color = endColor;
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0;
        Color startColor = textComponent.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);

        while (elapsedTime < fadeDuration)
        {
            textComponent.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        textComponent.color = endColor;
    }
}
