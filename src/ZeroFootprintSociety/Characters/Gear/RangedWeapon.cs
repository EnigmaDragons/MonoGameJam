using MonoDragons.Core.Common;

namespace ZeroFootPrintSociety.Characters.Gear
{
    public abstract class RangedWeapon : Weapon
    {
        public override bool IsRanged => true;

        public abstract int Accuracy { get; } // Account * 5 = Hit Chance
        public int AccuracyPercent => Accuracy * 5;
        public abstract Map<int, float> EffectiveRanges { get; } // Damage factor at X tiles away
        public abstract int NumShotsPerAttack { get; }
        public abstract int DamagePerHit { get; }
    }
}
