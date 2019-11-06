using Orbitality.Weapon;
using Orbitality.Weapons;
using UnityEngine;

namespace Orbitality.Main
{
    public class EnemyManager : ExtendedCustomMonoBehaviour
    {
        [SerializeField] private WeaponDataTemplate weaponDataTemplate;
        [SerializeField] private GameObject bulletSpawnPoint;

        [SerializeField] private WeaponManager weaponManager;
        [SerializeField] private PlanetManager planetManager;

        public IWeapon Weapon
        {
            get => weaponManager;
        }
        public IPlanet Planet
        {
            get => planetManager;
        }

        public override void Init()
        {
            base.Init();
            
            SetId(myGO.GetHashCode());
            
            weaponManager.InitData(weaponDataTemplate.WeaponData, bulletSpawnPoint);
            
            weaponManager.SetId(id);
            planetManager.SetId(id);
        }
        
        private void Shot(IWeapon value)
        {
            if (weaponManager.Shot())
            {
                SoundManager.Instance?.PlaySoundByIndex(2, myTransform.position);
            }
            else
            {
                SoundManager.Instance?.PlaySoundByIndex(5, myTransform.position);
            }
        }
    }
}