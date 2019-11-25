using SOMStudio.BASE.InputManagement;
using UnityEngine;

namespace SOMStudio.Orbitality.InputManagement
{
    public class SampleBindings : InputBindings
    {
        protected override void SetupBindings()
        {
            base.SetupBindings();
            keyBindings.Add("shoot", KeyCode.Mouse0);
        }
    }
}