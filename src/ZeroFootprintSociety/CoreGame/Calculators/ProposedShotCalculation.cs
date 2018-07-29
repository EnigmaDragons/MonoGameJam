using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class ProposedShotCalculation
    {
        private readonly Character _attacker;
        private readonly Character _defender;
        private readonly int _attackerBlockChance;
        private readonly int _defenderBlockChance;

        public ProposedShotCalculation(Character attacker, Character defender, int attackerBlockChance, int defenderBlockChance)
        {
            _attacker = attacker;
            _defender = defender;
            _attackerBlockChance = attackerBlockChance;
            _defenderBlockChance = defenderBlockChance;
        }

        public ShotProposed CalculateShot()
        {
            var distance = _attacker.CurrentTile.Position.TileDistance(_defender.CurrentTile.Position);
            var attackerWeapon = _attacker.Gear.EquippedWeapon.AsRanged();
            var proposed = new ShotProposed
            {
                Attacker = _attacker,
                Defender = _defender,
                AttackerHitChance = new HitChanceCalculation(_attacker.Accuracy, _defenderBlockChance, _defender.Stats.Agility).Get(),
                AttackerBulletDamage = (int)(attackerWeapon.DamagePerHit * attackerWeapon.EffectiveRanges[distance]),
                AttackerBlockChance = _attackerBlockChance
            };
            if (_defender.Gear.EquippedWeapon.IsRanged)
            {
                var defenderWeapon = _defender.Gear.EquippedWeapon.AsRanged();
                proposed.DefenderHitChance = new HitChanceCalculation(_defender.Accuracy, _attackerBlockChance, _attacker.Stats.Agility).Get();
                if (defenderWeapon.EffectiveRanges.ContainsKey(distance))
                    proposed.DefenderBulletDamage = (int)(defenderWeapon.DamagePerHit * defenderWeapon.EffectiveRanges[distance]);
            }
            proposed.AttackerDamage = proposed.DefenderHitChance * proposed.DefenderBullets * proposed.DefenderBulletDamage / 100;
            proposed.DefenderDamage = proposed.AttackerHitChance * proposed.AttackerBullets * proposed.AttackerBulletDamage / 100;
            proposed.DefenderBlockChance = _defenderBlockChance;
            return proposed;
        }
    }
}
