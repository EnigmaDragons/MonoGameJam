using System.Collections.Generic;
using MonoDragons.Core;

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

        public override int Accuracy { get; } = 12;
        public override int NumShotsPerAttack { get; } = 1;
        public override int DamagePerHit { get; } = 20;

        public override Map<int, float> EffectiveRanges { get; } = new Map<int, float> {
            { 1, 0.6f },
            { 2, 0.7f },
            { 3, 0.7f },
            { 4, 0.8f },
            { 5, 0.8f },
            { 6, 0.9f },
            { 7, 0.9f },
            { 8, 1f },
            { 9, 1f },
            { 10, 1.1f },
            { 11, 1.1f },
            { 12, 1.2f },
            { 13, 1.2f },
            { 14, 1.3f },
            { 15, 1.3f },
            { 16, 1.4f },
        };
    }

#if DEBUG
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
#endif
    
    class WarShotgun : RangedWeapon
    {
        public override string Name { get; } = "G13 Combat Shotgun";
        public override string Image { get; } = "Weapons/urxsemishotgun";
        public override string ShortDescription { get; } = "Brutal semi-automatic combat weapon. Lethal in close-quarters combat. ";
        public override int Accuracy { get; } = 10;
        public override int NumShotsPerAttack { get; } = 1;
        public override int DamagePerHit { get; } = 34;

        public override Map<int, float> EffectiveRanges { get; } = new Map<int, float> {
            { 1, 1.5f },
            { 2, 1.3f },
            { 3, 1.1f },
            { 4, 0.9f },
            { 5, 0.7f },
            { 6, 0.5f }
        };
    }

    class WarUzi : RangedWeapon
    {
        public override string Name { get; } = "WAR-27 Defense SMG";
        public override string Image { get; } = "Weapons/smanclassicar";
        public override string ShortDescription { get; } = "Urban terrain submachine gun. Spits out bullets at an unbelievable rate.";
        public override int Accuracy { get; } = 8;
        public override int NumShotsPerAttack { get; } = 7;
        public override int DamagePerHit { get; } = 5;

        public override Map<int, float> EffectiveRanges { get; } = new Map<int, float> {
            { 1, 0.9f },
            { 2, 1.2f },
            { 3, 1.3f },
            { 4, 1.2f },
            { 5, 1.1f },
            { 6, 1.0f },
            { 7, 1.0f },
            { 8, 0.9f },
            { 9, 0.8f },
            { 10, 0.6f },
        };
    }
    
    class AutoPistol : RangedWeapon
    {
        public override string Name { get; } = "Glock-99";
        public override string Image { get; } = "Weapons/rephoser";
        public override string ShortDescription { get; } = "The latest in self-defense full-auto pistols.";
        public override int Accuracy { get; } = 9;
        public override int NumShotsPerAttack { get; } = 4;
        public override int DamagePerHit { get; } = 6;

        public override Map<int, float> EffectiveRanges { get; } = new Map<int, float> {
            { 1, 0.9f },
            { 2, 1.2f },
            { 3, 1.3f },
            { 4, 1.2f },
            { 5, 1.0f },
            { 6, 0.9f },
            { 7, 0.8f },
            { 8, 0.7f },
        };
    }

    class PowerMagnum : RangedWeapon
    {
        public override string Name { get; } = "RSX .60 Magnum";
        public override string Image { get; } = "Weapons/tirmagnum";
        public override string ShortDescription { get; } = "Throws slugs that pack a serious wallop. Has a lot of recoil.";
        public override int Accuracy { get; } = 11;
        public override int NumShotsPerAttack { get; } = 2;
        public override int DamagePerHit { get; } = 9;

        public override Map<int, float> EffectiveRanges { get; } = new Map<int, float> {
            { 1, 0.7f },
            { 2, 0.8f },
            { 3, 0.9f },
            { 4, 1.0f },
            { 5, 1.1f },
            { 6, 1.2f },
            { 7, 1.3f },
            { 8, 1.2f },
            { 9, 1.1f },
            { 10, 1.0f },
            { 11, 0.9f },
            { 12, 0.8f },
        };
    }
    
    class SideArm : RangedWeapon
    {
        public override string Name { get; } = "SLG Standard Semi";
        public override string Image { get; } = "Weapons/slgstandard";
        public override string ShortDescription { get; } = "Accurate, effective, and reliable personal close-range side arm.";
        public override int Accuracy { get; } = 10;
        public override int NumShotsPerAttack { get; } = 3;
        public override int DamagePerHit { get; } = 7;

        public override Map<int, float> EffectiveRanges { get; } = new Map<int, float> {
            { 1, 0.8f },
            { 2, 0.8f },
            { 3, 0.9f },
            { 4, 0.9f },
            { 5, 1f },
            { 6, 1f },
            { 7, 1.1f },
            { 8, 1.1f },
            { 9, 1.2f },
            { 10, 1.2f },
        };
    }
}
