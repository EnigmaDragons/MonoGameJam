using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    public class Gunshots : IVisualAutomaton
    {
        private readonly Random _random = new Random(Guid.NewGuid().GetHashCode());
        private readonly List<ShotVisual> _shots = new List<ShotVisual>();

        public Gunshots()
        {
            Event.Subscribe<ShotHit>(OnShotHit, this);
            Event.Subscribe<ShotMissed>(OnShotMissed, this);
            Event.Subscribe<ShotBlocked>(OnShotBlocked, this);
        }

        public void Update(TimeSpan delta)
        {
            _shots.ToList().ForEach(x => x.SecondsRemaining -= delta.TotalSeconds);
            _shots.RemoveAll(x => x.SecondsRemaining <= 0);
        }

        public void Draw(Transform2 parentTransform)
        {
            _shots.ToList().ForEach(x => x.Draw(parentTransform));
        }

        private void OnShotHit(ShotHit e)
        {
            var shotFrom = e.Attacker.CurrentTile.Transform.Center() + new Vector2(0, -24);
            var shotTo = e.Target.CurrentTile.Transform.Center() + new Vector2(0, -24);
            float rotation = Convert.ToSingle(Math.Atan2(shotTo.Y - shotFrom.Y, shotTo.X - shotFrom.X) + _random.Next(-10, 10) * 0.001);
            _shots.Add(new ShotVisual(new RectangleTexture(Color.White).Create(), new Transform2(shotFrom, new Rotation2(rotation), new Size2(1, (int)shotTo.Distance(shotFrom)), 1), 1));
        }

        private void OnShotMissed(ShotMissed e)
        {
            var shotFrom = e.Attacker.CurrentTile.Transform.Center() + new Vector2(0, -24);
            var shotTo = e.Target.CurrentTile.Transform.Center() + new Vector2(0, -24);
            float rotation = Convert.ToSingle(Math.Atan2(shotTo.Y - shotFrom.Y, shotTo.X - shotFrom.X) + (_random.Next(0, 1) == 0 ? -1 : 1) * _random.Next(2, 5) * 0.1);
            _shots.Add(new ShotVisual(new RectangleTexture(Color.White).Create(), new Transform2(shotFrom, new Rotation2(rotation), new Size2(1, (int)shotTo.Distance(shotFrom) + 100), 1), 1));
        }

        private void OnShotBlocked(ShotBlocked e)
        {
            var shotFrom = e.Attacker.CurrentTile.Transform.Center() + new Vector2(0, -24);
            var shotTo = new ShotCalculation(e.Attacker.CurrentTile, e.Target.CurrentTile).BestShot().Covers.First().Provider.Transform.Center();
            var rotation = Convert.ToSingle(Math.Atan2(shotTo.Y - shotFrom.Y, shotTo.X - shotFrom.X) + _random.Next(-100, 100) * 0.001);
            _shots.Add(new ShotVisual(new RectangleTexture(Color.White).Create(), new Transform2(shotFrom, new Rotation2(rotation), new Size2(1, (int)shotTo.Distance(shotFrom) - 20), 1), 1));
        }

        private class ShotVisual : IVisual
        {
            private Texture2D _texture;
            private Transform2 _transform;
            public double SecondsRemaining { get; set; }

            public ShotVisual(Texture2D texture, Transform2 transform, double seconds)
            {
                _texture = texture;
                _transform = transform;
                SecondsRemaining = seconds;
            }

            public void Draw(Transform2 parentTransform)
            {
                UI.SpriteBatch.Draw(texture: _texture,
                    destinationRectangle: (_transform + parentTransform).ToRectangle(),
                    sourceRectangle: null,
                    color: Color.White,
                    rotation: _transform.Rotation.Value - (float)(Math.PI / 2),
                    origin: new Vector2(0, 0),
                    effects: SpriteEffects.None,
                    layerDepth: 0.0f);
            }
        }
    }
}
