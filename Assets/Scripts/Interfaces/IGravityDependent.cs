﻿using System;
using UnityEngine;

namespace Orbitality.Main
{
    public interface IGravityDependent
    {
        Vector3 GetDependencyVector(Vector3 positionV3);
    }
}