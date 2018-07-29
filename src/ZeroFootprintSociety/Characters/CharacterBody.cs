﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Memory;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.ActionEvents;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.PhsyicsMath;
using ZeroFootPrintSociety.Tiles;
using ZeroFootPrintSociety.UIEffects;

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
        private SpriteAnimation _currentAnimation;

        private List<Point> _path = new List<Point>();
        private bool _stopped = false;
        
        public Vector2 CurrentTileLocation { get; private set; }
        public GameTile CurrentTile => GameWorld.Map[GameWorld.Map.MapPositionToTile(CurrentTileLocation)];
        public Transform2 Transform { get; private set; }

        public CharacterBody(string characterPath, Vector2 offset, Color glowColor)
        {
            _glow = new GlowEffect(new Size2(60, 100)) { Tint = Color.FromNonPremultiplied(glowColor.R, glowColor.G, glowColor.B, 18) };
            _characterPath = characterPath;
            _offset = offset;
            Event.Subscribe(EventSubscription.Create<MovementConfirmed>(OnMovementConfirmed, this));
            Event.Subscribe(EventSubscription.Create<ShotFired>(UpdateFacing, this));
            Event.Subscribe<MoveResolved>(ContinueMoving, this);
        }

        private void OnMovementConfirmed(MovementConfirmed movement)
        {
            if (GameWorld.Turns.CurrentCharacter.Body == this)
            {
                _path = movement.Path.ToList();
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
            const double speedModifier = 0.3;
            _currentAnimation.Update(delta);
            if (_path.Any() && !_stopped)
            {
                var targetLocation = GameWorld.Map[_path.First()].Transform.Location;
                var pastLocation = CurrentTileLocation;
                CurrentTileLocation = CurrentTileLocation.MoveTowards(targetLocation, delta.TotalMilliseconds * speedModifier);
                SetFacing(pastLocation);
                if (CurrentTileLocation.X == targetLocation.X && CurrentTileLocation.Y == targetLocation.Y)
                {
                    _stopped = true;
                    Event.Publish(new Moved { Character = GameWorld.CurrentCharacter, Position = _path.First() });
                }
            }
        }

        private void ContinueMoving(MoveResolved e)
        {
            if (GameWorld.CurrentCharacter.Body == this)
            {
                _stopped = false;
                _path.RemoveAt(0);
                if (!_path.Any())
                    Event.Publish(new MovementFinished());
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

        private void SetFacing(Vector2 pastLocation)
        {
            if (pastLocation.X < CurrentTileLocation.X)
                _currentAnimation = _idleRight;
            if (pastLocation.X > CurrentTileLocation.X)
                _currentAnimation = _idleLeft;
            if (pastLocation.Y < CurrentTileLocation.Y)
                _currentAnimation = _idleDown;
            if (pastLocation.Y > CurrentTileLocation.Y)
                _currentAnimation = _idleUp;
        }

        public void Draw(Transform2 parentTransform)
        {
            _glow.Draw(parentTransform + Transform + CurrentTileLocation + _glowOff);
            _currentAnimation.Draw(parentTransform + Transform + _offset + CurrentTileLocation);
        }
    }
}
