using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiLevelList : MonoBehaviour
{
    [SerializeField] private UiLevelButton[] visitToggles;

    public void UpdateVisit(int value)
    {
        for (int i = 0; i < value; i++)
        {
            visitToggles[i].Interactable = true;
            visitToggles[i].Visit = true;
        }

        visitToggles[value].Interactable = true;
    }
}
