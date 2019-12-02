using System.Collections.Generic;
using Orbitality.Menu;
using Orbitality.Weapons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Orbitality.Main
{
    public class GameController : BaseGameController, IGravityController
    {
        [Header("Main")]
        [SerializeField] private WeaponDataTemplate[] weapon;
        
        [Header("Player settings")]
        [SerializeField] private PlayerManager player;
        [SerializeField] private UiPlayerHUD uiPlayerHUD;
        
        [Header("Enemy Settings")]
        [SerializeField] private EnemyManager[] enemy;
        [SerializeField] private UiEnemyHUD[] uiEnemyHUD;
        
        public static GameController Instance { get; private set; }
        
        private readonly List<IGravityDependent> planetList = new List<IGravityDependent>();

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
            player.Planet.ChangeLifeEvent += CheckLifePlayer;
            player.Weapon.ChangeAmmoEvent += uiPlayerHUD.UpdateAmmo;
            player.Weapon.ChangeStateEvent += uiPlayerHUD.UpdateState;

            var playerWeapon = weapon[Random.Range(0, weapon.Length)].WeaponData;
            player.Weapon.WeaponData = playerWeapon;
            player.Cursor.SpeedMove = playerWeapon.BulletSpeed;
            
            for (int i = 0; i < enemy.Length; i++)
            {
                enemy[i].Planet.ChangeLifeEvent += uiEnemyHUD[i].UpdateLife;
                enemy[i].Planet.ChangeLifeEvent += CheckLifeEnemy;
                
                var enemyWeapon = weapon[Random.Range(0, weapon.Length)].WeaponData;
                enemy[i].Weapon.WeaponData = enemyWeapon;
                enemy[i].Cursor.SpeedMove = enemyWeapon.BulletSpeed;
            }
        }

        public override void PlayerDestroyed()
        {
            base.PlayerDestroyed();
            
            LoadMenu();
        }

        public override void EnemyDestroyed()
        {
            base.EnemyDestroyed();

            LoadMenu();
        }

        private void CheckLifePlayer(float value)
        {
            if (value == 0)
            {
                PlayerDestroyed();
            }
        }

        private void CheckLifeEnemy(float value)
        {
            
            for (int i = 0; i < enemy.Length; i++)
            {
                if (enemy[i].Planet.Life > 0)
                {
                    return;
                }
            }
            
            EnemyDestroyed();
        }

        private void LoadMenu()
        {
            SceneManager.LoadScene("Main");
        }
        
        public void AddPlanet(IGravityDependent planet)
        {
            planetList.Add(planet);
        }
        
        public void RemovePlanet(IGravityDependent planet)
        {
            planetList.Remove(planet);
        }
        
        public Vector3 GetDependencyVector(Vector3 positionV3)
        {
            Vector3 result = Vector3.zero;

            foreach (var variable in planetList)
            {
                result += variable.GetDependencyVector(positionV3);
            }
            
            return result;
        }

        public void PauseGame()
        {
            Paused = !Paused;
        }
    }
}
