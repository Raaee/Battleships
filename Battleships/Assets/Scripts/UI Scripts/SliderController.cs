using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SliderController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sliderText = null;
    [SerializeField] private float maxSliderAmt = 100f;


    public void SliderChange(float value)
    {
        float localValue = value * maxSliderAmt;
        sliderText.text = localValue.ToString("0");
    }

}
