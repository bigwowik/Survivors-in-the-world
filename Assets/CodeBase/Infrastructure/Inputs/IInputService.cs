using UnityEngine;

namespace CodeBase.Infrastructure.Inputs
{
    public interface IInputService : IService
    {
        Vector3 Axis { get; }
    }
}