﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using MonoTiled.Tiled.TmxLoading;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame
{
    public class TacticsGame : IAutomaton, IVisual
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly TurnBasedCombat _combat;

        private Transform2 _offset;
        private MouseState _lastMouseState;

        private Point Target;

        public TacticsGame(string tmx)
        {
            _combat = new TurnBasedCombat(new GameMapFactory().CreateGameMap(new Tmx(CurrentGame.GraphicsDevice, tmx), new Size2(48, 48)), 
                new List<Character>
                {
                    new Character("CorporateSecurity", 3, ShowMoveOptions),
                    new Character("CorporateSecurity", 4, ShowMoveOptions)
                });
        }

        public void Init()
        {
            _combat.Init();
            _offset = new Transform2(new Vector2(-_combat.CurrentCharacter.CurrentTile.Transform.Location.X, -_combat.CurrentCharacter.CurrentTile.Transform.Location.Y)) 
                + new Transform2(new Vector2(800, 450));
            _combat.AvailableMoves.ForEach(x =>
            {
                var coloredBox = new ColoredRectangle { Transform = _combat.Map[x.X, x.Y].Transform, Color = Color.FromNonPremultiplied(200, 0, 0, 100) };
                _visuals.Add(coloredBox);
            });
        }

        public void ShowMoveOptions()
        {
            _offset = new Transform2(new Vector2(-_combat.CurrentCharacter.CurrentTile.Transform.Location.X, -_combat.CurrentCharacter.CurrentTile.Transform.Location.Y))
                      + new Transform2(new Vector2(800, 450));
            _visuals.Clear();
            _combat.AvailableMoves.ForEach(x =>
            {
                var coloredBox = new ColoredRectangle { Transform = _combat.Map[x.X, x.Y].Transform, Color = Color.FromNonPremultiplied(200, 0, 0, 100) };
                _visuals.Add(coloredBox);
            });
        }

        public void Update(TimeSpan delta)
        {
            var mouse = Mouse.GetState();
            if (CurrentGame.TheGame.IsActive)
            {
                var positionOnMap = new Vector2(mouse.X - _offset.Location.X, mouse.Y - _offset.Location.Y);
                var tilePositionOnMap = new Vector2(positionOnMap.X - (positionOnMap.X % 48), positionOnMap.Y - (positionOnMap.Y % 48));
                var index = new Point((int)tilePositionOnMap.X / 48, (int)tilePositionOnMap.Y / 48);
                if (_combat.Map.Exists(index.X, index.Y) && mouse.LeftButton == ButtonState.Pressed && _lastMouseState.LeftButton == ButtonState.Released)
                    Target = index;
                else if (_combat.Map.Exists(Target.X, Target.Y) && Target == index && mouse.LeftButton == ButtonState.Released && _lastMouseState.LeftButton == ButtonState.Pressed)
                    _combat.MoveTo(index.X, index.Y);
                else if (mouse.LeftButton == ButtonState.Released && _lastMouseState.LeftButton == ButtonState.Pressed) 
                    Target = new Point(-1, -1);
                _lastMouseState = mouse;
            }
        }

        public void Draw(Transform2 parentTransform)
        {
            _combat.Draw(parentTransform + _offset);
            _visuals.ToList().ForEach(x => x.Draw(parentTransform + _offset));
        }
    }
}
