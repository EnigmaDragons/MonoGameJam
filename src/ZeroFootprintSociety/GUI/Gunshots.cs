using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Graphics;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.CoreGame.ActionEvents;
using ZeroFootPrintSociety.CoreGame.Calculators;
using ZeroFootPrintSociety.PhysicsMath;
using MonoDragons.Core.Common;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.Themes;
using ZeroFootPrintSociety.Tiles;
using ZeroFootPrintSociety.CoreGame;

namespace ZeroFootPrintSociety.GUI
{
    public class Gunshots : IVisualAutomaton
    {
        private readonly Random _random = new Random(Guid.NewGuid().GetHashCode());
        private readonly List<IShotVisual> _shots = new List<IShotVisual>();
        public const double TravelSpeed = 0.085;
        
        public Gunshots()
        {
            Event.Subscribe<ShotHit>(OnShotHit, this);
            Event.Subscribe<ShotMissed>(OnShotMissed, this);
            Event.Subscribe<ShotBlocked>(OnShotBlocked, this);
        }

        public void Update(TimeSpan delta)
        {
            _shots.ToList().ForEach(x => x.Update(delta));
            _shots.RemoveAll(x => x.IsDone);
        }

        public void Draw(Transform2 parentTransform)
        {
            _shots.ToList().ForEach(x => x.Draw(parentTransform));
        }

        private void OnShotHit(ShotHit e)
        {
            _shots.Add(new HitShotVisual(
                new RectangleTexture(UiColors.Gunshot).Create(), 
                CalculateTransform(e.Attacker, e.Target, _random.Next(-10, 10) * 0.001), 
                e.Attacker,
                e.Target));
        }

        private void OnShotMissed(ShotMissed e)
        {
            _shots.Add(new MissedShotVisual(
                new RectangleTexture(UiColors.Gunshot).Create(), 
                CalculateTransform(e.Attacker, e.Target, (_random.Next(0, 1) == 0 ? -1 : 1) * _random.Next(2, 5) * 0.1),
                e.Attacker.CurrentTile.Position));
        }

        private void OnShotBlocked(ShotBlocked e)
        {
            _shots.Add(new BlockedShotVisual(
                new RectangleTexture(UiColors.Gunshot).Create(),
                CalculateTransform(
                    e.Attacker, 
                    e.Blocker.Transform.Center(), 
                    _random.Next(-100, 100) * 0.001),
                e.Blocker.Position));
        }

        private Transform2 CalculateTransform(Character attacker, Character target, double rotationModifier)
        {
            return CalculateTransform(attacker, target.CurrentTile.Transform.Center() + new Vector2(0, -24), rotationModifier);
        }

        private Transform2 CalculateTransform(Character attacker, Vector2 shotTo, double rotationModifier)
        {
            var shotFrom = attacker.CurrentTile.Transform.Center() + new Vector2(0, -24);
            var rotation = Convert.ToSingle(Math.Atan2(shotTo.Y - shotFrom.Y, shotTo.X - shotFrom.X) + rotationModifier);
            var result = new Transform2(shotFrom, new Rotation2(rotation), new Size2(1, 24), 1);
            result.Location = result.Location.MoveInDirection(result.Rotation.Value, 52);;
            return result;
        }

        private interface IShotVisual : IVisualAutomaton
        {
            bool IsDone { get; }
        }

        private class MissedShotVisual : IShotVisual
        {
            private readonly Texture2D _texture;
            private readonly Transform2 _transform;
            private double _distanceTravled = 0;
            private Point _tile;
            public bool IsDone { get; private set; }
            private bool _publishedEvent;

            public MissedShotVisual(Texture2D texture, Transform2 transform, Point shootPoint)
            {
                _texture = texture;
                _transform = transform;
                _tile = shootPoint;
            }

            public void Update(TimeSpan delta)
            {
                var distance = delta.TotalMilliseconds * TravelSpeed;
                _distanceTravled += distance;
                if (!GameWorld.Map.Exists(_tile) || GameWorld.Map[_tile].Cover == Cover.Heavy)
                    IsDone = true;

                if (!_publishedEvent && (_distanceTravled > 50 || IsDone))
                {
                    Event.Publish(new AttackAnimationsFinished());
                    _publishedEvent = true;
                }
            }

            public void Draw(Transform2 parentTransform)
            {
                var modifiedTransform = _transform;
                modifiedTransform.Location = modifiedTransform.Location.MoveInDirection(_transform.Rotation.Value, _distanceTravled);
                if (GameWorld.Map.Exists(GameWorld.Map.MapPositionToTile(modifiedTransform.Location)))
                    _tile = GameWorld.Map.MapPositionToTile(modifiedTransform.Location);
                else
                    IsDone = true;
                modifiedTransform += parentTransform;
                UI.SpriteBatch.Draw(texture: _texture,
                    destinationRectangle: UI.ScaleRectangle(modifiedTransform.ToRectangle()),
                    sourceRectangle: null,
                    color: UiColors.Gunshot_MissedShot,
                    rotation: _transform.Rotation.Value - (float)(Math.PI / 2),
                    origin: new Vector2(1, 1),
                    effects: SpriteEffects.None,
                    layerDepth: 0.0f);
            }
        }

        private class HitShotVisual : IShotVisual
        {
            private readonly Texture2D _texture;
            private readonly Transform2 _transform;
            private readonly Point _destination;
            private readonly Character _attacker;
            private readonly Character _target;
            private double _distanceTravled = 0;
            private Point _tile;
            public bool IsDone { get; private set; }
            private bool _publishedEvent;

            public HitShotVisual(Texture2D texture, Transform2 transform, Character attacker, Character target)
            {
                _texture = texture;
                _transform = transform;
                _destination = target.CurrentTile.Position;
                _attacker = attacker;
                _target = target;
            }

            public void Update(TimeSpan delta)
            {
                var distance = delta.TotalMilliseconds * TravelSpeed;
                _distanceTravled += distance;
                if (_tile == _destination)
                    IsDone = true;

                if (!_publishedEvent && (_distanceTravled > 50 || IsDone))
                {
                    Event.Publish(new AttackAnimationsFinished { Attacker = _attacker, Target = _target });
                    _publishedEvent = true;
                }
            }

            public void Draw(Transform2 parentTransform)
            {
                var modifiedTransform = _transform;
                modifiedTransform.Location =
                    modifiedTransform.Location.MoveInDirection(_transform.Rotation.Value, _distanceTravled);
                if (GameWorld.Map.Exists(GameWorld.Map.MapPositionToTile(new Vector2(modifiedTransform.Location.X,
                    modifiedTransform.Location.Y + 24))))
                    _tile = GameWorld.Map.MapPositionToTile(new Vector2(modifiedTransform.Location.X,
                        modifiedTransform.Location.Y + 24));
                else
                    IsDone = true;
                modifiedTransform += parentTransform;
                UI.SpriteBatch.Draw(texture: _texture,
                    destinationRectangle: UI.ScaleRectangle(modifiedTransform.ToRectangle()),
                    sourceRectangle: null,
                    color: UiColors.Gunshot_TargetedShotVisual,
                    rotation: _transform.Rotation.Value - (float) (Math.PI / 2),
                    origin: new Vector2(1, 1),
                    effects: SpriteEffects.None,
                    layerDepth: 0.0f);
            }
        }

        private class BlockedShotVisual : IShotVisual
        {
            private readonly Texture2D _texture;
            private readonly Transform2 _transform;
            private readonly Point _target;
            private double _distanceTraveled = 0;
            private Point _tile;
            public bool IsDone { get; private set; }
            private bool _publishedEvent;

            public BlockedShotVisual(Texture2D texture, Transform2 transform, Point target)
            {
                _texture = texture;
                _transform = transform;
                _target = target;
            }

            public void Update(TimeSpan delta)
            {
                var distance = delta.TotalMilliseconds * TravelSpeed;
                _distanceTraveled += distance;
                if (_tile == _target)
                    IsDone = true;

                if (!_publishedEvent && (_distanceTraveled > 50 || IsDone))
                {
                    Event.Publish(new AttackAnimationsFinished());
                    _publishedEvent = true;
                }
            }

            public void Draw(Transform2 parentTransform)
            {
                var modifiedTransform = _transform;
                modifiedTransform.Location = modifiedTransform.Location.MoveInDirection(_transform.Rotation.Value, _distanceTraveled);
                if (GameWorld.Map.Exists(GameWorld.Map.MapPositionToTile(modifiedTransform.Location)))
                    _tile = GameWorld.Map.MapPositionToTile(modifiedTransform.Location);
                else
                    IsDone = true;
                modifiedTransform += parentTransform;
                UI.SpriteBatch.Draw(texture: _texture,
                    destinationRectangle: UI.ScaleRectangle(modifiedTransform.ToRectangle()),
                    sourceRectangle: null,
                    color: UiColors.Gunshot_TargetedShotVisual,
                    rotation: _transform.Rotation.Value - (float)(Math.PI / 2),
                    origin: new Vector2(1, 1),
                    effects: SpriteEffects.None,
                    layerDepth: 0.0f);
            }
        }
    }
}
