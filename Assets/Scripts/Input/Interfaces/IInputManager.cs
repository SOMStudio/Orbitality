using System;
using UnityEngine;

namespace SOMStudio.BASE.InputManagement.Interfaces
{
    public interface IInputManager
    {
        void AddActionToBinding(string binding, Action action);
        float GetAxis(string axisName);
        bool GetButton(string buttonName);
        Vector2 GetMouseVector(Vector2 relativePosition);
    }
}

