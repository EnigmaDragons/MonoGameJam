using System;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame.ActionEvents;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Resolution
{
    public class RangedAction
    {
        private static readonly Random _random = new Random(Guid.NewGuid().GetHashCode());

        public RangedAction()
        {
            Event.Subscribe(EventSubscription.Create<TargetInspected>(PreviewShot, this));
            Event.Subscribe(EventSubscription.Create<ShotConfirmed>(ResolveShot, this));
        }

        private void PreviewShot(TargetInspected e)
        {
            var distance = e.Attacker.CurrentTile.Position.TileDistance(e.Defender.CurrentTile.Position);
            var attackerWeapon = e.Attacker.Gear.EquippedWeapon.AsRanged();
            var proposed = new ShotProposed
            {
                Attacker = e.Attacker,
                Defender = e.Defender,
                AttackerHitChance = e.Attacker.Stats.AccuracyPercent + attackerWeapon.AccuracyPercent - e.Defender.Stats.Agility,
                AttackerBulletDamage = (int)(attackerWeapon.DamagePerHit * attackerWeapon.EffectiveRanges[distance]),
            };
            if (e.Defender.Gear.EquippedWeapon.IsRanged)
            {
                var defenderWeapon = e.Defender.Gear.EquippedWeapon.AsRanged();
                proposed.DefenderHitChance = e.Defender.Stats.AccuracyPercent + defenderWeapon.AccuracyPercent - e.Attacker.Stats.Agility;
                if (defenderWeapon.EffectiveRanges.ContainsKey(distance))
                    proposed.DefenderBulletDamage = (int)(defenderWeapon.DamagePerHit * defenderWeapon.EffectiveRanges[distance]);
            }
            proposed.AttackerDamage = proposed.DefenderHitChance * proposed.DefenderBullets * proposed.DefenderBulletDamage / 100;
            proposed.DefenderDamage = proposed.AttackerHitChance * proposed.AttackerBullets * proposed.AttackerBulletDamage / 108;
            Event.Publish(proposed);
        }

        private void ResolveShot(ShotConfirmed e)
        {
            for (var i = 0; i < e.Proposed.AttackerBullets; i++)
            {
                if (_random.Next(0, 100) < e.Proposed.AttackerHitChance)
                {
                    e.Proposed.Defender.State.RemainingHealth -= e.Proposed.AttackerBulletDamage;
                    Event.Publish(new ShotHit { Attacker = e.Proposed.Attacker, Target = e.Proposed.Defender, DamageAmount = e.Proposed.AttackerBulletDamage });
                }
                else
                {
                    Event.Publish(new ShotMissed { Attacker = e.Proposed.Attacker, Target = e.Proposed.Defender });
                }
            }
            if (e.Proposed.Defender.State.RemainingHealth > 0)
            {
                for (var i = 0; i < e.Proposed.DefenderBullets; i++)
                {
                    if (_random.Next(0, 100) < e.Proposed.DefenderHitChance)
                    {
                        e.Proposed.Attacker.State.RemainingHealth -= e.Proposed.DefenderBulletDamage;
                        Event.Publish(new ShotHit { Attacker = e.Proposed.Defender, Target = e.Proposed.Attacker, DamageAmount = e.Proposed.AttackerBulletDamage });
                    }
                    else
                    {
                        Event.Publish(new ShotMissed { Attacker = e.Proposed.Defender, Target = e.Proposed.Attacker });
                    }
                }
            }
            Event.Publish(new ActionResolved());
        }
    }
}
