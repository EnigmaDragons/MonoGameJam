using System;

namespace ZeroFootPrintSociety.Characters.Gear
{
    public abstract class Weapon
    {
        public abstract string Name { get; }
        public abstract string Image { get; }
        public abstract bool IsRanged { get; }

        public RangedWeapon AsRanged()
        {
            if (!IsRanged)
                throw new InvalidOperationException("Non Ranged Weapons cannot be used as Ranged Weapon.");
            return (RangedWeapon)this;
        }
    }
}
