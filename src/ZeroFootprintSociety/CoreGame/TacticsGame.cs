using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using ZeroFootPrintSociety.AI;
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
        private readonly Point _startingCameraTile;
        private readonly Camera _camera = new Camera();
        
        private MouseState _lastMouseState;
        private MouseAction _mouseAction = MouseAction.None;
        private Point Target;
        private GameDrawMaster _drawMaster = new GameDrawMaster();
        private bool _shouldIgnoreClicks;

        public TacticsGame(TurnBasedCombat combatEngine, Point startingCameraTile)
        {
            _combat = combatEngine;
            _startingCameraTile = startingCameraTile;
        }

        public void Init()
        {
            base.GetOffset = () => new Transform2(-_camera.Position.ToVector2());

            // TODO: Make Mouse Management a separate component
            Event.Subscribe(EventSubscription.Create<MovementOptionsAvailable>(e => _mouseAction = MouseAction.Move, this));
            Event.Subscribe(EventSubscription.Create<MovementConfirmed>(e => _mouseAction = MouseAction.None, this));
            Event.Subscribe(EventSubscription.Create<ShootSelected>(e => _mouseAction = MouseAction.Shoot, this));
            Event.Subscribe(EventSubscription.Create<MenuRequested>(e => _shouldIgnoreClicks = true, this));
            Event.Subscribe(EventSubscription.Create<MenuDismissed>(e => _shouldIgnoreClicks = false, this));

            Add(new EnemyAI());
            Add(new ActionOptionsCalculator());
            Add(new HideUI());
            Add(new MovementOptionsCalculator());
            Add(new ShootOptionsCalculator());
            Add(new AvailableMovesView(GameWorld.Map));
            Add(new AvailableTargetsView());
            _combat.Init();
            Add(_drawMaster);
            Add(_combat);
            Add(new HudView());
            Add(_camera);

            _camera.Init(_startingCameraTile);
        }

        public override void Update(TimeSpan delta)
        {
            var mouse = Mouse.GetState();
            if (CurrentGame.TheGame.IsActive)
            {
                var positionOnMap = mouse.Position + _camera.Position;
                var tilePoint = GameWorld.Map.MapPositionToTile(positionOnMap.ToVector2());
                GameWorld.HoveredTile = tilePoint;

                if (_combat.Map.Exists(tilePoint.X, tilePoint.Y) && mouse.LeftButton == ButtonState.Pressed && _lastMouseState.LeftButton == ButtonState.Released)
                    Target = tilePoint;
                else if (_combat.Map.Exists(Target.X, Target.Y) && Target == tilePoint && mouse.LeftButton == ButtonState.Released && _lastMouseState.LeftButton == ButtonState.Pressed)
                    InvokeClickAction(tilePoint.X, tilePoint.Y);
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
