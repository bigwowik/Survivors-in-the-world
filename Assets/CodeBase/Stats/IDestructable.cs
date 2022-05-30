using UnityEngine;

namespace CodeBase.Stats
{
    public interface IDestructable
    {
        void OnDestruction(GameObject destroyer);
    }
}