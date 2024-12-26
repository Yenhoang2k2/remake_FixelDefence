using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SliderWave : MonoBehaviour
{
    [SerializeField] private Image sliderWave;
    [SerializeField] private Text txtWave;

    public void SetSlider(float sliderCurrent, int txt)
    {
        sliderWave.fillAmount = sliderCurrent / 1f;
        txtWave.text = "Wave" + txt;
    }
}
