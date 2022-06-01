using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        public HpBar HpBar;

        private IHealth _Health;


        public void Construct(IHealth health)
        {
            _Health = health;
            _Health.HealthChanged += UpdateHpBar;
        }

        private void OnDestroy() => 
            _Health.HealthChanged -= UpdateHpBar;

        private void UpdateHpBar()
        {
            HpBar.SetValue(_Health.Current, _Health.Max);
        }
    }
}