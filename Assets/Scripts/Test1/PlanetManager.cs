using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    [SerializeField]
    private float dependMass = 1.0f;
    [SerializeField]
    private float dependDistance = 5.0f;

    public Vector3 GetDependencyVector(Transform dependObject)
    {
        Vector3 vectorToObject = transform.position - dependObject.position;
        float distanceToObject = vectorToObject.magnitude;
        Vector3 result = Vector3.zero;

        if (distanceToObject <= dependDistance)
        {
            float inverseDependency = 1 - (distanceToObject / dependDistance);
            result = vectorToObject.normalized * inverseDependency;
        }

        return result * dependMass;
    }
}
