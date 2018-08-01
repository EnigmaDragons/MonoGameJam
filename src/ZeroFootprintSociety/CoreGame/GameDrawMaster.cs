using System.Linq;
using System.Security.Cryptography.X509Certificates;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.GUI;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Development;

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
        private const int Shadows = 10;

        static readonly Color MultiplyColor = new Color(150, 210, 255, 255);

        public void Draw(Transform2 parentTransform)
        {
            var chars = GameWorld.Characters
                .OrderBy(x => x.CurrentTile.Position.X)
                .ThenBy(x => x.CurrentTile.Position.Y).ToList();
            Perf.Time("Drew Walls + Floors", () => GameWorld.Map.Tiles.ForEach(x =>
            {
                x.Draw(Floors, parentTransform);
                x.Draw(Walls, parentTransform);
            }));
            Perf.Time("Drew Highlights", () => GameWorld.Highlights.Draw(parentTransform));
            Perf.Time("Drew Under Char Objects", () => GameWorld.Map.Tiles.ForEach(x =>
            {
                x.Draw(UnderChar1, parentTransform);
                x.Draw(UnderChar2, parentTransform);
            }));
            Perf.Time("Drew Characters", () => chars.ForEach(x => x.Draw(parentTransform)));
            Perf.Time("Drew Over Char Objects", () => GameWorld.Map.Tiles.ForEach(x =>
            {
                x.Draw(OverChar1, parentTransform);
                x.Draw(OverChar2, parentTransform);
                x.Draw(Shadows, parentTransform);
            }));
            Perf.Time("Drew High Highlights", () => GameWorld.HighHighlights.Draw(parentTransform));
            Perf.Time("Drew PostFX", () => GameWorld.Map.Tiles.ForEach(x => _fx.Draw(parentTransform, x)));
            Perf.Time("Drew Char UI", () => chars.ForEach(x => x.DrawUI(parentTransform)));
        }
    }
}
