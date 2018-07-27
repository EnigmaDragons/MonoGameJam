﻿using MonoDragons.Core.Common;

namespace ZeroFootPrintSociety.Gear
{
    public abstract class RangedWeapon
    {
        public abstract string Name { get; }
        public abstract string Image { get; }

        public abstract int Accuracy { get; set; } // Account * 5 = Hit Chance
        public abstract Map<int, float> EffectiveRanges { get; set; } // Damage factor at X tiles away
        public abstract string NumShotsPerAttack { get; }
        public abstract int DamagePerHit { get; }
    }
}
