using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.CoreGame.StateEvents
{
    public class ShotProposed
    {
        public Character Attacker { get; set; }
        public Character Defender { get; set; }

        public int AttackerHitChance { get; set; }
        public int AttackerBullets => Attacker.Gear.EquippedWeapon.IsRanged ? Attacker.Gear.EquippedWeapon.AsRanged().NumShotsPerAttack : 0;
        public int AttackerBulletDamage { get; set; }
        public int AttackerMaxHealth => Attacker.Stats.HP;
        public int AttackerCurrentHealth => Attacker.State.RemainingHealth;
        public int AttackerDamage { get; set; }
        public int DefenderHitChance { get; set; }
        public int DefenderBullets => Defender.Gear.EquippedWeapon.IsRanged ? Defender.Gear.EquippedWeapon.AsRanged().NumShotsPerAttack : 0;
        public int DefenderBulletDamage { get; set; }
        public int DefenderMaxHealth => Defender.Stats.HP;
        public int DefenderCurrentHealth => Defender.State.RemainingHealth;
        public int DefenderDamage { get; set; }
    }
}
