using System;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Resolution
{
    public class RangedAction
    {
        private static readonly Random _random = new Random(Guid.NewGuid().GetHashCode());

        public RangedAction()
        {
            Event.Subscribe(EventSubscription.Create<ShotConfirmed>(ResolveShot, this));
        }

        private void ResolveShot(ShotConfirmed e)
        {
            var distance = e.Attacker.CurrentTile.Position.TileDistance(e.Defender.CurrentTile.Position);
            var attackerWeapon = e.Attacker.Gear.EquippedWeapon.AsRanged();
            var attackerHitChance = e.Attacker.Stats.AccuracyPercent + attackerWeapon.AccuracyPercent - e.Defender.Stats.Agility;
            for (var i = 0; i < attackerWeapon.NumShotsPerAttack; i++)
                if (_random.Next(0, 100) < attackerHitChance)
                    e.Defender.State.RemainingHealth -= (int)(attackerWeapon.DamagePerHit * attackerWeapon.EffectiveRanges[distance]);
            if (e.Defender.Gear.EquippedWeapon.IsRanged && e.Defender.State.RemainingHealth > 0 && e.Defender.Gear.EquippedWeapon.AsRanged().EffectiveRanges.ContainsKey(distance))
            {
                var defenderWeapon = e.Defender.Gear.EquippedWeapon.AsRanged();
                var defenderHitChance = e.Defender.Stats.AccuracyPercent + defenderWeapon.AccuracyPercent - e.Attacker.Stats.Agility;
                    for (var i = 0; i < defenderWeapon.NumShotsPerAttack; i++)
                        if (_random.Next(0, 100) < defenderHitChance)
                            e.Attacker.State.RemainingHealth -= (int)(defenderWeapon.DamagePerHit * defenderWeapon.EffectiveRanges[distance]);
            }
            Event.Publish(new ActionResolved());
        }
    }
}
