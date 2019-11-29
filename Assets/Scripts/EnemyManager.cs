using Orbitality.Enemy.AI;
using Orbitality.Test;
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

        private AiManager aiManager;

        public IPlanet Planet
        {
            get => planetManager;
        }
        
        public IWeapon Weapon
        {
            get => weaponManager;
        }

        public CursorManager Cursor
        {
            get => cursorManager;
        }

        public override void Init()
        {
            base.Init();

            SetId(myGO.GetHashCode());

            planetManager.SetId(id);
            
            weaponManager.SetId(id);
            Weapon.SpawnPoint = bulletSpawnPoint.transform;
        }
        
        private void FixedUpdate()
        {
            UpdateCursor();
        }

        private Vector2 GetPositionOnScreen()
        {
            Vector3 worldToScreenPos = Camera.main.WorldToScreenPoint(myTransform.position);
            return new Vector2(worldToScreenPos.x, worldToScreenPos.y);
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