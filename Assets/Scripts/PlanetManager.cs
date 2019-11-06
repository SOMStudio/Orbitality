﻿using System;
using UnityEngine;

namespace Orbitality.Main
{
    public class PlanetManager : ExtendedCustomMonoBehaviour, IPlanet, IGravityDependent
    {
        [SerializeField] private string namePlanet = "Planet";
        [SerializeField] private float mass = 10.0f;
        [SerializeField] private float distanceDepend = 50.0f;
        [SerializeField] private float life = 10.0f;

        private float startLife = 10.0f;

        [SerializeField] private UiPlanet uiManager;

        public event Action<float> ChangeLifeEvent;

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

        public float DistanceDepend
        {
            get => distanceDepend;
            set => distanceDepend = value;
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

        private void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                var ignorable = other.GetComponent<IIgnoreable>();
                if (ignorable != null && !ignorable.InIgnore(id))
                {
                    life -= damageable.GetDamage();

                    ChangeLifeEvent?.Invoke(life * 100 / startLife);
                }
            }
        }

        public Vector3 GetDependencyVector(Transform positionV3)
        {
            Vector3 vectorToObject = transform.position - positionV3.position;
            float distanceToObject = vectorToObject.magnitude;
            Vector3 result = Vector3.zero;
            
            if (distanceToObject <= distanceDepend)
            {
                float inverseDependency = 1 - (distanceToObject / distanceDepend);
                result = vectorToObject.normalized * inverseDependency;
            }

            return result * mass;
        }
    }
}