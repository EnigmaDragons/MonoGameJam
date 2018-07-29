using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using System.Linq;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Tiles;
using ZeroFootPrintSociety.UIEffects;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    class MovementPathHighlights : MutableSceneContainer
    {
        public MovementPathHighlights()
        {
            Event.Subscribe(EventSubscription.Create<MovementConfirmed>(OnMovementConfirmed, this));
            Event.Subscribe(EventSubscription.Create<MovementFinished>(OnMovementFinished, this));
        }

        private void OnMovementConfirmed(MovementConfirmed e)
        {
            var destination = e.Path.Last();
            Add(new ColoredRectangle
            {
                Transform = GameWorld.Map.TileToWorldTransform(destination).WithSize(TileData.RenderSize),
                Color = Color.FromNonPremultiplied(80, 120, 220, 20)
            });
            Add(new TileRotatingEdgesAnim(destination, Color.FromNonPremultiplied(110, 170, 255, 255)).Initialized());
            GameWorld.Highlights.Add(this);
        }

        private void OnMovementFinished(MovementFinished e)
        {
            Clear();
            GameWorld.Highlights.Remove(this);
        }
    }
}
