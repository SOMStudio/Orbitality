using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orbitality.Test1
{
    public class PlayerManager : MonoBehaviour
    {

        [SerializeField]
        private GameObject bulletPrefab;
        [SerializeField]
        private Transform bulletSpawnPoint;
        
        void Start()
        {
        
        }
        
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            }
        }
    }
}

