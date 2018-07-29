using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Inputs;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    internal sealed class CameraArrowKeysControl: CameraControl
    {
        public override void Update(TimeSpan delta)
        {
            KeyboardState keys = Keyboard.GetState();
            // Add both Keyboard Up and Down status.
            int up = keys.IsKeyDown(Keys.Up) ? (int)VerticalDirection.Up : 0,
                down = keys.IsKeyDown(Keys.Down) ? (int)VerticalDirection.Down : 0,
                left = keys.IsKeyDown(Keys.Left) ? (int)HorizontalDirection.Left : 0,
                right = keys.IsKeyDown(Keys.Right) ? (int)HorizontalDirection.Right : 0;

            int targetX = (CameraSpeed * (left + right)),
                targetY = (CameraSpeed * (up + down));
            
            Offset = new Point(targetX, targetY);
        }
    }
}
