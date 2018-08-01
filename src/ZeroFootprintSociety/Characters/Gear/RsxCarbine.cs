using MonoDragons.Core.Common;

namespace ZeroFootPrintSociety.Characters.Gear
{
    class RsxCarbine : RangedWeapon
    {
        public override string Name { get; } = "RSX-167 Carbine";
        public override string Image { get; } = "Weapons/RsxCarbine";
        public override string ShortDescription { get; } = "Very accurate, single shot gun. Particularly deadly at long range.";

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

    class PowerKatana : MeleeWeapon
    {
        public override string Name { get; } = "PWR-56 Katana";
        public override string Image { get; } = "Weapons/RsxCarbine";
        public override string ShortDescription { get; } = "Popular among Yakuza assassins for it's ability to pierce right through armor in the heart's of any unsuspecting victim.";

        public override int NumHitsPerAttack { get; } = 1;
        public override int DamagePerHit { get; } = 17;
        public override int Trauma { get; } = 0;
        public override int Defense { get; } = 0;
        public override bool DoesStunOnHit { get; } = false;
    }

    class NaniteEdgedKatana : MeleeWeapon
    {
        public override string Name { get; } = "Nanite Infused Katana";
        public override string Image { get; } = "Weapons/RsxCarbine";
        public override string ShortDescription { get; } = "Once this blade contacts your skin, nanites begin to tear you apart from the inside. Highly illegal, highly effective.";

        public override int NumHitsPerAttack { get; } = 1;
        public override int DamagePerHit { get; } = 10;
        public override int Trauma { get; } = 5;
        public override int Defense { get; } = 0;
        public override bool DoesStunOnHit { get; } = false;
    }

    class PowerWakizashi : MeleeWeapon
    {
        public override string Name { get; } = "PWR-78 Wakizashi";
        public override string Image { get; } = "Weapons/RsxCarbine";
        public override string ShortDescription { get; } = "A light fast blade, especially effective for ripping apart low-armored targets";

        public override int NumHitsPerAttack { get; } = 1;
        public override int DamagePerHit { get; } = 9;
        public override int Trauma { get; } = 5;
        public override int Defense { get; } = 0;
        public override bool DoesStunOnHit { get; } = false;
    }

    class StunStick : MeleeWeapon
    {
        public override string Name { get; } = "Surge Baton";
        public override string Image { get; } = "Weapons/RsxCarbine";
        public override string ShortDescription { get; } = "Used by riot control to easily lock down even the most violent cybernetic protesters.";

        public override int NumHitsPerAttack { get; } = 2;
        public override int DamagePerHit { get; } = 10;
        public override int Trauma { get; } = 0;
        public override int Defense { get; } = 0;
        public override bool DoesStunOnHit { get; } = false;
    }

    class PowerHammer : MeleeWeapon
    {
        public override string Name { get; } = "PWR-43 Hammer";
        public override string Image { get; } = "Weapons/RsxCarbine";
        public override string ShortDescription { get; } = "Used by riot control to easily lock down even the most violent cybernetic protesters.";

        public override int NumHitsPerAttack { get; } = 1;
        public override int DamagePerHit { get; } = 10;
        public override int Trauma { get; } = 0;
        public override int Defense { get; } = 0;
        public override bool DoesStunOnHit { get; } = true;
    }

    class PowerShieldAndBaton : MeleeWeapon
    {
        public override string Name { get; } = "Power Shield and Baton";
        public override string Image { get; } = "Weapons/RsxCarbine";
        public override string ShortDescription { get; } = "The extra fortification needed to keep you alive and kicking.";

        public override int NumHitsPerAttack { get; } = 1;
        public override int DamagePerHit { get; } = 10;
        public override int Trauma { get; } = 0;
        public override int Defense { get; } = 5;
        public override bool DoesStunOnHit { get; } = false;
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

    class RsxAssultRifle : RangedWeapon
    {
        public override string Name { get; } = "RSX-107 Assult Rifle";
        public override string Image { get; } = "Weapons/nnvscopedassault";
        public override string ShortDescription { get; } = "A reliable 3-round burst rifle";
        public override int Accuracy { get; } = 12;
        public override int NumShotsPerAttack { get; } = 3;
        public override int DamagePerHit { get; } = 9;

        public override Map<int, float> EffectiveRanges { get; } = new Map<int, float> {
            { 1, 0.7f },
            { 2, 0.8f },
            { 3, 0.9f },
            { 4, 1.0f },
            { 5, 0.9f },
            { 6, 0.8f },
            { 7, 0.7f },
        };
    }

    class WarUzi : RangedWeapon
    {
        public override string Name { get; } = "WAR-27 \"Spray and Pray\" Gun";
        public override string Image { get; } = "Weapons/smanclassicar";
        public override string ShortDescription { get; } = "You just point in the general direction of the enemy and waste a clip and pray to god they are not moving afterwards.";
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

    class FiliBlade : MeleeWeapon
    {
        public override string Name { get; } = "Fili Blade \"Box Cutter\"";
        public override string Image { get; } = "Weapons/kteccombatblade";
        public override string ShortDescription { get; } = "These are general purpose knives, though it's not unheard of for shakings to be done with these.";

        public override int NumHitsPerAttack { get; } = 2;
        public override int DamagePerHit { get; } = 8;
        public override int Trauma { get; } = 0;
        public override int Defense { get; } = 0;
        public override bool DoesStunOnHit { get; } = false;
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

    class WristShotgun : RangedWeapon
    {
        public override string Name { get; } = "Johnny's Shotgun";
        public override string Image { get; } = "Weapons/RsxCarbine";
        public override string ShortDescription { get; } = "Don't feel safe going to the grocery store, no worries this easy to carry shotgun has got you covered.";
        public override int Accuracy { get; } = 10;
        public override int NumShotsPerAttack { get; } = 1;
        public override int DamagePerHit { get; } = 18;

        public override Map<int, float> EffectiveRanges { get; } = new Map<int, float> {
            { 1, 1.2f },
            { 2, 1.1f },
            { 3, 1.1f },
            { 4, 0.9f }
        };
    }


    class DoubleShotgun : WarShotgun
    {
        public override string Name => $"Double \"{base.Name}\"s";
        public override string Image { get; } = "Weapons/RsxCarbine";
        public override int NumShotsPerAttack => base.NumShotsPerAttack * 2;
    }

    class DoubleAssultRifle : RsxAssultRifle
    {
        public override string Name => $"Double \"{base.Name}\"s";
        public override string Image { get; } = "Weapons/RsxCarbine";
        public override int NumShotsPerAttack => base.NumShotsPerAttack * 2;
    }

    class DoubleUzi : WarUzi
    {
        public override string Name => $"Double \"{base.Name}\"s";
        public override string Image { get; } = "Weapons/RsxCarbine";
        public override int NumShotsPerAttack => base.NumShotsPerAttack * 2;
    }

    class DoubleMagnum : PowerMagnum
    {
        public override string Name => $"Double \"{base.Name}\"s";
        public override string Image { get; } = "Weapons/RsxCarbine";
        public override int NumShotsPerAttack => base.NumShotsPerAttack * 2;
    }

    class DoubleAutoPistol : AutoPistol
    {
        public override string Name => $"Double \"{base.Name}\"s";
        public override string Image { get; } = "Weapons/RsxCarbine";
        public override int NumShotsPerAttack => base.NumShotsPerAttack * 2;
    }
}
