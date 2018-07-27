using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Memory;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.CoreGame;

namespace ZeroFootPrintSociety.Tiles
{
    public class Character : IAutomaton, IVisual
    {
        private readonly string _characterPath;

        private SpriteAnimation _idleDown;
        private SpriteAnimation _idleUp;
        private SpriteAnimation _idleLeft;
        private SpriteAnimation _idleRight;
        private Transform2 _size;
        private SpriteAnimation _currentAnimation;
        
        public GameTile CurrentTile { get; set; }
        public int Speed { get; }
        public Action OnTurnStart { get; }

        public Character(string characterPath, int speed, Action onTurnStart)
        {
            _characterPath = characterPath;
            Speed = speed;
            OnTurnStart = onTurnStart;
        }

        public void Init()
        {
            _idleDown = new SpriteAnimation(
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-down-1.png"), 2, 0.5f),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-down-2.png"), 2, 0.5f));
            _idleUp = new SpriteAnimation(
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-up-1.png"), 2, 0.5f),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-up-2.png"), 2, 0.5f));
            _idleLeft = new SpriteAnimation(
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-left-1.png"), 2, 0.5f),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-left-2.png"), 2, 0.5f));
            _idleRight = new SpriteAnimation(
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-right-1.png"), 2, 0.5f),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-right-2.png"), 2, 0.5f));
            var sprite = Resources.Load<Texture2D>($"Characters/{_characterPath}-idle-down-1.png");
            _size = new Transform2(new Size2(sprite.Width * 2, sprite.Height * 2));
            _currentAnimation = _idleDown;
        }

        public void Update(TimeSpan delta)
        {
            _currentAnimation.Update(delta);
        }

        public void Draw(Transform2 parentTransform)
        {
            _currentAnimation.Draw(parentTransform + _size + new Transform2(new Vector2(
                    CurrentTile.Transform.Location.X + ((float)(CurrentTile.Transform.Size.Width - _size.Size.Width) / 2),
                    CurrentTile.Transform.Location.Y + CurrentTile.Transform.Size.Height - _size.Size.Height)));
        }
    }
}
