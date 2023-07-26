using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class BlurPanel : Image
{
    //Rae please dont touch, I used a tutorial I barely know whaats going on myself
    public bool animate;
    private float time = 0.75f;
    public float delay = 0f;
    private float currentValue = 0f;

    CanvasGroup canvas;


    protected override void Awake()
    {
        canvas = GetComponent<CanvasGroup>();
    }

    protected override void Reset()
    {
        base.Reset();
        color = Color.black * 0.1f;
    }

    protected override void OnEnable()
    {
        if(Application.isPlaying)
        {
            material.SetFloat("_Size", 0);
            canvas.alpha = 0;

            StartCoroutine(UpdateBlurRoutine());
        }
    }


    void UpdateBlur(float value)
    {
        material.SetFloat("_Size", value);
        canvas.alpha = value;
    }

    private IEnumerator UpdateBlurRoutine()
    {
        for(float elaspedTime = 0.0f; elaspedTime < time; elaspedTime += Time.deltaTime)
        {
            currentValue = Mathf.Lerp(0.0f, 1.0f, elaspedTime / time);
            UpdateBlur(currentValue*2);
            yield return null; // Wait for the next frame
        }
        var colorthing = GetComponent<Image>().color;
        colorthing.a = 0.5f;
        GetComponent<Image>().color = colorthing;

    }
}
