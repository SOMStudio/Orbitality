using System;
using System.Collections;
using Orbitality.Main;
using Orbitality.Weapons;
using UnityEngine;

namespace Orbitality.Weapon
{
    public class WeaponManager : ExtendedCustomMonoBehaviour, IWeapon
    {
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private Transform spawnPoint;
        private int currentAmmo;
        private bool isReloading = false;
        private float lastFire = 0;

        public event Action<float, float> ChangeAmmoEvent;
        public event Action<string> ChangeStateEvent;
        
        public void InitData(WeaponData weaponDataValue, GameObject spawnPointValue)
        {
            WeaponData = weaponDataValue;
            SpawnPoint = spawnPointValue.transform;
            
            ChangeStateEvent?.Invoke("Active");
        }

        public WeaponData WeaponData
        {
            get => weaponData;
            set
            {
                weaponData = value;
                currentAmmo = weaponData.MaxAmmo;
            }
        }
        
        public Transform SpawnPoint
        {
            get => spawnPoint;
            set => spawnPoint = value;
        }

        public bool Shot()
        {
            if (lastFire + weaponData.MinFireInterval > Time.time)
            {
                return false;
            }
			
            if (isReloading)
            {
                return false;
            }
			
            if (currentAmmo > 0)
            {
                currentAmmo--;
                lastFire = Time.time;
                SpawnBullet();
                
                ChangeAmmoEvent?.Invoke(currentAmmo, weaponData.MaxAmmo);
                
                return true;
            } 
            if (currentAmmo <= 0 && weaponData.DoesAutoReload)
            {
                Reload();
                return false;
            }
            else
            {
                return false;
            }
        }

        private void SpawnBullet()
        {
            GameObject newBullet = Instantiate(weaponData.BulletPrefab, spawnPoint.position, spawnPoint.rotation);
            
            RocketManager bulletManager = newBullet.GetComponent<RocketManager>();
            bulletManager.Speed = weaponData.BulletSpeed;
            bulletManager.Damage = weaponData.BaseDamage;
            bulletManager.DestroyTime = weaponData.BulletDestroyTime;
            
            bulletManager.AddIgnore(id);
            bulletManager.InitDestroyAfterTime();
        }

        public void Reload()
        {
            isReloading = true;
            StartCoroutine(ReloadTimer());
        }

        private IEnumerator ReloadTimer()
        {
            ChangeStateEvent?.Invoke("Reload");
            
            float timer = 0;
            while (timer <= weaponData.ReloadTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            currentAmmo = weaponData.MaxAmmo;
            
            ChangeAmmoEvent?.Invoke(currentAmmo, weaponData.MaxAmmo);
            ChangeStateEvent?.Invoke("Active");
            
            isReloading = false;
        } 
    }
}