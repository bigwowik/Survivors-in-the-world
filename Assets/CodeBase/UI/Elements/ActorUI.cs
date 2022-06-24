using CodeBase.Logic;
using UnityEngine;
using System;

namespace CodeBase.UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        public HpBar HpBar;

        private IHealth _health;

        public void Construct(IHealth health)
        {
            _health = health;
            _health.HealthChanged += UpdateHpBar;
        }

        private void Start() => 
            UpdateHpBar();

        private void OnDisable()
        {
            try
            {
                _health.HealthChanged -= UpdateHpBar;
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Unsubscribe exception: {ex}");
            }
        }

        private void UpdateHpBar()
        {
            HpBar.SetValue(_health.Current, _health.Max);
        }
    }
}