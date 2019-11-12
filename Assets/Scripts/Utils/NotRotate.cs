using UnityEngine;

[AddComponentMenu("Utility/NotRotate")]

public class NotRotate : ExtendedCustomMonoBehaviour
{
    void Update()
    {
        myTransform.rotation = Quaternion.identity; 
    }
}
