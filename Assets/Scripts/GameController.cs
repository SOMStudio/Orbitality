using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orbitality.Main
{
    public class GameController : MonoBehaviour, IGravityController
    {
        [Header("Main")]
        
        [Header("Player settings")]
        [SerializeField] private PlayerManager player;
        [SerializeField] private UiPlayerHUD uiPlayerHUD;
        
        [Header("Enemy Settings")]
        [SerializeField] private EnemyManager[] enemy;
        [SerializeField] private UiEnemyHUD[] uiEnemyHUD;

        public static GameController Instance { get; private set; }
        
        private List<IGravityDependent> planetList = new List<IGravityDependent>();

        void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            InitLevel();
        }

        private void InitLevel()
        {
            player.Planet.ChangeLifeEvent += uiPlayerHUD.UpdateLife;
            player.Weapon.ChangeAmmoEvent += uiPlayerHUD.UpdateAmmo;
            player.Weapon.ChangeStateEvent += uiPlayerHUD.UpdateState;

            for (int i = 0; i < enemy.Length; i++)
            {
                enemy[i].Planet.ChangeLifeEvent += uiEnemyHUD[i].UpdateLife;
            }
        }
        
        public void AddPlanet(IGravityDependent planet)
        {
            planetList.Add(planet);
        }
        
        public void RemovePlanet(IGravityDependent planet)
        {
            planetList.Remove(planet);
        }
        
        public Vector3 GetDependencyVector(Transform positionV3)
        {
            Vector3 result = Vector3.zero;

            foreach (var variable in planetList)
            {
                result += variable.GetDependencyVector(positionV3);
            }
            
            return result;
        }
    }
}
