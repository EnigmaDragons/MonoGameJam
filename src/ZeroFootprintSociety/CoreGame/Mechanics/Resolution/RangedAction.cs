using System;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame.ActionEvents;
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
            var distance = Math.Abs(e.Attacker.CurrentTile.Position.X - e.Defender.CurrentTile.Position.X) +
                           Math.Abs(e.Attacker.CurrentTile.Position.Y - e.Defender.CurrentTile.Position.Y);

            var attackerWeapon = e.Attacker.Gear.EquippedWeapon.AsRanged();
            var attackerHitChance = e.Attacker.Stats.AccuracyPercent + attackerWeapon.AccuracyPercent - e.Defender.Stats.Agility;
            for (var i = 0; i < attackerWeapon.NumShotsPerAttack; i++)
                if (_random.Next(0, 100) < attackerHitChance)
                {
                    var amt = (int)(attackerWeapon.DamagePerHit * attackerWeapon.EffectiveRanges[distance]);
                    e.Defender.State.RemainingHealth -= amt;
                    Event.Publish(new ShotHit { Attacker = e.Attacker, Target = e.Defender, DamageAmount = amt });
                }
                else
                    Event.Publish(new ShotMissed { Attacker = e.Attacker, Target = e.Defender });

            if (e.Defender.Gear.EquippedWeapon.IsRanged && e.Defender.State.RemainingHealth > 0)
            {
                var defenderWeapon = e.Defender.Gear.EquippedWeapon.AsRanged();
                var defenderHitChance = e.Defender.Stats.AccuracyPercent + defenderWeapon.AccuracyPercent - e.Attacker.Stats.Agility;
                    for (var i = 0; i < defenderWeapon.NumShotsPerAttack; i++)
                        if (_random.Next(0, 100) < defenderHitChance)
                        {
                            var amt = (int)(defenderWeapon.DamagePerHit * defenderWeapon.EffectiveRanges[distance]);
                            e.Attacker.State.RemainingHealth -= amt;
                            Event.Publish(new ShotHit { Attacker = e.Defender, Target = e.Attacker, DamageAmount = amt });
                        }
                        else
                            Event.Publish(new ShotMissed { Attacker = e.Defender, Target = e.Attacker });
            }
        }
    }
}
