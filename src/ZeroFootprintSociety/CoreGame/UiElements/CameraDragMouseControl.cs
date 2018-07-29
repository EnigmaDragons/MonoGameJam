using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    internal sealed class CameraDragMouseControl: CameraControl
    {
        public bool IsDragging { get; private set; } = false;

        public override void Update(TimeSpan delta)
        {
            Offset = Point.Zero;
            return;
            bool weGood = CurrentGame.TheGame.IsActive && Mouse.GetState().RightButton == ButtonState.Pressed;
            if (weGood)
            {
                // Take last mouse position since last 

            }
        }

        public override bool TestBreakAfterUpdate() => IsDragging;
    }
}
