using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using ZeroFootPrintSociety.CoreGame.Calculators;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.CoreGame.UiElements;
using ZeroFootPrintSociety.CoreGame.UiElements.UiEvents;

namespace ZeroFootPrintSociety.CoreGame
{
    public class TacticsGame : SceneContainer
    {
        private enum MouseAction
        {
            None,
            Move,
            Shoot,
        }

        private readonly TurnBasedCombat _combat;

        private Transform2 _cameraOffset;

        private MouseState _lastMouseState;
        private MouseAction _mouseAction = MouseAction.None;
        private Point Target;
        private bool _shouldIgnoreClicks;
        
        public TacticsGame(TurnBasedCombat combatEngine)
        {
            _combat = combatEngine;
        }

        public void Init()
        {
            base.GetOffset = () => _cameraOffset;
            Event.Subscribe(EventSubscription.Create<TurnBegun>(e => UpdateCameraPosition(e), this));

            // TODO: Make Mouse Management a separate component
            Event.Subscribe(EventSubscription.Create<MovementOptionsAvailable>(e => _mouseAction = MouseAction.Move, this));
            Event.Subscribe(EventSubscription.Create<MovementConfirmed>(e => _mouseAction = MouseAction.None, this));
            Event.Subscribe(EventSubscription.Create<ShootSelected>(e => _mouseAction = MouseAction.Shoot, this));
            Event.Subscribe(EventSubscription.Create<MenuRequested>(e => _shouldIgnoreClicks = true, this));
            Event.Subscribe(EventSubscription.Create<MenuDismissed>(e => _shouldIgnoreClicks = false, this));

            new MovementOptionsCalculator();
            new ShootOptionsCalculator();

            _combat.Init();
            Add(_combat);

            Add(new AvailableMovesView(GameState.Map));
            Add(new AvailableTargetsView());
            Add(new HudView());
        }

        private void UpdateCameraPosition(TurnBegun e)
        {
            _cameraOffset = new Transform2(
                new Vector2(800, 450) -
                new Vector2(
                    GameState.CurrentCharacter.CurrentTile.Transform.Location.X, 
                    GameState.CurrentCharacter.CurrentTile.Transform.Location.Y));
        }

        public new void Update(TimeSpan delta)
        {
            var mouse = Mouse.GetState();
            if (CurrentGame.TheGame.IsActive)
            {
                var positionOnMap = new Vector2(mouse.X - _cameraOffset.Location.X, mouse.Y - _cameraOffset.Location.Y);
                var index = GameState.Map.MapPositionToTile(positionOnMap);

                if (_combat.Map.Exists(index.X, index.Y) && mouse.LeftButton == ButtonState.Pressed && _lastMouseState.LeftButton == ButtonState.Released)
                    Target = index;
                else if (_combat.Map.Exists(Target.X, Target.Y) && Target == index && mouse.LeftButton == ButtonState.Released && _lastMouseState.LeftButton == ButtonState.Pressed)
                    InvokeClickAction(index.X, index.Y);
                else if (mouse.LeftButton == ButtonState.Released && _lastMouseState.LeftButton == ButtonState.Pressed)
                    Target = new Point(-1, -1);

                _lastMouseState = mouse;
            }
            base.Update(delta);
        }

        private void InvokeClickAction(int x, int y)
        {
            if (_shouldIgnoreClicks)
                return;
            if (_mouseAction.Equals(MouseAction.Move))
                _combat.MoveTo(x, y);
            if (_mouseAction.Equals(MouseAction.Shoot))
                _combat.Shoot(x, y);
        }
    }
}
