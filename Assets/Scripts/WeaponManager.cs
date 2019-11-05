using System;
using System.Collections;
using Orbitality.Main;
using Orbitality.Weapons;
using UnityEngine;

namespace Orbitality.Weapon
{
    public class WeaponManager : ExtendedCustomMonoBehaviour, IWeapon
    {
        private WeaponData weaponData;
        private int currentAmmo;
        private bool isReloading = false;
        private float lastFire = 0;
        private Transform spawnPoint;

        public void InitData(WeaponData weaponDataValue, GameObject spawnPointValue)
        {
            weaponData = weaponDataValue;
            currentAmmo = weaponDataValue.MaxAmmo;
            spawnPoint = spawnPointValue.transform;
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
            float timer = 0;
            while (timer <= weaponData.ReloadTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            currentAmmo = weaponData.MaxAmmo;
            isReloading = false;
        } 
    }
}