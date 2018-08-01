using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using System.Linq;
using MonoDragons.Core.Common;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Themes;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.GUI
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
                Add(new UiImage
                {
                    Image = "Effects/Cover_Gray",
                    Transform = GameWorld.Map.TileToWorldTransform(x.Last()).WithSize(TileData.RenderSize),
                    Tint = UIColors.AvailableMovesView_Rectangles
                });
            });
        }
    }
}
