using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.CoreGame.UiElements;

namespace ZeroFootPrintSociety.CoreGame
{
    public class TacticsGame : IAutomaton, IVisual
    {
        private enum MouseAction
        {
            None,
            Move,
        }

        private readonly ClickUI _clickUI = new ClickUI();
        private readonly List<IVisual> _uiVisuals = new List<IVisual>();
        private readonly List<IVisual> _worldVisuals = new List<IVisual>();
        private readonly TurnBasedCombat _combat;

        private Transform2 _cameraOffset;

        private MouseState _lastMouseState;
        private MouseAction _mouseAction = MouseAction.None;
        private Point Target;

        public TacticsGame(TurnBasedCombat combatEngine)
        {
            _combat = combatEngine;
        }

        public void Init()
        {
            Event.Subscribe(EventSubscription.Create<TurnBegun>(e => UpdateCameraPosition(), this));

            // TODO: Make Mouse Management a separate component
            Event.Subscribe(EventSubscription.Create<MovementOptionsAvailable>(e => _mouseAction = MouseAction.Move, this));
            Event.Subscribe(EventSubscription.Create<MovementConfirmed>(e => _mouseAction = MouseAction.None, this));

            _worldVisuals.Add(new AvailableMovesView(_combat.Map));
            _uiVisuals.Add(new ActionOptionsView(_clickUI));
            _combat.Init();

            // TODO: Make Camera a separate Component
            UpdateCameraPosition();
        }

        private void UpdateCameraPosition()
        {
            _cameraOffset = new Transform2(
                new Vector2(800, 450) -
                new Vector2(
                    _combat.CurrentCharacter.CurrentTile.Transform.Location.X, 
                    _combat.CurrentCharacter.CurrentTile.Transform.Location.Y));
        }

        public void Update(TimeSpan delta)
        {
            _clickUI.Update(delta);
            var mouse = Mouse.GetState();
            if (CurrentGame.TheGame.IsActive)
            {
                var positionOnMap = new Vector2(mouse.X - _cameraOffset.Location.X, mouse.Y - _cameraOffset.Location.Y);
                var tilePositionOnMap = new Vector2(positionOnMap.X - (positionOnMap.X % 48), positionOnMap.Y - (positionOnMap.Y % 48));
                var index = new Point((int)tilePositionOnMap.X / 48, (int)tilePositionOnMap.Y / 48);

                if (_combat.Map.Exists(index.X, index.Y) && mouse.LeftButton == ButtonState.Pressed && _lastMouseState.LeftButton == ButtonState.Released)
                    Target = index;
                else if (_combat.Map.Exists(Target.X, Target.Y) && Target == index && mouse.LeftButton == ButtonState.Released && _lastMouseState.LeftButton == ButtonState.Pressed)
                    InvokeClickAction(index.X, index.Y);
                else if (mouse.LeftButton == ButtonState.Released && _lastMouseState.LeftButton == ButtonState.Pressed)
                    Target = new Point(-1, -1);

                _lastMouseState = mouse;
            }
        }

        private void InvokeClickAction(int x, int y)
        {
            if (_mouseAction.Equals(MouseAction.Move))
                _combat.MoveTo(x, y);
        }

        public void Draw(Transform2 parentTransform)
        {
            _combat.Draw(parentTransform + _cameraOffset);
            _worldVisuals.ToList().ForEach(x => x.Draw(parentTransform + _cameraOffset));
            _uiVisuals.ForEach(x => x.Draw());
        }
    }
}
