using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Themes;
using ZeroFootPrintSociety.Tiles;

using ZeroFootPrintSociety.CoreGame;

namespace ZeroFootPrintSociety.GUI
{
    class MovementPathHighlights : MutableSceneContainer
    {
        public MovementPathHighlights()
        {
            // TODO: Draw that highlight on mouseover when MovementOptionsAvailable publishes. (maybe?)
            Event.Subscribe(EventSubscription.Create<MovementConfirmed>(OnMovementConfirmed, this));
            Event.Subscribe(EventSubscription.Create<MovementFinished>(_ => OnMovementFinished(), this));
            Event.Subscribe(EventSubscription.Create<TurnEnded>(_ => OnMovementFinished(), this));
        }

        private void OnMovementConfirmed(MovementConfirmed e)
        {
            var destination = e.Path.Last();
            if (GameWorld.CurrentCharacter.Team != Team.Friendly && !GameWorld.FriendlyPerception.ContainsKey(destination))
                return;

            Add(new UiImage
            {
                Image = "Effects/Cover_Gray",
                Transform = GameWorld.Map.TileToWorldTransform(destination).WithSize(TileData.RenderSize),
                Tint = UIColors.AvailableMovesView_Rectangles
            });

            Add(new TileRotatingEdgesAnim(destination, Color.FromNonPremultiplied(110, 170, 255, 255)).Initialized());
        }

        private void OnMovementFinished()
        {
            Clear();
        }
    }
}
