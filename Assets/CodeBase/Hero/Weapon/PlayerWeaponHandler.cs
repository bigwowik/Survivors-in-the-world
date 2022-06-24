using System;

namespace CodeBase.Hero.Weapon
{
    class PlayerWeaponHandler : WeaponHandler
    {
        protected override void Init()
        {
            Weapon = Instantiate(WeaponTemplate);
        }

        public void Reset()
        {
            Weapon.Attack.AttackValue = WeaponTemplate.Attack.AttackValue;
        }
    }
}