using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using System;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.UIEffects
{
    class TileFXCollection
    {
        private static readonly DictionaryWithDefault<string, Color> _colors = new DictionaryWithDefault<string, Color>(Color.FromNonPremultiplied(255, 255, 0, 120))
        {
            { "blue", Color.FromNonPremultiplied(30, 90, 230, 60) },
            { "red", Color.FromNonPremultiplied(255, 0, 0, 60) },
        };
        
        public void Draw(Transform2 parentTransform, GameTile tile)
        {
            tile.PostFX.ForEach(x =>
            {
                var fx = new FXString(x);
                if (fx.Name.Equals("trianglegradient", StringComparison.InvariantCultureIgnoreCase))
                    DrawTriangleGradient(parentTransform, tile, _colors[fx.Color]);
                if (fx.Name.Equals("circlegradient", StringComparison.InvariantCultureIgnoreCase))
                    DrawCircleGradient(parentTransform, tile, _colors[fx.Color]);
            });
        }

        private void DrawTriangleGradient(Transform2 parentTransform, GameTile tile, Color color)
        {
            var gradientOffset = new Vector2(-388, -740);
            var loc = (TileData.RenderSize.ToPoint() * tile.Position).ToVector2();
            var t = new Transform2(loc + gradientOffset, new Size2(800, 800));
            UI.Draw("Effects/TriangleGradient", parentTransform + t, color);
        }

        private void DrawCircleGradient(Transform2 parentTransform, GameTile tile, Color color)
        {
            var gradientOffset = new Vector2(-122, -128);
            var loc = (TileData.RenderSize.ToPoint() * tile.Position).ToVector2();
            var t = new Transform2(loc + gradientOffset, new Size2(290, 290));
            UI.Draw("Effects/CircleGradient", parentTransform + t, color);
        }
    }
}
