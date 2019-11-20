using Orbitality.Test;
using Orbitality.Weapon;
using Orbitality.Weapons;
using UnityEngine;

namespace Orbitality.Main
{
    public class PlayerManager : ExtendedCustomMonoBehaviour
    {
        [SerializeField] private GameObject bulletSpawnPoint;

        [SerializeField] private PlanetManager planetManager;
        [SerializeField] private WeaponManager weaponManager;
        [SerializeField] private CursorManager cursorManager;

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
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shot();
            }
        }

        private void FixedUpdate()
        {
            myTransform.rotation = UpdateRotation();
            
            UpdateCursor();
        }

        private Vector2 GetPositionOnScreen()
        {
            Vector3 worldToScreenPos = Camera.main.WorldToScreenPoint(myTransform.position);
            return new Vector2(worldToScreenPos.x, worldToScreenPos.y);
        }

        private Quaternion UpdateRotation()
        {
            Vector2 mousePos = Input.mousePosition;
            Vector3 positionOnScreen = GetPositionOnScreen();
            Vector2 relativeMousePos = new Vector2(mousePos.x - positionOnScreen.x, mousePos.y - positionOnScreen.y);
            float angle = Mathf.Atan2(relativeMousePos.y, relativeMousePos.x) * Mathf.Rad2Deg * -1;
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.up);
            return rot;
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
            if (cursorManager != null)
            {
                cursorManager.UpdateCursor();
            }
        }
    }
}

