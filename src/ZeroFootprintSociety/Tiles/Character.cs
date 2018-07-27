using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Memory;
using MonoDragons.Core.PhysicsEngine;

namespace ZeroFootPrintSociety.Tiles
{
    public class Character : IAutomaton, IVisual
    {
        private readonly string _characterPath;

        private SpriteAnimation _idleDown;
        private SpriteAnimation _idleUp;
        private SpriteAnimation _idleLeft;
        private SpriteAnimation _idleRight;
        private SpriteAnimation _currentAnimation; 

        public Vector2 Position { get; set; }

        public Character(string characterPath)
        {
            _characterPath = characterPath;
        }

        public void Init()
        {
            _idleDown = new SpriteAnimation(
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-down-1.png"), 1, 0.5f),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-down-2.png"), 1, 0.5f));
            _idleUp = new SpriteAnimation(
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-up-1.png"), 1, 0.5f),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-up-2.png"), 1, 0.5f));
            _idleLeft = new SpriteAnimation(
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-left-1.png"), 1, 0.5f),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-left-2.png"), 1, 0.5f));
            _idleRight = new SpriteAnimation(
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-right-1.png"), 1, 0.5f),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-right-2.png"), 1, 0.5f));
            _currentAnimation = _idleDown;
        }

        public void Update(TimeSpan delta)
        {
            _currentAnimation.Update(delta);
        }

        public void Draw(Transform2 parentTransform)
        {
            _currentAnimation.Draw(parentTransform + new Transform2(Position));
        }
    }
}
