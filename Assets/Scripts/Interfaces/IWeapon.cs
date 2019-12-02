using System;
using Orbitality.Weapons;
using UnityEngine;

namespace Orbitality.Weapon
{
    public interface IWeapon
    {
        void SetId(int val);
        WeaponData WeaponData { set; }
        Transform SpawnPoint { set; }
        bool Shot();
        void Reload();
        event Action<float, float> ChangeAmmoEvent;
        event Action<string> ChangeStateEvent;
    }
}