﻿using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class ProposedShotCalculator
    {
        public ProposedShotCalculator()
        {
            Event.Subscribe<RangedTargetInspected>(PreviewShot, this);
        }

        private void PreviewShot(RangedTargetInspected e)
        {
            var proposed = new ProposedShotCalculation(e.Attacker, e.Defender, e.AttackerBlockChance, e.DefenderBlockChance).CalculateShot();
            Event.Publish(new ActionSelected(() => Event.Publish(new ShotConfirmed { Proposed = proposed, OnFinished = () => Event.Publish(new ActionResolved()) })));
            Event.Publish(proposed);
        }
    }
}
