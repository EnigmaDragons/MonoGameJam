using System.Collections.Generic;
using MonoDragons.Core.Common;

namespace ZeroFootPrintSociety.Characters.Gear
{
    static class WeaponLists
    {
        private static List<Weapon> _primaries = new List<Weapon>
        {
            new RsxCarbine(),
            new WarUzi(),
            new WarShotgun()
        };
        private static List<Weapon> _secondaries = new List<Weapon>
        {
            new SideArm(),
            new PowerMagnum(),
            new AutoPistol()
        };

        public static Weapon RandomPrimary()
        {
            return _primaries.Random();
        }

        public static Weapon RandomSecondary()
        {
            return _secondaries.Random();
        }
    }
    
    class RsxCarbine : RangedWeapon
    {
        public override string Name { get; } = "RSX-167 Carbine";
        public override string Image { get; } = "Weapons/nnvscopedassault";
        public override string ShortDescription { get; } = "Very accurate, single shot rifle. Particularly deadly at long range.";

        public override int Accuracy { get; } = 14;
        public override int NumShotsPerAttack { get; } = 1;
        public override int DamagePerHit { get; } = 14;

        public override Map<int, float> EffectiveRanges { get; } = new Map<int, float> {
            { 1, 0.8f },
            { 2, 0.8f },
            { 3, 0.9f },
            { 4, 0.9f },
            { 5, 1.0f },
            { 6, 1.0f },
            { 7, 1.1f },
            { 8, 1.1f },
            { 9, 1.2f },
            { 10, 1.2f },
            { 11, 1.3f },
            { 12, 1.3f },
            { 13, 1.4f },
            { 14, 1.4f },
            { 15, 1.5f },
        };
    }

    class GoldenGun : RangedWeapon
    {
        public override string Name { get; } = "Golden Gun";
        public override string Image { get; } = "Weapons/slgstandard";
        public override string ShortDescription { get; } = "The ultimate";

        public override int Accuracy { get; } = 21;
        public override int NumShotsPerAttack { get; } = 1;
        public override int DamagePerHit { get; } = 99;

        public override Map<int, float> EffectiveRanges { get; } = new Map<int, float> {
            { 1, 1f },
            { 2, 1f },
            { 3, 1 },
            { 4, 1 },
            { 5, 1f },
            { 6, 1f },
            { 7, 1f },
            { 8, 1f },
            { 9, 1f }
        };
    }
    
    class WarShotgun : RangedWeapon
    {
        public override string Name { get; } = "WAR-12 Shotgun";
        public override string Image { get; } = "Weapons/urxsemishotgun";
        public override string ShortDescription { get; } = "When you want to make things personal";
        public override int Accuracy { get; } = 11;
        public override int NumShotsPerAttack { get; } = 1;
        public override int DamagePerHit { get; } = 20;

        public override Map<int, float> EffectiveRanges { get; } = new Map<int, float> {
            { 1, 1.5f },
            { 2, 1.2f },
            { 3, 1f },
            { 4, 0.7f },
            { 5, 0.4f }
        };
    }

    class WarUzi : RangedWeapon
    {
        public override string Name { get; } = "WAR-27 Defense SMG";
        public override string Image { get; } = "Weapons/smanclassicar";
        public override string ShortDescription { get; } = "Urban terrain optimized submachine gun. Fast and deadly.";
        public override int Accuracy { get; } = 7;
        public override int NumShotsPerAttack { get; } = 9;
        public override int DamagePerHit { get; } = 10;

        public override Map<int, float> EffectiveRanges { get; } = new Map<int, float> {
            { 1, 0.9f },
            { 2, 1.2f },
            { 3, 1f },
            { 4, 1f },
            { 5, 0.9f },
            { 6, 0.7f }
        };
    }
    
        class AutoPistol : RangedWeapon
    {
        public override string Name { get; } = "Glock-99";
        public override string Image { get; } = "Weapons/rephoser";
        public override string ShortDescription { get; } = "The latest in self-defense full-auto pistols.";
        public override int Accuracy { get; } = 8;
        public override int NumShotsPerAttack { get; } = 5;
        public override int DamagePerHit { get; } = 6;

        public override Map<int, float> EffectiveRanges { get; } = new Map<int, float> {
            { 1, 0.8f },
            { 2, 0.9f },
            { 3, 1f },
            { 4, 0.9f },
            { 5, 0.8f }
        };
    }

    class PowerMagnum : RangedWeapon
    {
        public override string Name { get; } = "PWR-1 Magnum";
        public override string Image { get; } = "Weapons/tirmagnum";
        public override string ShortDescription { get; } = "The first and greatest of it's kind, the magnum of the future.";
        public override int Accuracy { get; } = 11;
        public override int NumShotsPerAttack { get; } = 2;
        public override int DamagePerHit { get; } = 10;

        public override Map<int, float> EffectiveRanges { get; } = new Map<int, float> {
            { 1, 0.7f },
            { 2, 0.8f },
            { 3, 0.9f },
            { 4, 1f },
            { 5, 1.1f },
            { 6, 1.2f },
            { 7, 1.3f }
        };
    }
    
    class SideArm : RangedWeapon
    {
        public override string Name { get; } = "SLG Standard Semi";
        public override string Image { get; } = "Weapons/slgstandard";
        public override string ShortDescription { get; } = "Accurate, effective, timeless, reliable, and simple personal close-range side arm.";
        public override int Accuracy { get; } = 14;
        public override int NumShotsPerAttack { get; } = 1;
        public override int DamagePerHit { get; } = 15;

        public override Map<int, float> EffectiveRanges { get; } = new Map<int, float> {
            { 1, 1.3f },
            { 2, 1.3f },
            { 3, 1.3f },
            { 4, 1f },
            { 5, 0.7f },
            { 6, 0.7f },
            { 7, 0.7f }
        };
    }
}
