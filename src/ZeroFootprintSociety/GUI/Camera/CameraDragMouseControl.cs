using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;

namespace ZeroFootPrintSociety.GUI
{
    internal sealed class CameraDragMouseControl: CameraControl
    {
        public bool IsDragging { get; private set; }
        private Point? oldMousePos;

        public override void Update(TimeSpan delta)
        {
            MouseState mouse = Mouse.GetState();
            IsDragging = mouse.RightButton == ButtonState.Pressed;
            Point oldPosition = oldMousePos ?? mouse.Position;
            if (!IsDragging)
            {
                oldMousePos = null; 
                Offset = Point.Zero;
            }
            else
            {
                Offset = oldPosition - mouse.Position;
                oldMousePos = mouse.Position;
            }
        }

        public override bool TestBreakAfterUpdate() => IsDragging;
    }
}
