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
using System.Collections.Generic;
using System.Linq;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.CoreGame.UiElements.UiEvents;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    partial class Camera : IVisualAutomaton
    {
        private static readonly int GameWidth = CurrentDisplay.GameWidth;
        private static readonly int GameHeight = CurrentDisplay.GameHeight;
        private static readonly Point ScreenCenter = new Point(GameWidth / 2, GameHeight / 2);

        private const int TileOverage = 5;

        private readonly CameraEdgesMouseControl _edgesMouseControl;
        private readonly CameraArrowKeysControl _arrowKeysControl;
        private readonly CameraDragMouseControl _dragMouseControl;

        private readonly List<CameraControl> _cameraControls;

        private readonly int MinMapX = (GameWorld.Map.MinX - TileOverage) * TileData.RenderWidth;
        private readonly int MaxMapX = (GameWorld.Map.MaxX + TileOverage) * TileData.RenderWidth;
        private readonly int MinMapY = (GameWorld.Map.MinY - TileOverage) * TileData.RenderHeight;
        private readonly int MaxMapY = (GameWorld.Map.MaxY + TileOverage) * TileData.RenderHeight;

        private float _transitionCompletion;
        private Point _destination;
        public Point Position { get; private set; }
        private bool _shouldFreezeCamera;
        private readonly CameraOptions _cameraOptions;

        public Camera(CameraOptions options = null)
        {
            _cameraOptions = options ?? new CameraOptions();

            // Note: Don't change the ordering, it's important!
            _cameraControls = new List<CameraControl>
            {
                new CameraDragMouseControl() { CustomCanUpdateFunc = () => _cameraOptions.UseRightClickDrag },
                new CameraEdgesMouseControl(){ CameraSpeed = 13 },
                new CameraArrowKeysControl() { CameraSpeed = 7 }
            };

            Event.Subscribe<TurnBegun>(e => CenterOn(GameWorld.CurrentCharacter.CurrentTile.Transform), this);
            Event.Subscribe<MovementConfirmed>(e => CenterOn(GameWorld.Map.TileToWorldTransform(
                e.Path.Count > 0
                    ? e.Path.Last()
                    : GameWorld.CurrentCharacter.CurrentTile.Position)), this);
            Event.Subscribe<MenuRequested>(e => _shouldFreezeCamera = true, this);
            Event.Subscribe<MenuDismissed>(e => _shouldFreezeCamera = false, this);
            Input.On(Control.Select, () => CenterOn(GameWorld.CurrentCharacter.CurrentTile.Transform));
        }

        public void Update(TimeSpan delta)
        {
            if (_shouldFreezeCamera)
                return;

            if (_transitionCompletion < 1f)
            {
                _transitionCompletion += 0.04f;
                SetPosition(Vector2.Lerp(Position.ToVector2(), _destination.ToVector2(), _transitionCompletion).ToPoint());
                return;
            }

            if (CurrentGame.TheGame.IsActive)
            {
                foreach (CameraControl cameraControl in _cameraControls)
                {
                    if (cameraControl.CanUpdate() && cameraControl.CustomCanUpdateFunc())
                    {
                        cameraControl.Update(delta);
                        Position += cameraControl.Offset;
                        if (cameraControl.TestBreakAfterUpdate())
                            return;
                    }
                }
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

        public void SetPosition(Point raw)
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
                new ColoredRectangle { Color = color, Transform = new Transform2(new Vector2(_edgesMouseControl.XLeft, 0), new Size2(1, 2000)) }.Draw();
                new ColoredRectangle { Color = color, Transform = new Transform2(new Vector2(_edgesMouseControl.XRight, 0), new Size2(1, 2000)) }.Draw();
                new ColoredRectangle { Color = color, Transform = new Transform2(new Vector2(0, _edgesMouseControl.YTop), new Size2(2000, 1)) }.Draw();
                new ColoredRectangle { Color = color, Transform = new Transform2(new Vector2(0, _edgesMouseControl.YBottom), new Size2(2000, 1)) }.Draw();
            }
            UI.DrawText($"Mouse: X {Mouse.GetState().X} Y {Mouse.GetState().Y}", new Vector2(0, 88), color, font);
            UI.DrawText($"Cam: X {Position.X} Y {Position.Y}", new Vector2(0, 112), color, font);
#endif
        }
    }
}
