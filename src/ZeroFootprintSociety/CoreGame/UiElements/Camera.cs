using Microsoft.Xna.Framework;
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
        private Point ScreenCenter = new Point(CurrentDisplay.GameWidth / 2, CurrentDisplay.GameHeight / 2);

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
            if (_destination != Position)
            {
                _transitionCompletion += 0.04f;
                Position = Vector2.Lerp(Position.ToVector2(), _destination.ToVector2(), _transitionCompletion).ToPoint(); 
            }
        }

        private void CenterOn(Transform2 transform)
        {
            MoveTo(transform.Location.ToPoint() - ScreenCenter + new Point(transform.Size.Width / 2, transform.Size.Height / 2));
        }

        private void MoveTo(Point position)
        {
            _transitionCompletion = 0f;
            _destination = position;
        }

        public void Draw(Transform2 parentTransform)
        {
#if DEBUG
            if (false)
                new ColoredRectangle { Color = Color.Yellow, Transform = new Transform2(ScreenCenter.ToVector2(), new Size2(2, 2)) }.Draw();
            UI.DrawText($"Cam: X {Position.X} Y {Position.Y}", new Vector2(0, UI.OfScreenHeight(0.96f)), Color.Yellow);
#endif
        }
    }
}
