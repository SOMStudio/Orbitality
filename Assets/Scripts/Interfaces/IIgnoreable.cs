using System.Collections;

namespace Orbitality.Main
{
    public interface IIgnoreable
    {
        void AddIgnore(int value);
        void RemoveIgnore(int value);
        bool InIgnore(int value);
    }
}
