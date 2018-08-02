using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Development;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Tiles;
using ZeroFootPrintSociety.CoreGame;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using Direction = ZeroFootPrintSociety.Characters.Direction;

namespace ZeroFootPrintSociety.GUI
{
    class Camera : IVisualAutomaton
    {
        private static readonly int GameWidth = CurrentDisplay.GameWidth;
        private static readonly int GameHeight = CurrentDisplay.GameHeight;
        private static readonly Point ScreenCenter = new Point(GameWidth / 2, GameHeight / 2);

        private const int TileOverage = 8;
        
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
                //new CameraEdgesMouseControl(){ CameraSpeed = 13 }, // This one is annoying. Probably leave it off.
                new CameraArrowKeysControl() { CameraSpeed = 7 }
            };

            Event.Subscribe<TurnBegun>(e => IfPerceivable(GameWorld.CurrentCharacter.CurrentTile.Position, 
                () => CenterOn(GameWorld.CurrentCharacter.CurrentTile.Transform)), this);
            Event.Subscribe<Moved>(e => IfPerceivable(GameWorld.CurrentCharacter.CurrentTile.Position, 
                () =>
                {
                    if (!GameWorld.CurrentCharacter.IsFriendly)
                        CenterOn(GameWorld.Map.TileToWorldTransform(GameWorld.CurrentCharacter.CurrentTile.Position));
                }), this);
            Event.Subscribe<MovementConfirmed>(e =>
                {
                    if (GameWorld.CurrentCharacter.IsFriendly)
                        CenterOn(GameWorld.Map.TileToWorldTransform(AheadOfPoint(GameWorld.CurrentCharacter.CurrentTile.Position, e.Path.Last())));
                }, this);
            Event.Subscribe<EnemySpotted>(e => CenterOn(GameWorld.Map.TileToWorldTransform(e.Enemy.CurrentTile.Position)), this);
            Event.Subscribe<ShootSelected>(e => CenterOn(GameWorld.CurrentCharacter), this);
            Event.Subscribe<RangedTargetInspected>(e => CenterOn(new Transform2(new Vector2((e.Attacker.CurrentTile.Transform.Location.X + e.Defender.CurrentTile.Transform.Location.X) / 2, (e.Attacker.CurrentTile.Transform.Location.Y + e.Defender.CurrentTile.Transform.Location.Y) / 2))), this);
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
                        Position = ClampToWorldEdges(Position + cameraControl.Offset);
                        if (cameraControl.TestBreakAfterUpdate())
                            return;
                    }
                }
            }
        }

        private Point AheadOfPoint(Point start, Point dest)
        {
            var directions = start.PrimaryDirectionsTowards(dest);
            return new Point(
                dest.X + (directions.Contains(Direction.Left) ? -3 : 0) + (directions.Contains(Direction.Right) ? 3 : 0),
                dest.Y + (directions.Contains(Direction.Up) ? -3 : 0) + (directions.Contains(Direction.Down) ? 3 : 0));
        }

        private void IfPerceivable(Point tile, Action action)
        {
            if (GameWorld.FriendlyPerception[tile])
                action();
        } 
        
        private void CenterOn(Character ch)
        {
            CenterOn(ch.Body.CurrentTile.Transform);
        }
        
        private void CenterOn(Transform2 transform)
        {
            MoveTo(transform.Location.ToPoint() - ScreenCenter + new Point(transform.Size.Width / 2, transform.Size.Height / 2));
        }

        public void Init(Point startingCameraTile)
        {
            SetPosition(GameWorld.Map.TileToWorldPosition(startingCameraTile));
        }

        private Point ClampToWorldEdges(Point raw)
        {
            var clampedX = Math.Min(Math.Max(raw.X, MinMapX), MaxMapX - GameWidth);
            var clampedY = Math.Min(Math.Max(raw.Y, MinMapY), MaxMapY - GameHeight);
            return new Point(clampedX, clampedY);
        }

        public void SetPosition(Point raw)
        {
            Position = ClampToWorldEdges(raw);
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
            UI.DrawText($"Mouse: X {Mouse.GetState().X} Y {Mouse.GetState().Y}", new Vector2(0, 88), color, font);
            UI.DrawText($"Cam: X {Position.X} Y {Position.Y}", new Vector2(0, 112), color, font);
#endif
        }
    }
}
