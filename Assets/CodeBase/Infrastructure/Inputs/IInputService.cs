using UnityEngine;

namespace CodeBase
{
    public interface IInputService : IService
    {
        Vector3 Axis { get; }
    }
}