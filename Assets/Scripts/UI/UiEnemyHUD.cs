using UnityEngine;
using UnityEngine.UI;

namespace Orbitality.Menu
{
    public class UiEnemyHUD : MonoBehaviour
    {
        [SerializeField] private Slider sliderLife;

        public void UpdateLife(float value)
        {
            sliderLife.value = value;
        }
    }
}
