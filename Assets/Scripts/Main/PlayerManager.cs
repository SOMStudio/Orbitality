using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orbitality.Main
{
    public class PlayerManager : ExtendedCustomMonoBehaviour
    {

        [SerializeField]
        private GameObject bulletPrefab;
        [SerializeField]
        private Transform bulletSpawnPoint;

        private Vector2 positionOnScreen;
        
        public override void Init()
        {
            base.Init();

            Vector3 worlToScreenPos = Camera.main.WorldToScreenPoint(myTransform.position);
            positionOnScreen = new Vector2(worlToScreenPos.x, worlToScreenPos.y);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            }
        }

        private void FixedUpdate()
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 relativeMousePos = new Vector2(mousePos.x - positionOnScreen.x, mousePos.y - positionOnScreen.y);
            float angle = Mathf.Atan2(relativeMousePos.y, relativeMousePos.x) * Mathf.Rad2Deg * -1;
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.up);
            myTransform.rotation = rot;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger Player with: " + other.gameObject.tag);
        }
    }
}

