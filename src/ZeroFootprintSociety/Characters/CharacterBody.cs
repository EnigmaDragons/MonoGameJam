using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Memory;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.PhsyicsMath;
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
        private SpriteAnimation _currentAnimation;

        private List<Point> _path = new List<Point>();

        public Vector2 CurrentTileLocation { get; private set; }
        public GameTile CurrentTile => GameWorld.Map[GameWorld.Map.MapPositionToTile(CurrentTileLocation)];
        public Transform2 Transform { get; private set; }

        public CharacterBody(string characterPath, Vector2 offset)
        {
            _characterPath = characterPath;
            _offset = offset;
            Event.Subscribe(EventSubscription.Create<MovementConfirmed>(OnMovementConfirmed, this));
        }

        private void OnMovementConfirmed(MovementConfirmed movement)
        {
            if (GameWorld.Turns.CurrentCharacter.Body == this)
            {
                _path = movement.Path;

            }
        }

        public void Init(GameTile currentTile)
        {
            CurrentTileLocation = currentTile.Transform.Location;
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
            Transform = new Transform2(new Vector2((float)(CurrentTile.Transform.Size.Width - sprite.Width) / 2, sprite.Height - sprite.Height), new Size2(sprite.Width, sprite.Height));
            _currentAnimation = _idleDown;
        }

        public void Update(TimeSpan delta)
        {
            _currentAnimation.Update(delta);
            if (_path.Any())
            {
                var targetLocation = GameWorld.Map[_path.First()].Transform.Location;
                CurrentTileLocation = CurrentTileLocation.MoveTowards(targetLocation, delta.TotalMilliseconds);
                if (CurrentTileLocation.X == targetLocation.X && CurrentTileLocation.Y == targetLocation.Y)
                    _path.RemoveAt(0);
                if (!_path.Any())
                    Event.Publish(new MovementFinished());
            }
        }

        public void Draw(Transform2 parentTransform)
        {
            _currentAnimation.Draw(parentTransform + Transform + _offset + CurrentTileLocation);
        }
    }
}
