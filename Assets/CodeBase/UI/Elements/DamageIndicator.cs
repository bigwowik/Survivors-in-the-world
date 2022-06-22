using CodeBase.Hero;
using CodeBase.Hero.Weapon;
using CodeBase.Infrastructure.Difficulty;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Elements
{
    public class DamageIndicator : MonoBehaviour
    {
        public TextMeshProUGUI Text;

        private WeaponBase _weapon;
        private IDifficultyService _difficultyService;


        [Inject]
        public void Construct(IDifficultyService difficultyService)
        {
            _difficultyService = difficultyService;
        }

        public void Init(WeaponBase weapon)
        {
            _weapon = weapon;
        }

        private void Start()
        {
            _difficultyService.UpgradeWasCompleted += UpdateDamageText;
            UpdateDamageText();
        }

        private void OnDestroy()
        {
            _difficultyService.UpgradeWasCompleted -= UpdateDamageText;
        }

        private void UpdateDamageText()
        {
            Text.text = $"{_weapon.Attack.AttackValue}";
        }
    }
}
