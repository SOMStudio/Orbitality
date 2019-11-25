using Orbitality.Test;
using Orbitality.Weapon;
using Orbitality.Weapons;
using SOMStudio.BASE.InputManagement;
using SOMStudio.Orbitality.InputManagement;
using UnityEngine;
using UnityEngine.UIElements;

namespace Orbitality.Main
{
    public class PlayerManager : ExtendedCustomMonoBehaviour
    {
        [SerializeField] private GameObject bulletSpawnPoint;

        [SerializeField] private PlanetManager planetManager;
        [SerializeField] private WeaponManager weaponManager;
        [SerializeField] private CursorManager cursorManager;

        private InputManager inputManager;
        
        public IPlanet Planet
        {
            get => planetManager;
        }
        
        public IWeapon Weapon
        {
            get => weaponManager;
        }

        public override void Init()
        {
            base.Init();

            SetId(myGO.GetHashCode());

            weaponManager.SetId(id);
            planetManager.SetId(id);

            Weapon.SpawnPoint = bulletSpawnPoint.transform;

            inputManager = new InputManager(new SampleBindings(), new RadialMouseInputHandler());
            inputManager.AddActionToBinding("shoot", Shoot);
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

        private void Shoot()
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
            if (cursorManager != null)
            {
                cursorManager.UpdateCursor();
            }
        }
    }
}

