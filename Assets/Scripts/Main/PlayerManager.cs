using System;
using System.Collections;
using System.Collections.Generic;
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
                weaponManager.Shot();
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
    }
}

