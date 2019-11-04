using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orbitality.Main
{
    public class PlayerManager : ExtendedCustomMonoBehaviour
    {

        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private PlanetManager planetManager;
        
        public override void Init()
        {
            base.Init();
            
            planetManager.SetId(id);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                InstantiateBullet();
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

        private void InstantiateBullet()
        {
            GameObject newRacket = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            IIgnoreable racketIgnoreList = newRacket.GetComponent<IIgnoreable>();
            racketIgnoreList.AddIgnore(id);
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

