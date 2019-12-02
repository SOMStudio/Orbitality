using System;
using Orbitality.Cursor;
using UnityEngine;

namespace Orbitality.Enemy.AI
{
    public partial interface IAiInput
    {
        int RotateAxis { get; }
        Vector3 RotateVector { get; }
        int IncreaseAxis { get; }
        float IncreaseLength { get; }
        
        void SetCursor(ICursor val);
        
        event Action Shot;
    }
}