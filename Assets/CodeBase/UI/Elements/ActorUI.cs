using CodeBase.Logic;
using UnityEngine;
using System;

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

        private void OnDisable()
        {
            try
            {
                _Health.HealthChanged -= UpdateHpBar;
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Unsubscribe exception: {ex}");
            }
        }

        private void UpdateHpBar()
        {
            HpBar.SetValue(_Health.Current, _Health.Max);
        }
    }
}