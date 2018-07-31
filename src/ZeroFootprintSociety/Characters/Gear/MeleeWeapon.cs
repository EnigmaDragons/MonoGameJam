namespace ZeroFootPrintSociety.Characters.Gear
{
    public abstract class MeleeWeapon : Weapon
    {
        public override bool IsRanged => false;
        public override bool IsMelee => true;

        public abstract int NumHitsPerAttack { get; }
        public abstract int DamagePerHit { get; }
        public abstract int Trauma { get; }
        public abstract int Defense { get; }
        public abstract bool DoesStunOnHit { get; }
    }
}
