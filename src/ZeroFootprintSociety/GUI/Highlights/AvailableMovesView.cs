using System;
using System.Collections.Generic;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using System.Linq;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Themes;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.GUI
{
    class AvailableMovesView : IVisual
    {
        private readonly GameMap _map;
        private List<IVisual> _visuals = new List<IVisual>();

        public AvailableMovesView(GameMap map)
        {
            _map = map;
            Event.Subscribe(EventSubscription.Create<MovementOptionsAvailable>(ShowOptions, this));
            Event.Subscribe(EventSubscription.Create<MovementConfirmed>(OnMovementConfirmed, this));
        }
        
        private void OnMovementConfirmed(MovementConfirmed e)
        {
            _visuals = new List<IVisual>();
        }

        private void ShowOptions(MovementOptionsAvailable e)
        {
            if (!GameWorld.FriendlyPerception[GameWorld.CurrentCharacter.CurrentTile.Position])
                return;
            var visuals = new List<IVisual>();
            e.AvailableMoves.ForEach(x =>
            {
                visuals.Add(new UiImage
                {
                    Image = "Effects/Cover_Gray",
                    Transform = GameWorld.Map.TileToWorldTransform(x.Last()).WithSize(TileData.RenderSize),
                    Tint = UiColors.AvailableMovesView_Rectangles
                });
            });
            _visuals = visuals;
        }

        public void Draw(Transform2 parentTransform)
        {
            _visuals.ToList().ForEach(x => x.Draw(parentTransform));
        }
    }
}
