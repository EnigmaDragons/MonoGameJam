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
using ZeroFootPrintSociety.PhsyicsMath;
using MonoDragons.Core.Common;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    public class Gunshots : IVisualAutomaton
    {
        private readonly Random _random = new Random(Guid.NewGuid().GetHashCode());
        private readonly List<IShotVisual> _shots = new List<IShotVisual>();

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
            _shots.Add(new TargetedShotVisual(
                new RectangleTexture(Color.White).Create(), 
                CalculateTransform(e.Attacker, e.Target, _random.Next(-10, 10) * 0.001), 
                e.Target.CurrentTile.Position));
        }

        private void OnShotMissed(ShotMissed e)
        {
            _shots.Add(new MissedShotVisual(
                new RectangleTexture(Color.White).Create(), 
                CalculateTransform(e.Attacker, e.Target, (_random.Next(0, 1) == 0 ? -1 : 1) * _random.Next(2, 5) * 0.1),
                e.Attacker.CurrentTile.Position));
        }

        private void OnShotBlocked(ShotBlocked e)
        {
            var target = new ShotCalculation(e.Attacker.CurrentTile, e.Target.CurrentTile).BestShot().Covers.SelectMany(c => c.Providers).ToList().Random();
            _shots.Add(new TargetedShotVisual(
                new RectangleTexture(Color.White).Create(),
                CalculateTransform(
                    e.Attacker, 
                    target.Transform.Center(), 
                    _random.Next(-100, 100) * 0.001),
                target.Position));
        }

        private Transform2 CalculateTransform(Character attacker, Character target, double rotationModifier)
        {
            return CalculateTransform(attacker, target.CurrentTile.Transform.Center() + new Vector2(0, -24), rotationModifier);
        }

        private Transform2 CalculateTransform(Character attacker, Vector2 shotTo, double rotationModifier)
        {
            var shotFrom = attacker.CurrentTile.Transform.Center() + new Vector2(0, -24);
            var rotation = Convert.ToSingle(Math.Atan2(shotTo.Y - shotFrom.Y, shotTo.X - shotFrom.X) + rotationModifier);
            return new Transform2(shotFrom, new Rotation2(rotation), new Size2(1, 24), 1);
        }

        private interface IShotVisual : IVisualAutomaton
        {
            bool IsDone { get; }
        }

        private class MissedShotVisual : IShotVisual
        {
            private const double _speed = 0.01;
            private readonly Texture2D _texture;
            private readonly Transform2 _transform;
            private double _distanceTravled = 0;
            private Point _tile;
            public bool IsDone { get; private set; }

            public MissedShotVisual(Texture2D texture, Transform2 transform, Point shootPoint)
            {
                _texture = texture;
                _transform = transform;
                _tile = shootPoint;
            }

            public void Update(TimeSpan delta)
            {
                var distance = delta.TotalMilliseconds * _speed;
                _distanceTravled += distance;
                if (!GameWorld.Map.Exists(_tile) || GameWorld.Map[_tile].Cover == Cover.Heavy)
                    IsDone = true;
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
                    destinationRectangle: modifiedTransform.ToRectangle(),
                    sourceRectangle: null,
                    color: Color.White,
                    rotation: _transform.Rotation.Value - (float)(Math.PI / 2),
                    origin: new Vector2(1, 1),
                    effects: SpriteEffects.None,
                    layerDepth: 0.0f);
            }
        }

        private class TargetedShotVisual : IShotVisual
        {
            private const double _speed = 0.01;
            private readonly Texture2D _texture;
            private readonly Transform2 _transform;
            private readonly Point _target;
            private double _distanceTravled = 0;
            private Point _tile;
            public bool IsDone { get; private set; }

            public TargetedShotVisual(Texture2D texture, Transform2 transform, Point target)
            {
                _texture = texture;
                _transform = transform;
                _target = target;
            }

            public void Update(TimeSpan delta)
            {
                var distance = delta.TotalMilliseconds * _speed;
                _distanceTravled += distance;
                if (_tile == _target)
                    IsDone = true;
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
                    destinationRectangle: modifiedTransform.ToRectangle(),
                    sourceRectangle: null,
                    color: Color.White,
                    rotation: _transform.Rotation.Value - (float)(Math.PI / 2),
                    origin: new Vector2(1, 1),
                    effects: SpriteEffects.None,
                    layerDepth: 0.0f);
            }
        }
    }
}
