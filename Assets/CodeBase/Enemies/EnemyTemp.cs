using CodeBase.Hero;
using UnityEngine;
using Zenject;

namespace CodeBase.Enemies
{
    public class EnemyTemp : MonoBehaviour
    {
        public Transform Player;
        
        [Inject]
        private void Construct(HeroMove heroMove)
        {
            Player = heroMove.transform;
        }

        
        
    }
}