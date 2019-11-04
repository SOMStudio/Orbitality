using UnityEngine;

namespace Orbitality.Weapons
{
    [CreateAssetMenu(fileName = "New WeaponDataTemplate", menuName = "SOMStudio/Data/Create Weapon Data Template")]
    public class WeaponDataTemplate : ScriptableObject
    {
        [SerializeField] private WeaponData weaponData;

        public WeaponData WeaponData
        {
            get { return weaponData; }
        }
    }
}
