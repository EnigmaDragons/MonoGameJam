using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System.Windows.Forms;

namespace MonoDragons.Core.Engine
{
    public static class CurrentGame
    {
        private static readonly MustInit<Game> Game = new MustInit<Game>(nameof(CurrentGame));

        public static Game TheGame => Game.Get();
        private static Cursor _cursor;
        public static Cursor Cursor
        {
            get => _cursor;
            set
            {
                _cursor = value;
                Form winForm = (Form)Form.FromHandle(TheGame.Window.Handle);
                winForm.Cursor = _cursor;
            }
        }
        public static GraphicsDevice GraphicsDevice => TheGame.GraphicsDevice;
        public static ContentManager ContentManager => TheGame.Content;

        public static void Init(Game game) => Game.Init(game);
    }
}
