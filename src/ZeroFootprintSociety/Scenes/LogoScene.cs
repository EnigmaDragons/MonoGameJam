using MonoDragons.Core.Animations;
using MonoDragons.Core.AudioSystem;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using System;

namespace ZeroFootPrintSociety.Scenes
{
    public class LogoScene : ClickUiScene
    {
        private readonly string _nextScene;

        public LogoScene(string nextScene = "Intro")
        {
            _nextScene = nextScene;
        }

        public override void Init()
        {
            Input.On(Control.Start, NavigateToMainMenu);
            AddClickable(new ScreenClickable(NavigateToMainMenu));
            Sound.SoundEffect("SFX/logo-rumble.mp3").Play();

            var anim1 = new ScreenFade { Duration = TimeSpan.FromSeconds(3.4), FromAlpha = 255, ToAlpha = 0 };
            var anim2 = new ScreenFade { Duration = TimeSpan.FromSeconds(1), FromAlpha = 0, ToAlpha = 0 };
            var anim3 = new ScreenFade { Duration = TimeSpan.FromSeconds(1), FromAlpha = 0, ToAlpha = 255 };
            Add(new UiImage { Image = "Images/Logo/oilsplash", Transform = new Transform2(new Size2(1.0.VW(), 1.0.VH())) });
            Add(anim1);
            Add(anim2);
            Add(anim3);
            anim1.Start(() => anim2.Start(() => anim3.Start(NavigateToMainMenu)));
        }

        private void NavigateToMainMenu()
        {
            Scene.NavigateTo(_nextScene);
        }
    }
}
