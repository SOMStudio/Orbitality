using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orbitality.Main
{
    public class RocketManager : ExtendedCustomMonoBehaviour, IDamageable, IIgnoreable
    {
        [SerializeField] private float speed = 10.0f;
        [SerializeField] private float damage = 1.0f;

        private List<int> ignorePlanetList = new List<int>();
        
        private Vector3 vectorMove;
        private GameController gameController;
        
        public override void Init()
        {
            base.Init();
            
            gameController = FindObjectOfType<GameController>();
            vectorMove = myTransform.forward;
            
            Invoke(nameof(DestroyObject), 60);
        }

        void Update()
        {
            myTransform.position = UpdatePosition();
            myTransform.rotation = UpdateRotation();
        }

        private Vector3 UpdatePosition()
        {
            Vector3 speedVector = vectorMove * speed;
            Vector3 dependVector = gameController.GetDependencyVector(myTransform);

            if (dependVector != Vector3.zero)
            {
                Vector3 speedVectorNew = speedVector + dependVector;
                speedVector = Vector3.Lerp(speedVector, speedVectorNew, Time.deltaTime);
                
                vectorMove = speedVector.normalized;
                speedVector = vectorMove * speed;
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
        }


        public void SetDamage(float value)
        {
            damage = value;
        }

        public float GetDamage()
        {
            return damage;
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

