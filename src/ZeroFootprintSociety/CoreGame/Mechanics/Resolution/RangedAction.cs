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
            var attackerHitChance = e.Attacker.Stats.AccuracyPercent + e.Attacker.Gear.EquippedWeapon.AccuracyPercent - e.Defender.Stats.Agility;
            var defenderHitChance = e.Defender.Stats.AccuracyPercent + e.Defender.Gear.EquippedWeapon.AccuracyPercent - e.Attacker.Stats.Agility;
            var distance = e.Attacker.CurrentTile.Position.TileDistance(e.Defender.CurrentTile.Position);
            for (var i = 0; i < e.Attacker.Gear.EquippedWeapon.NumShotsPerAttack; i++)
                if (_random.Next(0, 100) < attackerHitChance)
                    e.Defender.State.RemainingHealth -= (int)(e.Attacker.Gear.EquippedWeapon.DamagePerHit * e.Attacker.Gear.EquippedWeapon.EffectiveRanges[distance]);
            if (e.Defender.State.RemainingHealth > 0 && e.Defender.Gear.EquippedWeapon.EffectiveRanges.ContainsKey(distance))
                for (var i = 0; i < e.Defender.Gear.EquippedWeapon.NumShotsPerAttack; i++)
                    if (_random.Next(0, 100) < defenderHitChance)
                        e.Attacker.State.RemainingHealth -= (int)(e.Defender.Gear.EquippedWeapon.DamagePerHit * e.Defender.Gear.EquippedWeapon.EffectiveRanges[distance]);
            Event.Publish(new ActionResolved());
        }
    }
}
