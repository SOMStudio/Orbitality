using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orbitality.Main
{
    public class GameController : MonoBehaviour
    {
        private List<IGravityDependent> planetList = new List<IGravityDependent>();
    
        void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public static GameController Instance { get; private set; }

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
