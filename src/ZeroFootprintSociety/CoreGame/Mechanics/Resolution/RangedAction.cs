using System;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.CombatResolution
{
    public class RangedAction
    {
        private static Random _random = new Random(Guid.NewGuid().GetHashCode());

        public RangedAction()
        {
            Event.Subscribe(EventSubscription.Create<ShotConfirmed>(ResolveShot, this));
        }

        private void ResolveShot(ShotConfirmed e)
        {
            var attackerHitChance = e.Attacker.Stats.AccuracyPercent + e.Attacker.Gear.EquippedWeapon.AccuracyPercent - e.Defender.Stats.Agility;
            var defenderHitChance = e.Defender.Stats.AccuracyPercent + e.Defender.Gear.EquippedWeapon.AccuracyPercent - e.Attacker.Stats.Agility;
            var distance = Math.Abs(e.Attacker.CurrentTile.Position.X - e.Defender.CurrentTile.Position.X) +
                           Math.Abs(e.Attacker.CurrentTile.Position.Y - e.Defender.CurrentTile.Position.Y);
            for (var i = 0; i < e.Attacker.Gear.EquippedWeapon.NumShotsPerAttack; i++)
                if (_random.Next(0, 100) < attackerHitChance)
                    e.Defender.State.RemainingHealth -= (int)(e.Attacker.Gear.EquippedWeapon.DamagePerHit * e.Attacker.Gear.EquippedWeapon.EffectiveRanges[distance]);
            if (e.Defender.State.RemainingHealth > 0)
                for (var i = 0; i < e.Defender.Gear.EquippedWeapon.NumShotsPerAttack; i++)
                    if (_random.Next(0, 100) < defenderHitChance)
                        e.Attacker.State.RemainingHealth -= (int)(e.Defender.Gear.EquippedWeapon.DamagePerHit * e.Defender.Gear.EquippedWeapon.EffectiveRanges[distance]);
            Event.Publish(new ActionResolved());
        }
    }
}
