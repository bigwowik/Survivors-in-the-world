using UnityEngine;

namespace CodeBase
{
    public class UnityInputService : IInputService
    {
        public Vector3 Axis => new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
    }
}
    