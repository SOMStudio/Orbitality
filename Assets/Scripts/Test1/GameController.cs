using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orbitality.Test1
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private PlanetManager[] planetList;
    
        void Start()
        {
        
        }
    
        void Update()
        {
        
        }

        public Vector3 GetDependencyVector(Transform dependObject)
        {
            Vector3 result = Vector3.zero;

            foreach (var variable in planetList)
            {
                result += variable.GetDependencyVector(dependObject);
            }
            
            return result;
        }
    }
}
