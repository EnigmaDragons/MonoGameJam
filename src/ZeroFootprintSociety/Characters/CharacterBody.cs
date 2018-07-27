using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Memory;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.Characters
{
    public sealed class CharacterBody : IAutomaton, IVisual
    {
        private readonly Vector2 _offset;
        private readonly string _characterPath;

        private SpriteAnimation _idleDown;
        private SpriteAnimation _idleUp;
        private SpriteAnimation _idleLeft;
        private SpriteAnimation _idleRight;
        private Transform2 _size;
        private SpriteAnimation _currentAnimation;

        // TODO: Make this private and have setter and getter
        public GameTile CurrentTile { get; set; }

        public CharacterBody(string characterPath, Vector2 offset)
        {
            _characterPath = characterPath;
            _offset = offset;
        }

        public void Init()
        {
            const float duration = 0.5f;
            const float scale = 1f;
            _idleDown = new SpriteAnimation(
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-down-1.png"), scale, duration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-down-2.png"), scale, duration));
            _idleUp = new SpriteAnimation(
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-up-1.png"), scale, duration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-up-2.png"), scale, duration));
            _idleLeft = new SpriteAnimation(
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-left-1.png"), scale, duration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-left-2.png"), scale, duration));
            _idleRight = new SpriteAnimation(
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-right-1.png"), scale, duration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-right-2.png"), scale, duration));
            var sprite = Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-down-1.png");
            _size = new Transform2(new Size2(sprite.Width, sprite.Height));
            _currentAnimation = _idleDown;
        }

        public void Update(TimeSpan delta)
        {
            _currentAnimation.Update(delta);
        }

        public void Draw(Transform2 parentTransform)
        {
            _currentAnimation.Draw(parentTransform + _size + _offset + 
                new Vector2(
                    CurrentTile.Transform.Location.X + ((float)(CurrentTile.Transform.Size.Width - _size.Size.Width) / 2),
                    CurrentTile.Transform.Location.Y + CurrentTile.Transform.Size.Height - _size.Size.Height));
        }
    }
}
