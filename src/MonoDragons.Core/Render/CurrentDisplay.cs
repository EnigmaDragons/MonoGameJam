using Microsoft.Xna.Framework;

namespace MonoDragons.Core.Render
{
    public static class CurrentDisplay
    {
        private static GraphicsDeviceManager _graphics;
        private static Display _display;
        public static Display Display {
            set
            {
                _display = value;
                _display.Apply(_graphics);
            }
        }

        public static int GameWidth => _display.GameWidth;
        public static int GameHeight => _display.GameHeight;
        public static bool FullScreen => _display.FullScreen;
        internal static int ProgramWidth => _display.ProgramWidth;
        internal static int ProgramHeight => _display.ProgramHeight;
        public static float Scale => _display.Scale;

        internal static void Init(GraphicsDeviceManager graphics, Display display)
        {
            _graphics = graphics;
            Display = display;
        }

        public static Rectangle FullScreenRectangle => new Rectangle(0, 0, _display.GameWidth, _display.GameHeight);
    }
}
