using Orbitality.Weapon;
using Orbitality.Weapons;
using UnityEngine;

namespace Orbitality.Main
{
    public class PlayerManager : ExtendedCustomMonoBehaviour
    {

        [SerializeField] private WeaponDataTemplate weaponDataTemplate;
        [SerializeField] private GameObject bulletSpawnPoint;

        [SerializeField] private WeaponManager weaponManager;
        [SerializeField] private PlanetManager planetManager;

        public IWeapon Weapon
        {
            get => weaponManager;
        }
        public IPlanet Planet
        {
            get => planetManager;
        }

        public override void Init()
        {
            base.Init();
            
            SetId(myGO.GetHashCode());
            
            weaponManager.InitData(weaponDataTemplate.WeaponData, bulletSpawnPoint);
            
            weaponManager.SetId(id);
            planetManager.SetId(id);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shot(weaponManager);
            }
        }

        private void FixedUpdate()
        {
            myTransform.rotation = UpdateRotation();
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

        private void Shot(IWeapon value)
        {
            if (weaponManager.Shot())
            {
                SoundManager.Instance?.PlaySoundByIndex(2, myTransform.position);
            }
            else
            {
                SoundManager.Instance?.PlaySoundByIndex(5, myTransform.position);
            }
        }
    }
}

