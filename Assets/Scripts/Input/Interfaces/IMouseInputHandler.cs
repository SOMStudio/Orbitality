using UnityEngine;

namespace SOMStudio.BASE.InputManagement.Interfaces
{
    public interface IMouseInputHandler
    {
        Vector2 GetRawPosition();
        Vector2 GetInput(Vector2 relativePosition);
    }
}
