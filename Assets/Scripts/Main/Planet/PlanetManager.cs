using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Orbitality.Main
{
    public class PlanetManager : ExtendedCustomMonoBehaviour, IGravityDependent
    {
        [SerializeField]
        private float mass = 10.0f;
        [SerializeField]
        private float distanceDepend = 50.0f;

        private void Awake()
        {
            var gameController = FindObjectOfType<GameController>();
            gameController.AddPlanet(this);
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

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger Planet with: " + other.gameObject.tag);
        }
    }
}
