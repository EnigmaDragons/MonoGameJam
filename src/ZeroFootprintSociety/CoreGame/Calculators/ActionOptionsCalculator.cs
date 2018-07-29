using MonoDragons.Core.EventSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    class ActionOptionsCalculator
    {
        public ActionOptionsCalculator()
        {
            Event.Subscribe<RangedTargetsAvailable>(SendActionOptionsAvailable, this);
        }

        private void SendActionOptionsAvailable(RangedTargetsAvailable e)
        {
            var options = new Dictionary<ActionType, Action>();
            if (e.Targets.Any())
                options[ActionType.Shoot] = () => new ShootSelected();
            if (true)
                options[ActionType.Hide] = () => new HideSelected();
            if (GameWorld.CurrentCharacter.Gear.EquippedWeapon.IsRanged)
                options[ActionType.Overwatch] = () => new OverwatchSelected();

            Event.Publish(new ActionOptionsAvailable { Options = options });
        }
    }
}
