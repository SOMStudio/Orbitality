﻿using System;
using Orbitality.Menu;
using UnityEngine;
using UnityEngine.Serialization;

namespace Orbitality.Main
{
    public class PlanetManager : ExtendedCustomMonoBehaviour, IPlanet, IGravityDependent
    {
        [Header("Value")]
        [SerializeField] private string namePlanet = "Planet";
        [SerializeField] private float mass = 10.0f;
        [SerializeField] private float gravityDistance = 10.0f;
        [SerializeField] private float life = 10.0f;

        private float startLife = 10.0f;

        [Header("References")]
        [SerializeField] private Transform gravityMeshRef;
        
        [Header("Managers")]
        [SerializeField] private UiPlanet uiManager;

        public event Action<float> ChangeLifeEvent;

        public int Id
        {
            get => GetId();
            set => SetId(value);
        }
        
        public string NamePlanet
        {
            get => namePlanet;
            set => namePlanet = value;
        }

        public float Mass
        {
            get => mass;
            set => mass = value;
        }

        public float GravityDistance
        {
            get => gravityDistance;
            set
            {
                gravityDistance = value;
                
                SetScaleGravityMesh(gravityDistance);
            }
        }

        public float Life
        {
            get => life;
            set => life = value;
        }

        public override void Init()
        {
            base.Init();

            ChangeLifeEvent += uiManager.SetLife;
            
            GameController.Instance?.AddPlanet(this);
            
            startLife = life;
        }

        private void SetScaleGravityMesh(float value)
        {
            gravityMeshRef.localScale = Vector3.one * value;
        }

        private float GetScaleGravityMesh()
        {
            return gravityMeshRef.localScale.x;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                var ignorable = other.GetComponent<IIgnoreable>();
                if (ignorable != null && !ignorable.InIgnore(id))
                {
                    if (life > 0)
                    {
                        life -= damageable.GetDamage();
                        life = life > 0 ? life : 0;
                        
                        ChangeLifeEvent?.Invoke(life * 100 / startLife);
                    }
                }
            }
        }

        public Vector3 GetDependencyVector(Vector3 positionV3)
        {
            Vector3 vectorToObject = transform.position - positionV3;
            float distanceToObject = vectorToObject.magnitude;
            Vector3 result = Vector3.zero;
            
            if (distanceToObject <= gravityDistance)
            {
                float inverseDependency = 1 - (distanceToObject / gravityDistance);
                result = vectorToObject.normalized * inverseDependency;
            }

            return result * mass;
        }
    }
}
