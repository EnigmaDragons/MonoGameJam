using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    internal sealed class CameraEdgesMouseControl : CameraControl
    {
        public readonly int XLeft = UI.OfScreenWidth(0.02f);
        public readonly int XRight = UI.OfScreenWidth(0.98f);
        public readonly int YTop = UI.OfScreenHeight(0.02f);
        public readonly int YBottom = UI.OfScreenHeight(0.98f);

        public override void Update(TimeSpan delta)
        {
            var mouse = Mouse.GetState();

            var hDir = mouse.X < XLeft ? HorizontalDirection.Left : mouse.X > XRight ? HorizontalDirection.Right : HorizontalDirection.None;
            var vDir = mouse.Y < YTop ? VerticalDirection.Up : mouse.Y > YBottom ? VerticalDirection.Down : VerticalDirection.None;

            int targetX = CameraSpeed * (int)hDir,
                targetY = CameraSpeed * (int)vDir;

            Offset = new Point(targetX, targetY);
        }
    }
}
