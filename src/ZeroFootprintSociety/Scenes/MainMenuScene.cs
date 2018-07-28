using Microsoft.Xna.Framework;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.AudioSystem;
using ZeroFootPrintSociety.CoreGame;

namespace ZeroFootPrintSociety.Scenes
{
    public sealed class MainMenuScene : ClickUiScene
    {
        public override void Init()
        {
            Input.ClearTransientBindings();
            Sound.Music("placeholder-main-theme").Play();
            Add(new ImageBox { Image = "Backgrounds/mainmenu-bg", Transform = new Transform2(new Size2(1920, 1080)) });
            Add(new ColoredRectangle { Color = Color.FromNonPremultiplied(0, 0, 0, 100), Transform = new Transform2(new Size2(1920, 1080)) });
            Add(new ImageBox { Image = "UI/title-placeholder", Transform = new Transform2(new Vector2(UI.OfScreenWidth(0.5f) - 452, 180), new Size2(904, 313)) });
            var button = new TextButton(new Rectangle(UI.OfScreenWidth(0.5f) - 150, 700, 300, 50),
                () =>
                {
                    GameWorld.Clear();
                    Scene.NavigateTo("SampleLevel");
                }, "Sample Corp", Color.Transparent, Color.LightBlue, Color.Blue);
            var lab = new TextButton(new Rectangle(UI.OfScreenWidth(0.5f) - 150, 770, 300, 50),
                () =>
                {
                    GameWorld.Clear();
                    Scene.NavigateTo("SampleLab");
                }, "Sample Lab", Color.Transparent, Color.LightBlue, Color.Blue);
            var darkAlley = new TextButton(new Rectangle(UI.OfScreenWidth(0.5f) - 150, 840, 300, 50),
                () =>
                {
                    GameWorld.Clear();
                    Scene.NavigateTo("DarkAlley");
                }, "Dark Alley", Color.Transparent, Color.LightBlue, Color.Blue);
            AddClickable(button);
            AddClickable(lab);
            AddClickable(darkAlley);
        }

        public override void Dispose()
        {
        }
    }
}
