using System.Linq;
using System.Security.Cryptography.X509Certificates;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.GUI;
using Microsoft.Xna.Framework;

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
            GameWorld.Map.Tiles.ForEach(x =>
            {
                x.Draw(Floors, parentTransform, MultiplyColor);
                x.Draw(Walls, parentTransform, MultiplyColor);
            });
            GameWorld.Highlights.Draw(parentTransform);
            GameWorld.Map.Tiles.ForEach(x =>
            {
                x.Draw(UnderChar1, parentTransform, MultiplyColor);
                x.Draw(UnderChar2, parentTransform, MultiplyColor);
            });
            chars.ForEach(x => x.Draw(parentTransform));
            GameWorld.Map.Tiles.ForEach(x =>
            {
                x.Draw(OverChar1, parentTransform, MultiplyColor);
                x.Draw(OverChar2, parentTransform, MultiplyColor);
                x.Draw(Shadows, parentTransform);
            });
            GameWorld.HighHighlights.Draw(parentTransform);
            GameWorld.Map.Tiles.ForEach(x => _fx.Draw(parentTransform, x));
            chars.ForEach(x => x.DrawUI(parentTransform));
        }
    }
}
