using System;
using UnityEngine;

namespace Orbitality.Weapons
{
	[Serializable]
	public class WeaponData
	{
		[Header("General Characteristics")]
		[SerializeField] private int maxAmmo;
		[SerializeField] private float minFireInterval;
		[Header("Reload")]
		[SerializeField] private float reloadTime;
		[SerializeField] private bool doesAutoReload;
		[Header("Bullet")]
		[SerializeField] private float baseDamage;
		[SerializeField] private float bulletSpeed;
		[SerializeField] private float bulletDestroyTime;
		[SerializeField] private GameObject bulletPrefab;

		public int MaxAmmo
		{
			get { return maxAmmo; }
		}

		public float ReloadTime
		{
			get { return reloadTime; }
		}

		public float BaseDamage
		{
			get { return baseDamage; }
		}

		public GameObject BulletPrefab
		{
			get { return bulletPrefab; }
		}

		public bool DoesAutoReload
		{
			get { return doesAutoReload; }
		}

		public float MinFireInterval
		{
			get { return minFireInterval; }
		}

		public float BulletSpeed
		{
			get { return bulletSpeed; }
		}

		public float BulletDestroyTime
		{
			get { return bulletDestroyTime; }
		}
	}
}
