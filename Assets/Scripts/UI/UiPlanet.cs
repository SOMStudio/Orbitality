using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPlanet : MonoBehaviour
{
    [SerializeField] private Slider lifeSlider;

    public void SetLife(float value)
    {
        lifeSlider.value = value;
    }
}
