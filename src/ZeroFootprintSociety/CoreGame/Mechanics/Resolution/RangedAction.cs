using System;
using System.Collections.Generic;
using MonoDragons.Core.Common;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.ActionEvents;
using ZeroFootPrintSociety.CoreGame.Calculators;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.GUI;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Resolution
{
    public class RangedAction
    {
        private Queue<Action> _eventQueue = new Queue<Action>();

        public RangedAction()
        {
            Event.Subscribe(EventSubscription.Create<ShotConfirmed>(ResolveShot, this));
            Event.Subscribe(EventSubscription.Create<ShotAnimationsFinished>(_ => AdvanceQueue(), this));
        }

        private void AdvanceQueue()
        {
            if (_eventQueue.Count > 0)
                _eventQueue.Dequeue().Invoke();
        }

        private void ResolveShot(ShotConfirmed e)
        {
            for (var i = 0; i < e.Proposed.AttackerBullets; i++)
            {
                var blockRoll = Rng.Int(0, e.Proposed.IsDefenderHiding ? 50 : 100);
                if(blockRoll < e.Proposed.DefenderBlockInfo.BlockChance)
                {
                    e.Proposed.DefenderBlockInfo.Covers.Shuffle();
                    foreach (CoverProvided cover in e.Proposed.DefenderBlockInfo.Covers)
                        if (blockRoll < (int)cover.Cover)
                        {
                            ShotBlocked(e.Proposed.Attacker, e.Proposed.Defender, cover.Providers.Random());
                            break;
                        }
                        else
                            blockRoll -= (int)cover.Cover;
                }
                else
                {
                    if (Rng.Int(0, 100) < e.Proposed.AttackerHitChance)
                        ShotHit(e.Proposed.Attacker, e.Proposed.Defender, e.Proposed.AttackerBulletDamage);
                    else
                        ShotMissed(e.Proposed.Attacker, e.Proposed.Defender);
                }
            }
            e.Proposed.Attacker.State.IsOverwatching = false;

            _eventQueue.Enqueue(() =>
            {
                if (e.Proposed.Defender.State.RemainingHealth > 0 && e.Proposed.DefenderBulletDamage > 0)
                {
                    for (var i = 0; i < e.Proposed.DefenderBullets; i++)
                    {
                        var blockRoll = Rng.Int(0, e.Proposed.IsAttackerHiding ? 50 : 100);
                        if (blockRoll < e.Proposed.AttackerBlockInfo.BlockChance)
                        {
                            e.Proposed.AttackerBlockInfo.Covers.Shuffle();
                            foreach (CoverProvided cover in e.Proposed.AttackerBlockInfo.Covers)
                                if (blockRoll < (int) cover.Cover)
                                {
                                    ShotBlocked(e.Proposed.Defender, e.Proposed.Attacker, cover.Providers.Random());
                                    break;
                                }
                                else
                                    blockRoll -= (int) cover.Cover;
                        }
                        else
                        {
                            if (Rng.Int(0, 100) < e.Proposed.DefenderHitChance)
                                ShotHit(e.Proposed.Defender, e.Proposed.Attacker, e.Proposed.DefenderBulletDamage);
                            else
                                ShotMissed(e.Proposed.Defender, e.Proposed.Attacker);
                        }
                    }
                    e.Proposed.Defender.State.IsOverwatching = false;
                }
                AdvanceQueue();
            });
            _eventQueue.Enqueue(() =>
            {
                _eventQueue.Enqueue(() => e.OnFinished());
                AdvanceQueue();
            });
            AdvanceQueue();
        }

        private void ShotHit(Character attacker, Character defender, int damage)
        {
            _eventQueue.Enqueue(() =>
            {
                EventQueue.Instance.Add(new ShotFired {Attacker = attacker, Target = defender});
                EventQueue.Instance.Add(new ShotHit {Attacker = attacker, Target = defender, DamageAmount = damage});
            });
        }

        private void ShotMissed(Character attacker, Character defender)
        {
            _eventQueue.Enqueue(() =>
            {
                EventQueue.Instance.Add(new ShotFired {Attacker = attacker, Target = defender});
                EventQueue.Instance.Add(new ShotMissed {Attacker = attacker, Target = defender});
            });
        }

        private void ShotBlocked(Character attacker, Character defender, GameTile blocker)
        {
            _eventQueue.Enqueue(() =>
            {
                EventQueue.Instance.Add(new ShotFired {Attacker = attacker, Target = defender});
                EventQueue.Instance.Add(new ShotBlocked {Attacker = attacker, Target = defender, Blocker = blocker});
            });
        }
    }
}
