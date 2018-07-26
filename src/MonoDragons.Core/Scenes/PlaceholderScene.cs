using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Render;
using MonoDragons.Core.UserInterface;

namespace MonoDragons.Core.Scenes
{
    public sealed class PlaceholderScene : IScene
    {
        public void Init()
        {
        }

        public void Update(TimeSpan delta)
        {
        }

        public void Draw()
        {
            UI.DrawTextCentered("Placeholder", CurrentDisplay.FullScreenRectangle, Color.White);
        }

        public void Dispose()
        {
        }
    }
}
