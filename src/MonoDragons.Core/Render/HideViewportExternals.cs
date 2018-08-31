using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Graphics;
using MonoDragons.Core.Scenes;
using System;

namespace MonoDragons.Core.Render
{
    public sealed class HideViewportExternals : IScene
    {
        private readonly IScene _inner;
        private readonly MustInit<Texture2D> _black = new MustInit<Texture2D>(nameof(HideViewportExternals));

        public HideViewportExternals(IScene inner)
        {
            _inner = inner;
        }

        public void Init()
        {
            _inner.Init();
            _black.Init(new RectangleTexture(Color.Black).Create());
        }

        public void Update(TimeSpan delta)
        {
            _inner.Update(delta);
        }

        public void Draw()
        {
            _inner.Draw();
            HideExternals();
        }

        private void HideExternals()
        {
            World.Draw(_black.Get(), new Rectangle(new Point((int)Math.Round(CurrentDisplay.GameWidth / CurrentDisplay.Scale), 0),
                new Point(5000, 5000)), Color.Black);
            World.Draw(_black.Get(), new Rectangle(new Point(0, (int)Math.Round(CurrentDisplay.GameHeight / CurrentDisplay.Scale)),
                new Point(5000, 5000)), Color.Black);
        }

        public void Dispose()
        {
        }
    }
}