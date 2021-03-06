﻿using System.Linq;
using MonoDragons.Core;

namespace ZeroFootPrintSociety.Characters.Gear
{
    public abstract class RangedWeapon : Weapon
    {
        public override bool IsRanged => true;
        public override bool IsMelee => false;

        public abstract int Accuracy { get; } // Account * 5 = Hit Chance
        public int AccuracyPercent => Accuracy * 5;
        public abstract Map<int, float> EffectiveRanges { get; } // Damage factor at X tiles away
        public int Range => EffectiveRanges.Keys.OrderByDescending(x => x).First();
        public abstract int NumShotsPerAttack { get; }
        public abstract int DamagePerHit { get; }
    }
}
