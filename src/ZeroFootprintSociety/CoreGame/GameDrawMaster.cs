using System.Linq;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.UIEffects;

namespace ZeroFootPrintSociety.CoreGame
{
    public class GameDrawMaster : IVisual
    {
        private readonly TileFXCollection _fx = new TileFXCollection();

        const int Floors = 0;
        const int Walls = 1;
        const int UnderChar1 = 2;
        const int UnderChar2 = 3;
        const int OverChar1 = 4;
        const int OverChar2 = 5;
        const int PostFx = 6;

        public void Draw(Transform2 parentTransform)
        {
            GameWorld.Map.Tiles.ForEach(x =>
            {
                x.Draw(Floors, parentTransform);
                x.Draw(Walls, parentTransform);
            });
            GameWorld.Highlights.Draw(parentTransform);
            GameWorld.Map.Tiles.ForEach(x =>
            {
                x.Draw(UnderChar1, parentTransform);
                x.Draw(UnderChar2, parentTransform);
            });
            GameWorld.Characters.OrderBy(x => x.CurrentTile.Position.X).ThenBy(x => x.CurrentTile.Position.Y).ForEach(x => x.Draw(parentTransform));
            GameWorld.Map.Tiles.ForEach(x =>
            {
                x.Draw(OverChar1, parentTransform);
                x.Draw(OverChar2, parentTransform);
            });

            GameWorld.Map.Tiles.ForEach(x => _fx.Draw(parentTransform, x));
        }
    }
}
