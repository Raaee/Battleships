using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// script that converts an image from colorful to grayscale and back again
/// </summary>
public class GrayscaleMaker : MonoBehaviour
{
    private Image img;
    private float grayscaleDuration = 0.5f;
    private void Start()
    {
        img = GetComponent<Image>();
       
    }


    public void DoGrayscale()
    {
        Debug.Log("entering");
        StartCoroutine(GrayscaleRoutine(grayscaleDuration, true));
    }

    public void Reset()
    {
        Debug.Log("leaving");
        StartCoroutine(GrayscaleRoutine(grayscaleDuration, false));    }

    private IEnumerator GrayscaleRoutine(float duration, bool isGrayscale)
    {
        float time = 0;
        while (duration > time)
        {
            float durationFrame = Time.deltaTime;
            float ratio = time / duration;
            float grayAmount = isGrayscale ?  ratio : 1 - ratio;
            SetGrayscale(grayAmount);
            time += durationFrame;
            yield return null;
        }
        
        SetGrayscale(isGrayscale ? 1 : 0);
    }


    public void SetGrayscale(float amount = 1)
    {
        Debug.Log("Setting grayscale: " + amount);
        img.material.SetFloat("_GrayscaleAmount", amount); //"_GrayscaleAmount" is from inside the shader script
    }
}
