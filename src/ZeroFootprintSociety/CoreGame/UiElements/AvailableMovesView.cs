using Microsoft.Xna.Framework;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using System.Linq;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Themes;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    class AvailableMovesView : MutableSceneContainer
    {
        private readonly GameMap _map;

        public AvailableMovesView(GameMap map)
        {
            _map = map;
            Event.Subscribe(EventSubscription.Create<MovementOptionsAvailable>(ShowOptions, this));
            Event.Subscribe(EventSubscription.Create<MovementConfirmed>(OnMovementConfirmed, this));
        }
        
        private void OnMovementConfirmed(MovementConfirmed e)
        {
            Clear();
        }

        private void ShowOptions(MovementOptionsAvailable e)
        {
            e.AvailableMoves.ForEach(x =>
            {
                Add(new ColoredRectangle
                {
                    Transform = GameWorld.Map.TileToWorldTransform(x.Last()).WithSize(TileData.RenderSize),
                    Color = UIColors.AvailableMovesView_Rectangles
                });
            });
        }
    }
}
