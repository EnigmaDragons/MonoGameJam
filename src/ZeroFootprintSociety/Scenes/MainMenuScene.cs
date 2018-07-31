using Microsoft.Xna.Framework;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.AudioSystem;
using ZeroFootPrintSociety.CoreGame;
using System;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.Scenes
{
    public sealed class MainMenuScene : ClickUiScene
    {
        private readonly string _newGameScene;

        public MainMenuScene(string newGameScene)
        {
            _newGameScene = newGameScene;
        }
        
        public override void Init()
        {
            Input.On(Control.Menu, () => Environment.Exit(0));
            Sound.Music("placeholder-main-theme").Play();
            Add(new UiImage { Image = "Backgrounds/mainmenu-bg", Transform = new Transform2(new Size2(1920, 1080)) });
            Add(new ColoredRectangle { Color = UIColors.MainMenuScene_Background, Transform = new Transform2(new Size2(1920, 1080)) });
            Add(new UiImage { Image = "UI/title-placeholder", Transform = new Transform2(new Vector2(UI.OfScreenWidth(0.5f) - 452, 180), new Size2(904, 313)) });
            var button = new TextButton(
                new Rectangle(UI.OfScreenWidth(0.5f) - 150, 700, 300, 50),
                () => {
                    GameWorld.Clear();
                    Scene.NavigateTo(_newGameScene);
                }, 
                "New Game",
                UIColors.MainMenuScene_ButtonDefault,
                UIColors.MainMenuScene_ButtonHover,
                UIColors.MainMenuScene_ButtonPress
            );
            AddClickable(button);
        }

        public override void Dispose() { }
    }
}
