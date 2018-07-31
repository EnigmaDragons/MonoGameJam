using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using System;
using ZeroFootPrintSociety.Themes;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.UIEffects
{
    class TileFXCollection
    {
        private static readonly DictionaryWithDefault<string, Color> _colors = new DictionaryWithDefault<string, Color>(UIColors.TileFXCollection_Default)
        {
            { "blue", UIColors.TileFXCollection_Blue },
            { "turqoise", UIColors.TileFXCollection_Turquoise },
            { "red", UIColors.TileFXCollection_Red },
        };
        
        public void Draw(Transform2 parentTransform, GameTile tile)
        {
            tile.PostFX.ForEach(x =>
            {
                var fx = new FXString(x);
                string lower_name = fx.Name.ToLowerInvariant();

                //The id is formed by "X_FXNAME_COLOR", where X is the offset
                //We're searching the FX id, so we make a substring that starts from 2
                switch (lower_name.Substring(2))
                {
                    case "trianglegradient": DrawTriangleGradient(parentTransform, tile, _colors[fx.Color], fx.Offset); break;
                    case "circlegradient": DrawCircleGradient(parentTransform, tile, _colors[fx.Color], fx.Offset); break;
                    case "smallcirclegradient": DrawSmallCircleGradient(parentTransform, tile, _colors[fx.Color], fx.Offset); break;
                    case "vertneongradient": DrawVertNeonGradient(parentTransform, tile, _colors[fx.Color], fx.Offset); break;
                    default: Logger.WriteLine($"Unknown FX '{x}'"); break;
                }
            });
        }

        private void DrawTriangleGradient(Transform2 parentTransform, GameTile tile, Color color, bool x_offset)
        {
            var gradientOffset = new Vector2(-388 + (x_offset ? 24 : 0), -740);
            var loc = (TileData.RenderSize.ToPoint() * tile.Position).ToVector2();
            var t = new Transform2(loc + gradientOffset, new Size2(800, 800));
            UI.Draw("Effects/TriangleGradient", parentTransform + t, color);
        }

        private void DrawCircleGradient(Transform2 parentTransform, GameTile tile, Color color, bool x_offset)
        {
            var gradientOffset = new Vector2(-122 + (x_offset ? 24 : 0), -128);
            var loc = (TileData.RenderSize.ToPoint() * tile.Position).ToVector2();
            var t = new Transform2(loc + gradientOffset, new Size2(290, 290));
            UI.Draw("Effects/CircleGradient", parentTransform + t, color);
        }

        private void DrawSmallCircleGradient(Transform2 parentTransform, GameTile tile, Color color, bool x_offset)
        {
            var gradientOffset = new Vector2(-46 + (x_offset ? 24 : 0), -20);
            var loc = (TileData.RenderSize.ToPoint() * tile.Position).ToVector2();
            var t = new Transform2(loc + gradientOffset, new Size2(140, 140));
            UI.Draw("Effects/CircleGradient", parentTransform + t, color);
        }

        private void DrawVertNeonGradient(Transform2 parentTransform, GameTile tile, Color color, bool x_offset)
        {
            var gradientOffset = new Vector2(0 + (x_offset ? 24 : 0), -48);
            var loc = (TileData.RenderSize.ToPoint() * tile.Position).ToVector2();
            var t = new Transform2(loc + gradientOffset, new Size2(96, 192));
            UI.Draw("Effects/VertNeonGradient", parentTransform + t, color);
        }
    }
}
