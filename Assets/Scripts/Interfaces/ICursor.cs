using UnityEngine;

namespace Orbitality.Cursor
{
    public interface ICursor
    {
        Transform SpawnPoint { set; }
        float SpeedMove { set; }
        float Length { get; set; }
        int CountPoint();
        Vector3 GetPointPosition(int val);
        Vector3 LastPointPosition { get; }

        void UpdateCursor();
    }
}