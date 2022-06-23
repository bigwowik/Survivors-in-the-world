using UnityEngine;

namespace CodeBase.Helpers
{
    public static class Extensions
    {
        public static Vector2Int ToVector2Int(this Vector3 vector)
        {
            return new Vector2Int((int) vector.x, (int) (vector.y));
        }
        public static Vector2Int ToVector2Int(this Vector2 vector)
        {
            return new Vector2Int((int) vector.x, (int)vector.y);
        }
    }
}