using System;

namespace Orbitality.Main
{
    public interface IPlanet
    {
        string NamePlanet { get; set; }
        float Mass { get; set; }
        float DistanceDepend { get; set; }
        float Life { get; set; }
        event Action<float> ChangeLifeEvent;
    }
}