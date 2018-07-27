using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    public class AvailableTargetsView : IVisual
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly GameMap _map;
        private readonly List<Character> _characters;

        public AvailableTargetsView(GameMap map, List<Character> characters)
        {
            _map = map;
            _characters = characters;
            Event.Subscribe(EventSubscription.Create<ShootSelected>(ShowOptions, this));
        }

        private void OnMovementConfirmed(MovementConfirmed e)
        {
            _visuals.Clear();
        }

        private void ShowOptions(ShootSelected e)
        {
        }

        public void Draw(Transform2 parentTransform)
        {
            _visuals.ForEach(x => x.Draw(parentTransform));
        }
    }
}
