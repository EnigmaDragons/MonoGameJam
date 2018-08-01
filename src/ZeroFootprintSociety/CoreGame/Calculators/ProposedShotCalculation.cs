using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.Mechanics.Covors;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class ProposedShotCalculation
    {
        private readonly Character _attacker;
        private readonly Character _defender;
        private readonly ShotCoverInfo _attackerBlockInfo;
        private readonly ShotCoverInfo _defenderBlockInfo;

        public ProposedShotCalculation(Character attacker, Character defender, ShotCoverInfo attackerBlockInfo, ShotCoverInfo defenderBlockInfo)
        {
            _attacker = attacker;
            _defender = defender;
            _attackerBlockInfo = attackerBlockInfo;
            _defenderBlockInfo = defenderBlockInfo;
        }

        public ShotProposed CalculateShot()
        {
            var distance = _attacker.CurrentTile.Position.TileDistance(_defender.CurrentTile.Position);
            var attackerWeapon = _attacker.Gear.EquippedWeapon.AsRanged();
            var proposed = new ShotProposed
            {
                Attacker = _attacker,
                Defender = _defender,
                AttackerHitChance = new HitChanceCalculation(_attacker.Accuracy, _defenderBlockInfo.BlockChance, _defender.Stats.Agility, _defender.State.IsHiding).Get(),
                AttackerBulletDamage = (int)(attackerWeapon.DamagePerHit * attackerWeapon.EffectiveRanges[distance]) - _defender.Stats.Guts,
                AttackerBlockInfo = _attackerBlockInfo,
                IsAttackerHiding = _attacker.State.IsHiding
            };
            if (_defender.Gear.EquippedWeapon.IsRanged)
            {
                var defenderWeapon = _defender.Gear.EquippedWeapon.AsRanged();
                proposed.DefenderHitChance = new HitChanceCalculation(_defender.Accuracy, _attackerBlockInfo.BlockChance, _attacker.Stats.Agility, _attacker.State.IsHiding).Get();
                if (defenderWeapon.EffectiveRanges.ContainsKey(distance))
                    proposed.DefenderBulletDamage = (int)(defenderWeapon.DamagePerHit * defenderWeapon.EffectiveRanges[distance]) - _attacker.Stats.Guts;
            }
            proposed.AttackerDamage = proposed.DefenderHitChance * proposed.DefenderBullets * proposed.DefenderBulletDamage / 100;
            proposed.DefenderDamage = proposed.AttackerHitChance * proposed.AttackerBullets * proposed.AttackerBulletDamage / 100;
            proposed.DefenderBlockInfo = _defenderBlockInfo;
            proposed.IsDefenderHiding = _defender.State.IsHiding;
            return proposed;
        }
    }
}
