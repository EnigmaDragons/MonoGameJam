using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using System.Collections.Generic;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    class AvailableMovesView : IVisual
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly GameMap _map;

        public AvailableMovesView(GameMap map)
        {
            _map = map;
            Event.Subscribe(EventSubscription.Create<MovementOptionsAvailable>(ShowOptions, this));
            Event.Subscribe(EventSubscription.Create<MovementConfirmed>(OnMovementConfirmed, this));
        }
        
        private void OnMovementConfirmed(MovementConfirmed e)
        {
            _visuals.Clear();
        }

        private void ShowOptions(MovementOptionsAvailable e)
        {
            e.AvailableMoves.ForEach(x =>
            {
                var coloredBox = new ColoredRectangle { Transform = _map[x.X, x.Y].Transform, Color = Color.FromNonPremultiplied(200, 0, 0, 20) };
                _visuals.Add(coloredBox);
            });
        }

        public void Draw(Transform2 parentTransform)
        {
            _visuals.ForEach(x => x.Draw(parentTransform));
        }
    }
}
