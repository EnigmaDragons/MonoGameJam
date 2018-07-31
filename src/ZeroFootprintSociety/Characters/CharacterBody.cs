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
using ZeroFootPrintSociety.CoreGame.ActionEvents;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.PhysicsMath;
using ZeroFootPrintSociety.Tiles;
using ZeroFootPrintSociety.GUI;

namespace ZeroFootPrintSociety.Characters
{
    public sealed class CharacterBody : IAutomaton, IVisual
    {
        private readonly Vector2 _offset;
        private readonly string _characterPath;
        private readonly GlowEffect _glow;
        private readonly Vector2 _glowOff = new Vector2(-30, -70);

        private SpriteAnimation _idleDown;
        private SpriteAnimation _idleUp;
        private SpriteAnimation _idleLeft;
        private SpriteAnimation _idleRight;
        private SpriteAnimation _runDown;
        private SpriteAnimation _runUp;
        private SpriteAnimation _runLeft;
        private SpriteAnimation _runRight;

        private SpriteAnimation _currentAnimation;

        public List<Point> Path = new List<Point>();
        public bool Stopped = false;
        public Direction Facing;

        public Vector2 CurrentTileLocation { get; private set; }
        public GameTile CurrentTile => GameWorld.Map[GameWorld.Map.MapPositionToTile(CurrentTileLocation)];
        public Transform2 Transform { get; private set; }

        public CharacterBody(string characterPath, Vector2 offset, Color glowColor)
        {
            _glow = new GlowEffect(new Size2(60, 100)) { Tint = Color.FromNonPremultiplied(glowColor.R, glowColor.G, glowColor.B, 18) };
            _characterPath = characterPath;
            _offset = offset;
            Event.Subscribe(EventSubscription.Create<ShotFired>(UpdateFacing, this));
        }

        public void Init(GameTile currentTile)
        {
            CurrentTileLocation = currentTile.Transform.Location;
            const float idleDuration = 0.5f;
            const float runDuration = 0.1f;
            const float scale = 1f;
            _idleDown = new SpriteAnimation(
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-idle-down-1.png"), scale, idleDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-idle-down-2.png"), scale, idleDuration));
            _idleUp = new SpriteAnimation(
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-idle-up-1.png"), scale, idleDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-idle-up-2.png"), scale, idleDuration));
            _idleLeft = new SpriteAnimation(
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-idle-left-1.png"), scale, idleDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-idle-left-2.png"), scale, idleDuration));
            _idleRight = new SpriteAnimation(
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-idle-right-1.png"), scale, idleDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-idle-right-2.png"), scale, idleDuration));
            _runDown = new SpriteAnimation(
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-down-1.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-down-2.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-down-3.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-down-4.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-down-5.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-down-6.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-down-7.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-down-8.png"), scale, runDuration));
            _runUp = new SpriteAnimation(                                                                         
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-up-1.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-up-2.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-up-3.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-up-4.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-up-5.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-up-6.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-up-7.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-up-8.png"), scale, runDuration));
            _runLeft = new SpriteAnimation(                                                                       
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-left-1.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-left-2.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-left-3.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-left-4.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-left-5.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-left-6.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-left-7.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-left-8.png"), scale, runDuration));
            _runRight = new SpriteAnimation(                                                                      
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-right-1.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-right-2.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-right-3.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-right-4.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-right-5.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-right-6.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-right-7.png"), scale, runDuration),
                new SpriteAnimationFrame(Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-run-right-8.png"), scale, runDuration));
            var sprite = Resources.Load<Texture2D>($"Characters/{_characterPath}/{_characterPath}-idle-down-1.png");
            Transform = new Transform2(new Vector2((float)(CurrentTile.Transform.Size.Width - sprite.Width) / 2, sprite.Height - sprite.Height), new Size2(sprite.Width, sprite.Height));
            _currentAnimation = _idleDown;
        }

        public void Update(TimeSpan delta)
        {
            const double speedModifier = 0.2;
            _currentAnimation.Update(delta);
            if (Path.Any() && !Stopped)
            {
                var targetLocation = GameWorld.Map[Path.First()].Transform.Location;
                var pastLocation = CurrentTileLocation;
                CurrentTileLocation = CurrentTileLocation.MoveTowards(targetLocation, delta.TotalMilliseconds * speedModifier);
                SetRunningFacing(pastLocation);
                if (CurrentTileLocation.X == targetLocation.X && CurrentTileLocation.Y == targetLocation.Y)
                {
                    Stopped = true;
                    SetFacing(pastLocation);
                    Event.Publish(new Moved { Character = GameWorld.CurrentCharacter, Position = Path.First() });
                }
            }
        }

        private void UpdateFacing(ShotFired obj)
        {
            if (obj.Attacker.Body.Equals(this))
                FaceToward(obj.Target);
        }

        private void FaceToward(Character other)
        {
            var delta = other.CurrentTile.Position - CurrentTile.Position;
            var useDeltaY = Math.Abs(delta.Y) > Math.Abs(delta.X);
            
            if (useDeltaY)
                _currentAnimation = delta.Y > 0 ? _idleDown : _idleUp;
            else
                _currentAnimation = delta.X > 0 ? _idleRight : _idleLeft;
        }

        private void SetRunningFacing(Vector2 pastLocation)
        {
            SetDirection(pastLocation);
            if (Facing == Direction.Right)
                _currentAnimation = _runRight;
            if (Facing == Direction.Left)
                _currentAnimation = _runLeft;
            if (Facing == Direction.Down)
                _currentAnimation = _runDown;
            if (Facing == Direction.Up)
                _currentAnimation = _runUp;
        }

        private void SetFacing(Vector2 pastLocation)
        {
            SetDirection(pastLocation);
            if (Facing == Direction.Right)
                _currentAnimation = _idleRight;
            if (Facing == Direction.Left)
                _currentAnimation = _idleLeft;
            if (Facing == Direction.Down)
                _currentAnimation = _idleDown;
            if (Facing == Direction.Up)
                _currentAnimation = _idleUp;
        }

        private void SetDirection(Vector2 pastLocation)
        {
            if (pastLocation.X < CurrentTileLocation.X)
                Facing = Direction.Right;
            if (pastLocation.X > CurrentTileLocation.X)
                Facing = Direction.Left;
            if (pastLocation.Y < CurrentTileLocation.Y)
                Facing = Direction.Down;
            if (pastLocation.Y > CurrentTileLocation.Y)
                Facing = Direction.Up;
        }

        public Transform2 GetTransform()
        {
            return Transform + _offset + CurrentTileLocation;
        }

        public void Draw(Transform2 parentTransform)
        {
            _glow.Draw(parentTransform + Transform + CurrentTileLocation + _glowOff);
            _currentAnimation.Draw(parentTransform + GetTransform());
        }
    }
}
