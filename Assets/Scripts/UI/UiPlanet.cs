using UnityEngine;
using UnityEngine.UI;

namespace Orbitality.Menu
{
    public class UiPlanet : MonoBehaviour
    {
        [SerializeField] private Slider lifeSlider;

        public void SetLife(float value)
        {
            lifeSlider.value = value;
        }
    }
}