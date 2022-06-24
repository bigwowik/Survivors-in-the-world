using CodeBase.Hero.Weapon;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroEnabler : MonoBehaviour
    {
        public void EnablePlayer(bool enable)
        {
            if (enable)
                transform.position = Vector2.zero;
            
            gameObject.SetActive(enable);
        }
        
        
    }
}