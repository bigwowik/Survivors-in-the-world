using CodeBase.Hero.Weapon;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroEnabler : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private HeroMove _heroMove;
        private PlayerWeaponHandler _playerWeaponHandler;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            TryGetComponent(out _heroMove);
        }

        public void EnablePlayer(bool enable)
        {
            if (enable)
                transform.position = Vector2.zero;
            
            gameObject.SetActive(enable);
            // _spriteRenderer.enabled = enable;
            // _heroMove.enabled = enable;
            // _playerWeaponHandler.enabled = enable;
        }
        
        
    }
}