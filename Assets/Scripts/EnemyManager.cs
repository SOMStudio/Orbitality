using System;
using Orbitality.Cursor;
using Orbitality.Enemy.AI;
using Orbitality.Weapon;
using UnityEngine;

namespace Orbitality.Main
{
    public class EnemyManager : ExtendedCustomMonoBehaviour
    {
        [SerializeField] private GameObject bulletSpawnPoint;

        [SerializeField] private PlanetManager planetManager;
        [SerializeField] private WeaponManager weaponManager;
        [SerializeField] private CursorManager cursorManager;
        
        private IAiInput aiInput;

        public IPlanet Planet
        {
            get => planetManager;
        }
        
        public IWeapon Weapon
        {
            get => weaponManager;
        }

        public ICursor Cursor
        {
            get => cursorManager;
        }

        private void Awake()
        {
            Weapon.SpawnPoint = bulletSpawnPoint.transform;
            
            Cursor.SpawnPoint = bulletSpawnPoint.transform;
        }

        public override void Init()
        {
            base.Init();

            SetId(myGO.GetHashCode());

            planetManager.SetId(id);
            
            weaponManager.SetId(id);

            aiInput = GetComponent<AiManager>();
            aiInput.SetCursor(cursorManager);
            aiInput.Shot += this.Shot;
        }
        
        private void FixedUpdate()
        {
            UpdatePlanet();
            
            UpdateCursor();
        }

        private void UpdatePlanet()
        {
            if (aiInput.RotateAxis != 0)
            {
                myTransform.Rotate(aiInput.RotateVector * Time.deltaTime);
            }

            if (aiInput.IncreaseAxis != 0)
            {
                cursorManager.Length += aiInput.IncreaseLength * Time.deltaTime;
            }
        }

        public void Shot()
        {
            if (Weapon != null)
            {
                if (Weapon.Shot())
                {
                    SoundManager.Instance?.PlaySoundByIndex(2, myTransform.position);
                }
            }
        }

        private void UpdateCursor()
        {
            if (cursorManager != null)
            {
                cursorManager.UpdateCursor();
            }
        }
    }
}