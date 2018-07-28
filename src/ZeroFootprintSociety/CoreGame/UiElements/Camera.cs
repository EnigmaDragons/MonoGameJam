using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Development;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using MonoDragons.Core.UserInterface;
using System;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    class Camera : IVisualAutomaton
    {
        private static Point ScreenCenter = new Point(CurrentDisplay.GameWidth / 2, CurrentDisplay.GameHeight / 2);
        private readonly int XLeft = UI.OfScreenWidth(0.02f);
        private readonly int XRight = UI.OfScreenWidth(0.98f);
        private readonly int YTop = UI.OfScreenHeight(0.02f);
        private readonly int YBottom = UI.OfScreenHeight(0.98f);
        private readonly int MouseCameraSpeed = 13;

        private float _transitionCompletion;
        private Point _destination;
        public Point Position { get; private set; }

        public Camera()
        {
            Event.Subscribe(EventSubscription.Create<TurnBegun>(e => CenterOn(GameWorld.CurrentCharacter.CurrentTile.Transform), this));
            Input.On(Control.Select, () => CenterOn(GameWorld.CurrentCharacter.CurrentTile.Transform));
        }

        public void Update(TimeSpan delta)
        {
            if (_transitionCompletion < 1f)
            {
                _transitionCompletion += 0.04f;
                Position = Vector2.Lerp(Position.ToVector2(), _destination.ToVector2(), _transitionCompletion).ToPoint();
                return;
            }

            UpdateCameraBasedOnMousePosition();
        }

        private void UpdateCameraBasedOnMousePosition()
        {
            if (CurrentGame.TheGame.IsActive)
            {
                var mouse = Mouse.GetState();
                var _hDir = mouse.X < XLeft ? HorizontalDirection.Left : mouse.X > XRight ? HorizontalDirection.Right : HorizontalDirection.None;
                var targetX = Position.X + (MouseCameraSpeed * (int)_hDir);
                var _vDir = mouse.Y < YTop ? VerticalDirection.Up : mouse.Y > YBottom ? VerticalDirection.Down : VerticalDirection.None;
                var targetY = Position.Y + (MouseCameraSpeed * (int)_vDir);
                Position = new Point(targetX, targetY);
            }
        }

        private void CenterOn(Transform2 transform)
        {
            MoveTo(transform.Location.ToPoint() - ScreenCenter + new Point(transform.Size.Width / 2, transform.Size.Height / 2));
        }

        public void Init(Point startingCameraTile)
        {
            Position = GameWorld.Map.TileToWorldPosition(startingCameraTile);
        }

        private void MoveTo(Point position)
        {
            _transitionCompletion = 0f;
            _destination = position;
        }

        public void Draw(Transform2 parentTransform)
        {
#if DEBUG
            var color = DevText.Color;
            var font = DevText.Font;
            if (false)
            {
                new ColoredRectangle { Color = color, Transform = new Transform2(ScreenCenter.ToVector2(), new Size2(2, 2)) }.Draw();
                new ColoredRectangle { Color = color, Transform = new Transform2(new Vector2(XLeft, 0), new Size2(1, 2000)) }.Draw();
                new ColoredRectangle { Color = color, Transform = new Transform2(new Vector2(XRight, 0), new Size2(1, 2000)) }.Draw();
                new ColoredRectangle { Color = color, Transform = new Transform2(new Vector2(0, YTop), new Size2(2000, 1)) }.Draw();
                new ColoredRectangle { Color = color, Transform = new Transform2(new Vector2(0, YBottom), new Size2(2000, 1)) }.Draw();
            }
            UI.DrawText($"Mouse: X {Mouse.GetState().X} Y {Mouse.GetState().Y}", new Vector2(0, 88), color, font);
            UI.DrawText($"Cam: X {Position.X} Y {Position.Y}", new Vector2(0, 112), color, font);
#endif
        }
    }
}
