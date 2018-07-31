using System;
using System.Collections.Generic;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame.ActionEvents;
using ZeroFootPrintSociety.CoreGame.Calculators;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.GUI;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Resolution
{
    public class RangedAction
    {
        private static readonly Random _random = new Random(Guid.NewGuid().GetHashCode());
        private Queue<Action> _eventQueue = new Queue<Action>();

        public RangedAction()
        {
            Event.Subscribe(EventSubscription.Create<ShotConfirmed>(ResolveShot, this));
            Event.Subscribe(EventSubscription.Create<ShotAnimationsFinished>(AdvanceQueue, this));
        }

        private void AdvanceQueue(ShotAnimationsFinished e)
        {
            if (_eventQueue.Count > 0)
                _eventQueue.Dequeue().Invoke();
        }

        private void ResolveShot(ShotConfirmed e)
        {
            for (var i = 0; i < e.Proposed.AttackerBullets; i++)
            {
                Event.Publish(new ShotFired { Attacker = e.Proposed.Attacker, Target = e.Proposed.Defender });
                if (_random.Next(0, 100) < e.Proposed.DefenderBlockChance)
                {
                    Event.Publish(new ShotBlocked { Attacker = e.Proposed.Attacker, Target = e.Proposed.Defender });
                }
                else
                {
                    if (_random.Next(0, 100) < e.Proposed.AttackerHitChance)
                        Event.Publish(new ShotHit { Attacker = e.Proposed.Attacker, Target = e.Proposed.Defender, DamageAmount = e.Proposed.AttackerBulletDamage });
                    else
                        Event.Publish(new ShotMissed { Attacker = e.Proposed.Attacker, Target = e.Proposed.Defender });
                }
            }
            e.Proposed.Attacker.State.IsOverwatching = false;

            _eventQueue.Enqueue(() =>
            {
                if (e.Proposed.Defender.State.RemainingHealth > 0)
                {
                    for (var i = 0; i < e.Proposed.DefenderBullets; i++)
                    {
                        Event.Publish(new ShotFired { Attacker = e.Proposed.Defender, Target = e.Proposed.Attacker });
                        if (_random.Next(0, 100) < e.Proposed.DefenderBlockChance)
                        {
                            Event.Publish(new ShotBlocked { Attacker = e.Proposed.Defender, Target = e.Proposed.Attacker });
                        }
                        else
                        {
                            if (_random.Next(0, 100) < e.Proposed.DefenderHitChance)
                                Event.Publish(new ShotHit { Attacker = e.Proposed.Defender, Target = e.Proposed.Attacker, DamageAmount = e.Proposed.AttackerBulletDamage });
                            else
                                Event.Publish(new ShotMissed { Attacker = e.Proposed.Defender, Target = e.Proposed.Attacker });
                        }
                    }
                    e.Proposed.Defender.State.IsOverwatching = false;
                }
                else
                {
                    Event.Publish(new ShotAnimationsFinished());
                }
            });
            _eventQueue.Enqueue(() => e.OnFinished());
        }
    }
}
