using MonoDragons.Core.Common;

namespace ZeroFootPrintSociety.Gear
{
    public abstract class RangedWeapon
    {
        public abstract string Name { get; }
        public abstract string Image { get; }

        public abstract int Accuracy { get; } // Account * 5 = Hit Chance
        public int AccuracyPercent => Accuracy * 5;
        public abstract Map<int, float> EffectiveRanges { get; } // Damage factor at X tiles away
        public abstract int NumShotsPerAttack { get; }
        public abstract int DamagePerHit { get; }
    }
}
