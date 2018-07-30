using MonoDragons.Core.EventSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Tiles;

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
                options[ActionType.Shoot] = () => Select(new ShootSelected { AvailableTargets = e.Targets });
            if (CanHide(GameWorld.CurrentCharacter))
                options[ActionType.Hide] = () => Select(new HideSelected());
            if (GameWorld.CurrentCharacter.Gear.EquippedWeapon.IsRanged)
                options[ActionType.Overwatch] = () => Select(new OverwatchSelected());
            options[ActionType.Pass] = () => Select(new PassSelected());

            Event.Publish(new ActionOptionsAvailable { Options = options });
        }

        private bool CanHide(Character character)
        {
            var point = character.CurrentTile.Position;
            var directions = new List<Point>
            {
                new Point(point.X - 1, point.Y),
                new Point(point.X + 1, point.Y),
                new Point(point.X, point.Y - 1),
                new Point(point.X, point.Y + 1)
            };
            return directions.Any(x => GameWorld.Map.Exists(x) && GameWorld.Map[x].Cover > Cover.None);
        }
        
        private void Select(object selection)
        {
            Event.Publish(new ActionSelected());
            Event.Publish(selection);
        }
    }
}
