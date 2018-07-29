using System.Linq;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace ZeroFootPrintSociety.CoreGame
{
    public class GameDrawMaster : IVisual
    {
        public void Draw(Transform2 parentTransform)
        {
            GameWorld.Map.Tiles.ForEach(x =>
            {
                x.Draw(0, parentTransform);
                x.Draw(1, parentTransform);
            });
            GameWorld.Highlights.Draw(parentTransform);
            GameWorld.Map.Tiles.ForEach(x =>
            {
                x.Draw(2, parentTransform);
                x.Draw(3, parentTransform);
            });
            GameWorld.Characters.OrderBy(x => x.CurrentTile.Position.X).ThenBy(x => x.CurrentTile.Position.Y).ForEach(x => x.Draw(parentTransform));
            GameWorld.Map.Tiles.ForEach(x =>
            {
                x.Draw(4, parentTransform);
                x.Draw(5, parentTransform);
            });
        }
    }
}
