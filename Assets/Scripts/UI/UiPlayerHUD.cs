using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPlayerHUD : MonoBehaviour
{
    [SerializeField] private Slider sliderLife;
    [SerializeField] private Text ammoText;
    [SerializeField] private Text weaponState;

    public void UpdateLife(float value)
    {
        sliderLife.value = value;
    }

    public void UpdateAmmo(float value1, float value2)
    {
        ammoText.text = String.Format("{0}/{1}", value1, value2);
    }

    public void UpdateState(string value)
    {
        weaponState.text = value;
    }
}
