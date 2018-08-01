using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.Mechanics.Covors;

namespace ZeroFootPrintSociety.CoreGame
{
    public class ShotProposed
    {
        public Character Attacker { get; set; }
        public Character Defender { get; set; }

        public int AttackerHitChance { get; set; }
        public int AttackerBullets => Attacker.Gear.EquippedWeapon.IsRanged ? Attacker.Gear.EquippedWeapon.AsRanged().NumShotsPerAttack : 0;
        public int AttackerBulletDamage { get; set; }
        public int AttackerDamage { get; set; }
        public ShotCoverInfo AttackerBlockInfo { get; set; }
        public bool IsAttackerHiding { get; set; }
        public int DefenderHitChance { get; set; }
        public int DefenderBullets => Defender.Gear.EquippedWeapon.IsRanged ? Defender.Gear.EquippedWeapon.AsRanged().NumShotsPerAttack : 0;
        public int DefenderBulletDamage { get; set; }
        public int DefenderDamage { get; set; }
        public ShotCoverInfo DefenderBlockInfo { get; set; }
        public bool IsDefenderHiding { get; set; }
    }
}
