using System;
using Orbitality.Weapons;
using UnityEngine;

namespace Orbitality.Weapon
{
    public interface IWeapon
    {
        WeaponData WeaponData { get; set; }
        Transform SpawnPoint { get; set; }
        bool Shot();
        void Reload();
        event Action<float, float> ChangeAmmoEvent;
        event Action<string> ChangeStateEvent;
    }
}