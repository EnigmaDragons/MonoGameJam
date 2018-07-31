using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Themes;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.UIEffects
{
    class GlowEffect : IVisual
    {
        private readonly Transform2 _transform;

        public Color Tint { get; set; } = UIColors.GlowEffect_DefaultTint;

        public GlowEffect(Size2 size)
        {
            _transform = new Transform2(size);
        }

        public GlowEffect(Point tile)
        {
            _transform = new Transform2(new Vector2(tile.X, tile.Y) * TileData.RenderSize.ToVector(), new Size2(320, 320));
        }

        public void Draw(Transform2 parentTransform)
        {
            UI.Draw("Effects/CircleGradient", parentTransform + _transform, Tint);
        }
    }
}
