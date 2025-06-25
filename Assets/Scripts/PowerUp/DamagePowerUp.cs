using UnityEngine;

namespace OZITK
{
    public class DamagePowerUp : DurationPowerUp
    {
        [Tooltip("Damage multiplier applied during power-up")]
        [SerializeField] private float value = 1.5f;

        private WeaponAbility[] weapons; // Cached array of WeaponAbility components
        private bool usingWeapons;

        protected override void OnPowerUpStart()
        {
            weapons = player.GetAbilities<WeaponAbility>();

            usingWeapons = weapons != null && weapons.Length > 0;
        }

        protected override void OnPowerUpUpdate()
        {
            if (usingWeapons)
            {
                foreach (var weapon in weapons)
                {
                    // Prevents overwriting stronger active buffs
                    if (weapon.DamageMulti < value)
                        weapon.DamageMulti = value;
                }
            }
        }

        protected override void OnPowerUpEnd()
        {
            if (usingWeapons)
            {
                foreach (var weapon in weapons)
                {
                    weapon.DamageMulti = 1;
                }
            }
        }
    }
}
