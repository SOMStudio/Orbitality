using UnityEngine;

namespace Orbitality.Main
{
    public interface IGravityController
    {
        void AddPlanet(IGravityDependent planet);
        void RemovePlanet(IGravityDependent planet);
        Vector3 GetDependencyVector(Transform positionV3);
    }
}