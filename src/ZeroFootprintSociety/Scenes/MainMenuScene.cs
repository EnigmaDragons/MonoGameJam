using Microsoft.Xna.Framework;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.AudioSystem;
using ZeroFootPrintSociety.CoreGame;
using System;

namespace ZeroFootPrintSociety.Scenes
{
    public sealed class MainMenuScene : ClickUiScene
    {
        public override void Init()
        {
            Input.On(Control.Menu, () => Environment.Exit(0));
            Sound.Music("placeholder-main-theme").Play();
            Add(new UiImage { Image = "Backgrounds/mainmenu-bg", Transform = new Transform2(new Size2(1920, 1080)) });
            Add(new ColoredRectangle { Color = Color.FromNonPremultiplied(0, 0, 0, 100), Transform = new Transform2(new Size2(1920, 1080)) });
            Add(new UiImage { Image = "UI/title-placeholder", Transform = new Transform2(new Vector2(UI.OfScreenWidth(0.5f) - 452, 180), new Size2(904, 313)) });
            var button = new TextButton(new Rectangle(UI.OfScreenWidth(0.5f) - 150, 700, 300, 50),
                () =>
                {
                    GameWorld.Clear();
                    Scene.NavigateTo("Level1");
                }, "New Game", Color.Transparent, Color.LightBlue, Color.Blue);
            AddClickable(button);
        }

        public override void Dispose() { }
    }
}
