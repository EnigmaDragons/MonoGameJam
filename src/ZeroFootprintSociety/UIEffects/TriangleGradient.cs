using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.UIEffects
{
    class TriangleGradient : IVisual
    {
        private readonly Transform2 _transform;

        public Color Tint { get; set; } = Color.FromNonPremultiplied(48, 140, 140, 70);

        public TriangleGradient(Size2 size)
        {
            _transform = new Transform2(size);
        }

        public TriangleGradient(Point tile)
        {
            _transform = new Transform2(new Vector2(tile.X, tile.Y) * TileData.RenderSize.ToVector(), new Size2(300, 300));
        }

        public void Draw(Transform2 parentTransform)
        {
            UI.Draw("Effects/TriangleGradient", parentTransform + _transform, Tint);
        }
    }
}
