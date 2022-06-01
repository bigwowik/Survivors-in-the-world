using UnityEngine;

namespace CodeBase.Hero
{
    public class EnemyMarker : MonoBehaviour
    {
        public EnemyType EnemyType;
        
        
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
        
        
    }

    
}