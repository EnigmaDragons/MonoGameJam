using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using System;

namespace MonoDragons.Core.Render
{
    public class Display
    {
        public readonly int ProgramWidth;
        public readonly int ProgramHeight;
        public readonly int GameWidth;
        public readonly int GameHeight;
        public readonly bool FullScreen;
        public readonly float Scale;

        public Display(int width, int height, bool fullScreen, float scale = 1)
        {
            FullScreen = fullScreen;
            if (FullScreen)
            {
                var widthScale = (float)CurrentGame.TheGame.GraphicsDevice.DisplayMode.Width / width;
                var heightScale = (float)CurrentGame.TheGame.GraphicsDevice.DisplayMode.Height / height;
                var newScaleModifier = Math.Min(heightScale, widthScale);
                Scale = scale * newScaleModifier;
                GameWidth = (int)Math.Round(width * newScaleModifier);
                GameHeight = (int)Math.Round(height * newScaleModifier);
                ProgramWidth = CurrentGame.TheGame.GraphicsDevice.DisplayMode.Width;
                ProgramHeight = CurrentGame.TheGame.GraphicsDevice.DisplayMode.Height;
            }
            else
            {
                Scale = scale;
                GameWidth = width;
                GameHeight = height;
                ProgramWidth = GameWidth;
                ProgramHeight = GameHeight;
            }
        }

        public void Apply(GraphicsDeviceManager device)
        {
            device.PreferredBackBufferWidth = ProgramWidth;
            device.PreferredBackBufferHeight = ProgramHeight;
            device.IsFullScreen = FullScreen;
            device.ApplyChanges();
        }
    }
}
