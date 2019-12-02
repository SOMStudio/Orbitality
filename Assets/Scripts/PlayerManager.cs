using System;
using Orbitality.Cursor;
using Orbitality.Weapon;
using SOMStudio.BASE.InputManagement;
using SOMStudio.BASE.InputManagement.Interfaces;
using SOMStudio.Orbitality.InputManagement;
using UnityEngine;

namespace Orbitality.Main
{
    public class PlayerManager : ExtendedCustomMonoBehaviour
    {
        [SerializeField] private GameObject bulletSpawnPoint;

        [SerializeField] private PlanetManager planetManager;
        [SerializeField] private WeaponManager weaponManager;
        [SerializeField] private CursorManager cursorManager;

        private IInputManager inputManager;
        
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

            Planet.Id = id;
            
            Weapon.SetId(id);

            inputManager = new InputManager(new SampleBindings(), new RadialMouseInputHandler());
            inputManager.AddActionToBinding("shoot", Shot);
        }

        private void FixedUpdate()
        {
            CheckForInput();

            UpdateCursor();
        }

        private Vector2 GetPositionOnScreen()
        {
            Vector3 worldToScreenPos = Camera.main.WorldToScreenPoint(myTransform.position);
            return new Vector2(worldToScreenPos.x, worldToScreenPos.y);
        }

        private void CheckForInput()
        {
            inputManager.CheckForInput();

            Vector2 mouseInput = inputManager.GetMouseVector(GetPositionOnScreen());
            myTransform.rotation = Quaternion.Euler(mouseInput);
        }

        private void Shot()
        {
            if (Weapon != null)
            {
                if (Weapon.Shot())
                {
                    SoundManager.Instance?.PlaySoundByIndex(2, myTransform.position);
                }
                else
                {
                    SoundManager.Instance?.PlaySoundByIndex(5, myTransform.position);
                }
            }
        }

        private void UpdateCursor()
        {
            Cursor?.UpdateCursor();
        }
    }
}

