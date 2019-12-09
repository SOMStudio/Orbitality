using System;

namespace Orbitality.Main
{
    public interface IPlanet
    {
        int Id { get; set; }
        string NamePlanet { get; set; }
        float Mass { get; set; }
        float GravityDistance { get; set; }
        float Life { get; set; }
        event Action<float> ChangeLifeEvent;
    }
}