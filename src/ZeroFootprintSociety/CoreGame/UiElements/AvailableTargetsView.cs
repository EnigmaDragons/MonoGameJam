using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    public class AvailableTargetsView : IVisual
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private List<Character> _availableTargets = new List<Character>();

        public AvailableTargetsView()
        {
            Event.Subscribe(EventSubscription.Create<RangedTargetsAvailable>(e => _availableTargets = e.Targets, this));
            Event.Subscribe(EventSubscription.Create<ShootSelected>(ShowOptions, this));
            Event.Subscribe(EventSubscription.Create<ShotConfirmed>(x => _visuals.Clear(), this));
        }

        private void ShowOptions(ShootSelected e)
        {
            _availableTargets.ForEach(x =>
            {
                var coloredBox = new ColoredRectangle { Transform = x.CurrentTile.Transform, Color = Color.FromNonPremultiplied(200, 0, 0, 50) };
                _visuals.Add(coloredBox);
            });
        }

        public void Draw(Transform2 parentTransform)
        {
            _visuals.ToList().ForEach(x => x.Draw(parentTransform));
        }
    }
}
