using System.Collections.Generic;
using UnityEngine;

namespace Orbitality.Main
{
    public class RocketManager : ExtendedCustomMonoBehaviour, IDamageable, IIgnoreable
    {
        [SerializeField] private float speed = 10.0f;
        [SerializeField] private float damage = 1.0f;
        [SerializeField] private float destroyTime = 10.0f;
        
        private List<int> ignorePlanetList = new List<int>();
        
        private Vector3 vectorMove;
        private IGravityController gravityController;

        public float Speed
        {
            get => speed;
            set => speed = value;
        }
        
        public float Damage
        {
            get => damage;
            set => damage = value;
        }

        public float DestroyTime
        {
            get => destroyTime;
            set => destroyTime = value;
        }

        public override void Init()
        {
            base.Init();
            
            gravityController = GameController.Instance;
            
            vectorMove = myTransform.forward;
        }

        void Update()
        {
            myTransform.position = UpdatePosition();
            myTransform.rotation = UpdateRotation();
        }
        
        private Vector3 UpdatePosition()
        {
            Vector3 speedVector = vectorMove * Speed;
            Vector3 dependVector = gravityController.GetDependencyVector(myTransform);

            if (dependVector != Vector3.zero)
            {
                Vector3 speedVectorNew = speedVector + dependVector;
                speedVector = Vector3.Lerp(speedVector, speedVectorNew, Time.deltaTime);
                
                vectorMove = speedVector.normalized;
                speedVector = vectorMove * Speed;
            }
            
            return  myTransform.position + speedVector * Time.deltaTime;
        }

        private Quaternion UpdateRotation()
        {
            return  Quaternion.LookRotation(vectorMove, Vector3.up);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Planet"))
            {
                DestroyObject();
            }
        }

        private void DestroyObject()
        {
            Destroy(myGO);
            
            SoundManager.Instance?.PlaySoundByIndex(6, myTransform.position);
        }

        public void InitDestroyAfterTime()
        {
            Invoke(nameof(DestroyObject), DestroyTime);
        }

        private void FuelOff()
        {
            Speed = 5;
        }

        public void SetDamage(float value)
        {
            Damage = value;
        }

        public float GetDamage()
        {
            return Damage;
        }

        public void AddIgnore(int value)
        {
            ignorePlanetList.Add(value);
        }

        public void RemoveIgnore(int value)
        {
            ignorePlanetList.Remove(value);
        }

        public bool InIgnore(int value)
        {
            return ignorePlanetList.Contains(value);
        }
    }
}

