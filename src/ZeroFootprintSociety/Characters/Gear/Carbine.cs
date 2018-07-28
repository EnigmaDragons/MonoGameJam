using MonoDragons.Core.Common;
using ZeroFootPrintSociety.Gear;

namespace ZeroFootPrintSociety.Characters.Gear
{
    class RsxCarbine : RangedWeapon
    {
        public override string Name { get; } = "RSX-167 Carbine";
        public override string Image { get; } = "";

        public override int Accuracy { get; } = 14;
        public override int NumShotsPerAttack { get; } = 1;
        public override int DamagePerHit { get; } = 12;

        public override Map<int, float> EffectiveRanges { get; } = new Map<int, float> {
            { 1, 0.8f },
            { 2, 0.9f },
            { 3, 1 },
            { 4, 1 },
            { 5, 1.1f },
            { 6, 1.2f },
            { 7, 1.3f },
            { 8, 1.4f },
            { 9, 1.5f }
        };
    }
}
