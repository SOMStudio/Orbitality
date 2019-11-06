using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiEnemyHUD : MonoBehaviour
{
    [SerializeField] private Slider sliderLife;
    
    public void UpdateLife(float value)
    {
        sliderLife.value = value;
    }
}
