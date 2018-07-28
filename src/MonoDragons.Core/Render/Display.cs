using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using System;

namespace MonoDragons.Core.Render
{
    public class Display
    {
        public readonly bool FullScreen;
        public int GameWidth;
        public int GameHeight;
        public int ProgramWidth;
        public int ProgramHeight;
        public float Scale;
        private bool _initialized;

        public Display(int width, int height, bool useFullscreen, float scale = 1)
        {
            FullScreen = useFullscreen;
            GameWidth = width;
            GameHeight = height;
            Scale = scale;
            if (!FullScreen)
            {
                ProgramWidth = GameWidth;
                ProgramHeight = GameHeight;
            }
        }

        public void Apply(GraphicsDeviceManager device)
        {
            if (!_initialized && FullScreen)
            {
                var widthScale = (float)CurrentGame.TheGame.GraphicsDevice.DisplayMode.Width / GameWidth;
                var heightScale = (float)CurrentGame.TheGame.GraphicsDevice.DisplayMode.Height / GameHeight;
                var newScaleModifier = Math.Min(heightScale, widthScale);
                Scale = Scale * newScaleModifier;
                GameWidth = (int)Math.Round(GameWidth * newScaleModifier);
                GameHeight = (int)Math.Round(GameHeight * newScaleModifier);
                ProgramWidth = CurrentGame.TheGame.GraphicsDevice.DisplayMode.Width;
                ProgramHeight = CurrentGame.TheGame.GraphicsDevice.DisplayMode.Height;
                _initialized = true;
            }

            device.PreferredBackBufferWidth = ProgramWidth;
            device.PreferredBackBufferHeight = ProgramHeight;
            device.IsFullScreen = FullScreen;
            device.ApplyChanges();
        }
    }
}
