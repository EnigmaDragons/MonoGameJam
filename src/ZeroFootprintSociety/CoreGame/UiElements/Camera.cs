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
using System.Linq;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    class Camera : IVisualAutomaton
    {
        private static readonly int GameWidth = CurrentDisplay.GameWidth;
        private static readonly int GameHeight = CurrentDisplay.GameHeight;
        private static readonly Point ScreenCenter = new Point(GameWidth / 2, GameHeight / 2);

        private const int TileOverage = 5;
        private const int MouseCameraSpeed = 13;

        private readonly int XLeft = UI.OfScreenWidth(0.02f);
        private readonly int XRight = UI.OfScreenWidth(0.98f);
        private readonly int YTop = UI.OfScreenHeight(0.02f);
        private readonly int YBottom = UI.OfScreenHeight(0.98f);
        private readonly int MinMapX = (GameWorld.Map.MinX - TileOverage) * TileData.RenderWidth;
        private readonly int MaxMapX = (GameWorld.Map.MaxX + TileOverage) * TileData.RenderWidth;
        private readonly int MinMapY = (GameWorld.Map.MinY - TileOverage) * TileData.RenderHeight;
        private readonly int MaxMapY = (GameWorld.Map.MaxY + TileOverage) * TileData.RenderHeight;

        private float _transitionCompletion;
        private Point _destination;
        public Point Position { get; private set; }

        public Camera()
        {
            Event.Subscribe(EventSubscription.Create<TurnBegun>(e => CenterOn(GameWorld.CurrentCharacter.CurrentTile.Transform), this));
            Event.Subscribe<MovementConfirmed>(e => CenterOn(GameWorld.Map.TileToWorldTransform(e.Path.Last())), this);
            Input.On(Control.Select, () => CenterOn(GameWorld.CurrentCharacter.CurrentTile.Transform));
        }

        public void Update(TimeSpan delta)
        {
            if (_transitionCompletion < 1f)
            {
                _transitionCompletion += 0.04f;
                SetPosition(Vector2.Lerp(Position.ToVector2(), _destination.ToVector2(), _transitionCompletion).ToPoint());
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
                SetPosition(new Point(targetX, targetY));
            }
        }

        private void CenterOn(Transform2 transform)
        {
            MoveTo(transform.Location.ToPoint() - ScreenCenter + new Point(transform.Size.Width / 2, transform.Size.Height / 2));
        }

        public void Init(Point startingCameraTile)
        {
            SetPosition(GameWorld.Map.TileToWorldPosition(startingCameraTile));
        }

        private void SetPosition(Point raw)
        {
            var clampedX = Math.Min(Math.Max(raw.X, MinMapX), MaxMapX - GameWidth);
            var clampedY = Math.Min(Math.Max(raw.Y, MinMapY), MaxMapY - GameHeight);
            Position = new Point(clampedX, clampedY);
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
