namespace CodeBase.Hero.Weapon
{
    class PlayerWeaponHandler : WeaponHandler
    {
        protected override void Init()
        {
            Weapon = Instantiate(WeaponTemplate);
        }
    }
}