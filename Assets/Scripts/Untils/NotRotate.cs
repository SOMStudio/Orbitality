using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotRotate : ExtendedCustomMonoBehaviour
{
    void Update()
    {
        myTransform.rotation = Quaternion.identity; 
    }
}
