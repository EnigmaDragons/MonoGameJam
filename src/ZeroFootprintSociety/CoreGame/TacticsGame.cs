using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.CoreGame.UiElements;

namespace ZeroFootPrintSociety.CoreGame
{
    public class TacticsGame : IAutomaton, IVisual
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly TurnBasedCombat _combat;

        private Transform2 _cameraOffset;
        private MouseState _lastMouseState;

        private Point Target;

        public TacticsGame(TurnBasedCombat combatEngine)
        {
            _combat = combatEngine;
        }

        public void Init()
        {
            _combat.Init();
            InitOffset();
            _visuals.Add(new AvailableMovesView(_combat.Map));
        }

        private void InitOffset()
        {
            _cameraOffset = new Transform2(
                new Vector2(800, 450) -
                new Vector2(
                    _combat.CurrentCharacter.CurrentTile.Transform.Location.X, 
                    _combat.CurrentCharacter.CurrentTile.Transform.Location.Y));
        }

        public void Update(TimeSpan delta)
        {
            var mouse = Mouse.GetState();
            if (CurrentGame.TheGame.IsActive)
            {
                var positionOnMap = new Vector2(mouse.X - _cameraOffset.Location.X, mouse.Y - _cameraOffset.Location.Y);
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
            _combat.Draw(parentTransform + _cameraOffset);
            _visuals.ToList().ForEach(x => x.Draw(parentTransform + _cameraOffset));
        }
    }
}
