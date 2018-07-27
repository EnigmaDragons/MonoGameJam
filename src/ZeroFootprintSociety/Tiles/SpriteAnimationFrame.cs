using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace ZeroFootPrintSociety.Tiles
{
    public class SpriteAnimationFrame : IVisual
    {
        private readonly Texture2D _sprite;
        private readonly Transform2 _transform;
        public float DurationInSeconds { get; } 

        public SpriteAnimationFrame(Texture2D sprite, float scale, float durationInSeconds)
        {
            _sprite = sprite;
            _transform = new Transform2(new Size2((int)(sprite.Width * scale), (int)(sprite.Height * scale)));
            DurationInSeconds = durationInSeconds;
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw(_sprite, parentTransform + _transform);
        }
    }
}
