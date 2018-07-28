using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.UIEffects
{
    class BlueLightSource : IVisual
    {
        private readonly Transform2 _transform;

        public BlueLightSource(Point mapLocation)
        {
            _transform = new Transform2(new Vector2(mapLocation.X, mapLocation.Y) * TileData.RenderSize.ToVector(), new Size2(320, 320));
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw("Effects/CircleGradient", parentTransform + _transform, Color.FromNonPremultiplied(48, 140, 140, 70));
        }
    }
}
